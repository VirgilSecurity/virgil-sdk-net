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
using System.Security.Cryptography.X509Certificates;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK
{
    using System.Threading.Tasks;
    using Virgil.CryptoAPI;
    using Virgil.SDK.Common;
    using Virgil.SDK.Validation;
    using Virgil.SDK.Web;

    public class CardManager
    {
        private readonly ICardCrypto cardCrypto;
        private readonly ModelSigner modelSigner;
        private readonly CardClient client;
        private readonly ICardVerifier cardVerifier;
        private readonly Func<RawSignedModel, Task<RawSignedModel>> signCallBack;
        private readonly IAccessTokenProvider accessTokenProvider;
        public CardManager(CardManagerParams @params)
        {
            ValidateCardManagerParams(@params);

            this.cardCrypto = @params.CardCrypto;
            this.accessTokenProvider = @params.accessTokenProvider;
            this.client = string.IsNullOrWhiteSpace(@params.ApiUrl)
                ? new CardClient()
                : new CardClient(@params.ApiUrl);

            this.cardVerifier = @params.Verifier;
            this.modelSigner = new ModelSigner(cardCrypto);
            this.signCallBack = @params.SignCallBackFunc;
        }

        private static void ValidateCardManagerParams(CardManagerParams @params)
        {
            if (@params.CardCrypto == null)
            {
                throw new ArgumentException($"{nameof(@params.CardCrypto)} property is mandatory");
            }

            if (@params.accessTokenProvider == null)
            {
                throw new ArgumentException($"{nameof(@params.accessTokenProvider)} property is mandatory");
            }
        }

        /// <summary>
        /// Gets the card by specified ID.
        /// </summary>
        /// <param name="cardId">The card ID to be found.</param>
        /// <returns>The instance of found <see cref="Card"/>.</returns>
        public async Task<Card> GetCardAsync(string cardId)
        {
            var token = await this.accessTokenProvider.GetTokenAsync();

            var rawCard = await this.client.GetCardAsync(cardId, token.ToString());
            var card = CardUtils.Parse(this.cardCrypto, rawCard);

            this.ValidateCards(new[] { card });

            return card;
        }

        /// <summary>
        /// Searches for cards by specified identity.
        /// </summary>
        /// <param name="identity">The identity to be found.</param>
        public async Task<IList<Card>> SearchCardsAsync(string identity)
        {
            var token = await this.accessTokenProvider.GetTokenAsync();

            var rawCards = await this.client.SearchCardsAsync(identity, token.ToString());

            var cards = CardUtils.Parse(this.cardCrypto, rawCards).ToArray();
            this.ValidateCards(cards);

            return ActualCards(cards);
        }

        private IList<Card> ActualCards(Card[] cards)
        {
            // sort array DESC by date, the latest cards are at the beginning
            Array.Sort(cards, (a, b) => -1 * DateTime.Compare(a.CreatedAt, b.CreatedAt));

            // actualCards contains 'actual' cards: 
            //1. which aren't associated with another one
            //2. which wasn't overrided as 'previous card'
            var actualCards = new List<Card>();
            var previousIds = new List<string>();

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];

                // there is no a card which references to current card, so it is the freshest one
                if (!previousIds.Contains(card.Id))
                    actualCards.Add(card);

                if (card.PreviousCardId != null)
                {
                    previousIds.Add(card.PreviousCardId);

                    // find previous card in the early cards
                    for (int j = i + 1; j < cards.Length; j++)
                    {
                        var earlyCard = cards[j];
                        if (earlyCard.Id == card.PreviousCardId)
                        {
                            card.PreviousCard = earlyCard;
                            break;
                        }
                    }
                }
            }
            return actualCards;
        }

        /// <summary>
        /// Publish a new Card using specified CSR.
        /// </summary>
        /// <param name="privateKey">The instance of <see cref="IPrivateKey"/> class.</param>
        /// <param name="previousCardId">The previous card id.</param>
        /// <returns>The instance of newly created <see cref="Card"/> class.</returns>
        public async Task<Card> PublishCardAsync(CardParams cardParams)
        {
            var token = await this.accessTokenProvider.GetTokenAsync();
            var rawSignedModel = GenerateRawCard(new CardParams()
            {
                Identity = !String.IsNullOrWhiteSpace(cardParams.Identity) ? cardParams.Identity : token.Identity(),
                PrivateKey = cardParams.PrivateKey,
                PublicKey = cardParams.PublicKey,
                PreviousCardId = cardParams.PreviousCardId,
                Meta = cardParams.Meta
            });
               
            return await PublishRawSignedModel(rawSignedModel, token);
        }

        private async Task<Card> PublishRawSignedModel(RawSignedModel rawSignedModel, IAccessToken token)
        {
            if (this.signCallBack != null)
            {
                rawSignedModel = await this.signCallBack.Invoke(rawSignedModel);
            }
            

            // todo catch UnauthorizedError
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var publishedModel = await this.client.PublishCardAsync(
                        rawSignedModel, token.ToString()).ConfigureAwait(false);
                    var card = CardUtils.Parse(this.cardCrypto, publishedModel);
                    this.ValidateCards(new[] {card});

                    return card;
                }
                catch (UnauthorizedClientException e)
                {
                    if (i == 2)
                    {
                        throw e;
                    }
                    else
                    {
                        token = await this.accessTokenProvider.GetTokenAsync();
                    }
                }
            }
            return null;
        }

        public RawSignedModel GenerateRawCard(CardParams cardParams)
        {
            ValidateCardParams(cardParams);
            var model = RawSignedModel.Generate(cardCrypto, cardParams);
            modelSigner.SelfSign(model, new SignParams(){SignerPrivateKey = cardParams.PrivateKey });
            return model;
        }

        private static void ValidateCardParams(CardParams cardParams)
        {
            if (cardParams == null)
            {
                throw new ArgumentNullException(nameof(cardParams));
            }

            if (cardParams.Identity == null)
            {
                throw new ArgumentException($"{cardParams.Identity} property is mandatory");
            }
            if (cardParams.PublicKey == null)
            {
                throw new ArgumentException($"{cardParams.PublicKey} property is mandatory");
            }

            if (cardParams.PrivateKey == null)
            {
                throw new ArgumentException($"{cardParams.PrivateKey} property is mandatory");
            }
        }

        public async Task<Card> PublishCardAsync(RawSignedModel rawSignedModel)
        {
            var token = await this.accessTokenProvider.GetTokenAsync();
            return await PublishRawSignedModel(rawSignedModel, token);
        }


        /// <summary>
        /// Generates a new request in order to apply for a card registration. It contains the public key for 
        /// which the card should be registered, identity information (such as a user name) and integrity 
        /// protection in form of digital self signature.
        /// </summary>
        /// <param name="cardParams">The information about identity and public key.</param>
        /// <returns>A new instance of <see cref="CSR"/> class.</returns>
       // private CSR GenerateRawCard(CardParams cardParams)
       // {
        //    return CSR.Generate(this.cardCrypto, cardParams);
        //}

        /// <summary>
        /// Signs the CSR using specified signer parameters included private key.
        /// </summary>
        /// <param name="csr">The <see cref="CSR"/> to be signed.</param>
        /// <param name="params">The signer parameters.</param>
       // public void SignCSR(CSR csr, ExtendedSignParams @params)
        //{
        //    csr.Sign(this.cardCrypto, @params);
       // }

        /// <summary>
        /// Imports the CSR from string. 
        /// </summary>
        /// <param name="csr">The CSR in string representation.</param>
        /// <returns>The instance of CSR object.</returns>
        //internal CSR ImportCSR(string csr)
        //{
         //   return CSR.Import(this.cardCrypto, csr);
        //}

        private void ValidateCards(IEnumerable<Card> cards)
        {
            if (this.cardVerifier == null)
            {
                return;
            }

            foreach (var card in cards)
            {
                var result = this.cardVerifier.VerifyCard(card);
                if (!result)
                {
                    throw new CardValidationException("Validation errors have been detected");
                }
            }
        }
    }
}