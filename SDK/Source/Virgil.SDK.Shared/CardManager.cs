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
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK
{
    using System.Threading.Tasks;
    using Virgil.CryptoAPI;
    using Virgil.SDK.Validation;
    using Virgil.SDK.Web;

    public class CardManager
    {
        private readonly ICardCrypto cardCrypto;
        private readonly CardClient client;
        private readonly ICardValidator validator;
        private readonly Func<string, Task<string>> signCallBackFunc;
        private readonly IAccessTokenProvider accessTokenProvider;
        public CardManager(CardsManagerParams @params)
        {
            ValidateCardManagerParams(@params);

            this.cardCrypto = @params.CardCrypto;
            this.accessTokenProvider = @params.accessTokenProvider;
            this.client = string.IsNullOrWhiteSpace(@params.ApiUrl)
                ? new CardClient()
                : new CardClient(@params.ApiUrl);

            this.validator = @params.Validator;
            this.signCallBackFunc = @params.SignCallBackFunc;
        }

        private static void ValidateCardManagerParams(CardsManagerParams @params)
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
            var card = Card.Parse(this.cardCrypto, rawCard);

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

            var cards = Card.Parse(this.cardCrypto, rawCards).ToArray();
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
        public async Task<Card> PublishCardAsync(IPrivateKey privateKey, IPublicKey publicKey, string previousCardId)
        {
            var token = await this.accessTokenProvider.GetTokenAsync();
            var csr = this.GenerateCSR(new CSRParams
            {
                Identity = token.Identity(),
                PublicKey = publicKey,
                PrivateKey = privateKey,
                PreviousCardId = previousCardId
            });

            if (this.signCallBackFunc != null)
            {
                var signedCsrByApp = await this.signCallBackFunc.Invoke(csr.Export());
                csr = CSR.Import(this.cardCrypto, signedCsrByApp);
            }
            var rawCard = await this.client.PublishCardAsync(csr.RawCard, token.ToString()).ConfigureAwait(false);
            var card = Card.Parse(this.cardCrypto, rawCard);
            this.ValidateCards(new[] { card });

            return card;
        }

        /// <summary>
        /// Generates a new request in order to apply for a card registration. It contains the public key for 
        /// which the card should be registered, identity information (such as a user name) and integrity 
        /// protection in form of digital self signature.
        /// </summary>
        /// <param name="csrParams">The information about identity and public key.</param>
        /// <returns>A new instance of <see cref="CSR"/> class.</returns>
        private CSR GenerateCSR(CSRParams csrParams)
        {
            return CSR.Generate(this.cardCrypto, csrParams);
        }

        /// <summary>
        /// Signs the CSR using specified signer parameters included private key.
        /// </summary>
        /// <param name="csr">The <see cref="CSR"/> to be signed.</param>
        /// <param name="params">The signer parameters.</param>
        public void SignCSR(CSR csr, SignParams @params)
        {
            csr.Sign(this.cardCrypto, @params);
        }

        /// <summary>
        /// Imports the CSR from string. 
        /// </summary>
        /// <param name="csr">The CSR in string representation.</param>
        /// <returns>The instance of CSR object.</returns>
        internal CSR ImportCSR(string csr)
        {
            return CSR.Import(this.cardCrypto, csr);
        }

        private void ValidateCards(IEnumerable<Card> cards)
        {
            if (this.validator == null)
            {
                return;
            }

            foreach (var card in cards)
            {
                var result = this.validator.Validate(this.cardCrypto, card);
                if (!result)
                {
                    throw new CardValidationException("Validation errors have been detected");
                }
            }
        }
    }
}