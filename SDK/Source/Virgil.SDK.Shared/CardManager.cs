#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2018 Virgil Security Inc.
// 
// Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions 
// are met:
// 
//   (1) Redistributions of source code must retain the above copyright
//   notice, this list of conditions and the following disclaimer.
//   
//   (2) Redistributions in binary form must reproduce the above copyright
//   notice, this list of conditions and the following disclaimer in
//   the documentation and/or other materials provided with the
//   distribution.
//   
//   (3) Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived 
//   from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Virgil.SDK.Signer;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK
{
    using System.Threading.Tasks;
    using Virgil.CryptoAPI;
    using Virgil.SDK.Common;
    using Verification;
    using Virgil.SDK.Web;

    /// <summary>
    /// The <see cref="CardManager"/> provides operations with Virgil Cards.
    /// </summary>
    public class CardManager
    {
        /// <summary>
        /// an instance of <see cref="ICardCrypto"/> which provides cryptographic operations.
        /// </summary>
        public readonly ICardCrypto CardCrypto;


        public readonly ModelSigner ModelSigner;

        public  ICardClient Client { get; internal set; }

        /// <summary>
        /// an instance of <see cref="ICardVerifier"/> which provides card verification.
        /// </summary>
        public readonly ICardVerifier CardVerifier;

        /// <summary>
        /// CallBack which performs additional signatures for card before publishing.
        /// </summary>
        public readonly Func<RawSignedModel, Task<RawSignedModel>> SignCallBack;

        /// <summary>
        /// an instance of <see cref="IAccessTokenProvider"/> which provides token delivery.
        /// </summary>
        public readonly IAccessTokenProvider AccessTokenProvider;

        /// <summary>
        /// If true <see cref="CardManager"/> repeats once a request to the <see cref="CardClient"/> after
        ///  getting <see cref="UnauthorizedClientException"/>.
        /// </summary>
        public readonly bool RetryOnUnauthorized;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardManager"/> class by
        /// a specified set of parameters.
        /// </summary>
        /// <param name="params">the instance of <see cref="CardManagerParams"/>. </param>
        public CardManager(CardManagerParams @params)
        {
            ValidateCardManagerParams(@params);
            this.CardCrypto = @params.CardCrypto;
            this.AccessTokenProvider = @params.AccessTokenProvider;
            this.Client = string.IsNullOrWhiteSpace(@params.ApiUrl)
                ? new CardClient()
                : new CardClient(@params.ApiUrl);

            this.CardVerifier = @params.Verifier;
            this.ModelSigner = new ModelSigner(CardCrypto);
            this.SignCallBack = @params.SignCallBack;
            this.RetryOnUnauthorized = @params.RetryOnUnauthorized;
        }


        private static void ValidateCardManagerParams(CardManagerParams @params)
        {
            if (@params.CardCrypto == null)
            {
                throw new ArgumentException($"{nameof(@params.CardCrypto)} property is mandatory");
            }

            if (@params.AccessTokenProvider == null)
            {
                throw new ArgumentException($"{nameof(@params.AccessTokenProvider)} property is mandatory");
            }
        }

        /// <summary>
        /// Gets the card by specified ID.
        /// </summary>
        /// <param name="cardId">The card ID to be found.</param>
        /// <returns>The instance of found <see cref="Card"/>.</returns>
        public async Task<Card> GetCardAsync(string cardId)
        {
            if (String.IsNullOrWhiteSpace(cardId))
            {
                throw new ArgumentException(nameof(cardId));
            }
            var tokenContext = new TokenContext(null, "get");
            var accessToken = await AccessTokenProvider.GetTokenAsync(tokenContext);

            var cardWithStatus = (Tuple<RawSignedModel, bool>)await TryExecute(
                async (IAccessToken token) =>
                {
                    return await this.Client.GetCardAsync(cardId,
                        token.ToString());
                }, tokenContext, accessToken);
            var card = CardUtils.Parse(this.CardCrypto, (RawSignedModel)cardWithStatus.Item1, cardWithStatus.Item2);

            if (card.Id != cardId)
            {
                throw new CardVerificationException("Invalid card");
            }
            this.ValidateCards(new Card[] { card });

            return card;
        }

        /// <summary>
        /// Searches for cards by specified identity.
        /// </summary>
        /// <param name="identity">The identity to be found.</param>
        /// <returns>The list of found <see cref="Card"/>.</returns>
        public async Task<IList<Card>> SearchCardsAsync(string identity)
        {
            if (String.IsNullOrWhiteSpace(identity))
            {
                throw new ArgumentException(nameof(identity));
            }
            var tokenContext = new TokenContext(null, "search");
            var accessToken = await AccessTokenProvider.GetTokenAsync(tokenContext);
            var rawCards = await TryExecute(
                async (IAccessToken token) =>
                {
                    return await Client.SearchCardsAsync(identity,
                        token.ToString()
                    );
                }, tokenContext, accessToken);

            var cards = CardUtils.Parse(this.CardCrypto, (IEnumerable<RawSignedModel>)rawCards).ToArray();
            if (cards.Any(x => x.Identity != identity))
            {
                throw new CardVerificationException("Invalid cards");
            }
            this.ValidateCards(cards);
            return CardUtils.LinkedCardLists(cards);
        }

        /// <summary>
        /// Publish a new Card using specified <see cref="CardParams"/>.
        /// </summary>
        /// <param name="cardParams">The instance of <see cref="CardParams"/> class.
        /// It should has mandatory parameters: public key, private key</param>
        /// <returns>The instance of newly published <see cref="Card"/> class.</returns>
        public async Task<Card> PublishCardAsync(CardParams cardParams)
        {
            ValidateCardParams(cardParams);

            var tokenContext = new TokenContext(cardParams.Identity, "publish");

            var token = await this.AccessTokenProvider.GetTokenAsync(tokenContext);
            var rawSignedModel = GenerateRawCard(new CardParams()
            {
                Identity = token.Identity(),
                PrivateKey = cardParams.PrivateKey,
                PublicKey = cardParams.PublicKey,
                PreviousCardId = cardParams.PreviousCardId,
                ExtraFields = cardParams.ExtraFields
            });

            return await PublishRawSignedModel(rawSignedModel, tokenContext, token);
        }

        private async Task<Card> PublishRawSignedModel(RawSignedModel rawSignedModel,
            TokenContext context,
            IAccessToken accessToken)
        {
            if (this.SignCallBack != null)
            {
                rawSignedModel = await this.SignCallBack.Invoke(rawSignedModel);
            }

            var publishedModel = await TryExecute(
                async (IAccessToken token) =>
            {
                var rawCard = await Client.PublishCardAsync(
                    rawSignedModel,
                    token.ToString()
                );
                return rawCard;
            }, context, accessToken);

            if (!rawSignedModel.ContentSnapshot.SequenceEqual(((RawSignedModel)publishedModel).ContentSnapshot))
            {
                throw new CardVerificationException("Publishing returns invalid card");
            }
            var card = CardUtils.Parse(this.CardCrypto, (RawSignedModel)publishedModel);
            this.ValidateCards(new[] { card });

            return card;
        }

        private async Task<object> TryExecute(Func<IAccessToken, Task<object>> func, TokenContext context, IAccessToken token)
        {
            object result = null;
            // if RetryOnUnauthorized == true then add one attempt
            var attemptsNumber = (RetryOnUnauthorized ? 2 : 1);
            for (var i = 0; i < attemptsNumber; i++)
            {
                try
                {
                    result = await func.Invoke(token);
                    break;
                }
                catch (UnauthorizedClientException e)
                {
                    if (i == attemptsNumber - 1)
                    {
                        throw e;
                    }

                    token = await this.AccessTokenProvider.GetTokenAsync(
                        new TokenContext(context.Identity, context.Operation, true)
                        );
                }
            }
            return result;
        }

        /// <summary>
        /// Generates a new <see cref="RawSignedModel"/> in order to apply for a card registration. 
        /// </summary> 
        /// <param name="cardParams">The instance of <see cref="CardParams"/> class.
        /// It should has mandatory parameters: identity, public key, private key</param>
        /// <returns>A new self signed instance of <see cref="RawSignedModel"/> class.</returns>
        public RawSignedModel GenerateRawCard(CardParams cardParams)
        {
            ValidateCardParams(cardParams, true);
            var timeNow = DateTime.UtcNow;
            //to truncate milliseconds and microseconds
            timeNow = timeNow.AddTicks(-timeNow.Ticks % TimeSpan.TicksPerSecond);
            var model = RawSignedModelUtils.Generate(CardCrypto, cardParams, timeNow);
            ModelSigner.SelfSign(model, cardParams.PrivateKey, cardParams.ExtraFields);
            return model;
        }

        private static void ValidateCardParams(CardParams cardParams, bool validateIdentity = false)
        {
            if (cardParams == null)
            {
                throw new ArgumentNullException(nameof(cardParams));
            }
            if (validateIdentity && String.IsNullOrWhiteSpace(cardParams.Identity))
            {
                throw new ArgumentException($"{nameof(cardParams.Identity)} property is mandatory");
            }
            if (cardParams.PublicKey == null)
            {
                throw new ArgumentException($"{nameof(cardParams.PublicKey)} property is mandatory");
            }

            if (cardParams.PrivateKey == null)
            {
                throw new ArgumentException($"{nameof(cardParams.PrivateKey)} property is mandatory");
            }
        }

        /// <summary>
        /// Publish a new Card using specified <see cref="RawSignedModel"/>.
        /// </summary>
        /// <param name="rawSignedModel">The instance of <see cref="RawSignedModel"/> class.</param>
        /// <returns>The instance of newly published <see cref="Card"/> class.</returns>
        public async Task<Card> PublishCardAsync(RawSignedModel rawSignedModel)
        {
            var cardContent = SnapshotUtils.ParseSnapshot<RawCardContent>(
                rawSignedModel.ContentSnapshot);
            var tokenContext = new TokenContext(cardContent.Identity, "publish");

            var token = await this.AccessTokenProvider.GetTokenAsync(tokenContext);

            return await PublishRawSignedModel(rawSignedModel, tokenContext, token);
        }

        /// <summary>
        /// Exports the specified card as a BASE64 string.
        /// </summary>
        /// <param name="card">the instance of <see cref="Card"/> to be exported.</param>
        /// <returns>serialized card to BASE64. </returns>
        /// <remarks>Use this method to transmit the card 
        /// signing request through the network.</remarks>
        public string ExportCardAsString(Card card)
        {
            return ExportCardAsRawCard(card).ExportAsString();
        }

        /// <summary>
        /// Exports the specified card as a json.
        /// </summary>
        /// <param name="card">the instance of <see cref="Card"/> to be exported.</param>
        /// <returns>serialized card to JSON. </returns>
        /// <remarks>Use this method to transmit the card 
        /// signing request through the network.</remarks>
        public string ExportCardAsJson(Card card)
        {
            return ExportCardAsRawCard(card).ExportAsJson();
        }

        /// <summary>
        /// Exports card as an instance of <see cref="RawSignedModel"/>.
        /// </summary>
        /// <param name="card">the instance of <see cref="Card"/> to be exported.</param>
        /// <returns>an instance of <see cref="RawSignedModel"/> representing Card. </returns>
        public RawSignedModel ExportCardAsRawCard(Card card)
        {
            return RawSignedModelUtils.Parse(CardCrypto, card);
        }

        /// <summary>
        /// Imports and verifies <see cref="Card"/> from json.
        /// </summary>
        /// <param name="json">json to get card from.</param>
        /// <returns>Imported and verified card.</returns>
        public Card ImportCardFromJson(string json)
        {
            var rawSignedModel = RawSignedModelUtils.GenerateFromJson(json);
            var card = CardUtils.Parse(CardCrypto, rawSignedModel);
            this.ValidateCards(new[] { card });
            return card;
        }

        /// <summary>
        /// Imports and verifies <see cref="Card"/> from an 
        /// instance of <see cref="RawSignedModel"/>.
        /// </summary>
        /// <param name="model">an instance of <see cref="RawSignedModel"/> 
        /// to get card from.</param>
        /// <returns>Imported and verified card.</returns>
        public Card ImportCard(RawSignedModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var card = CardUtils.Parse(CardCrypto, model);
            this.ValidateCards(new[] { card });
            return card;
        }

        /// <summary>
        /// Imports and verifies <see cref="Card"/> from BASE64 string.
        /// </summary>
        /// <param name="str">BASE64 string to get card from.</param>
        /// <returns>Imported and verified card.</returns>
        public Card ImportCardFromString(string str)
        {
            var rawSignedModel = RawSignedModelUtils.GenerateFromString(str);
            var card = CardUtils.Parse(CardCrypto, rawSignedModel);
            this.ValidateCards(new[] { card });
            return card;
        }

        private void ValidateCards(Card[] cards)
        {
            if (this.CardVerifier == null)
            {
                return;
            }

            foreach (var card in cards)
            {
                var result = this.CardVerifier.VerifyCard(card);
                if (!result)
                {
                    throw new CardVerificationException("Validation errors have been detected");
                }
            }
        }
    }
}