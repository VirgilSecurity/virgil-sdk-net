#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2017 Virgil Security Inc.
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Virgil.SDK.Shared.Web.Authorization;

namespace Virgil.SDK
{
    using System.Threading.Tasks;
    using Virgil.CryptoApi;
    using Virgil.SDK.Validation;
    using Virgil.SDK.Web;

    public class CardManager  
    {
        private readonly ICrypto crypto;
        private readonly CardsClient client;
        private readonly ICardValidator validator;
        
        public CardManager(CardsManagerParams @params)
        {
            if (string.IsNullOrWhiteSpace(@params.AccountId))
            {
                throw new ArgumentException($"{nameof(@params.AccountId)} property is mandatory");
            }

            if (string.IsNullOrWhiteSpace(@params.AppId))
            {
                throw new ArgumentException($"{nameof(@params.AppId)} property is mandatory");
            }

            if (@params.Crypto == null)
            {
                throw new ArgumentException($"{nameof(@params.Crypto)} property is mandatory");
            }

            if (@params.ApiKey == null)
            {
                throw new ArgumentException($"{nameof(@params.ApiKey)} property is mandatory");
            }

            this.crypto = @params.Crypto;

            var jwt = new JsonWebToken(@params.AccountId, new string[]{@params.AppId}, "1.0");
            jwt.SignBy(@params.Crypto, @params.ApiKey);

            this.client = string.IsNullOrWhiteSpace(@params.ApiUrl)
                ? new CardsClient(jwt.ToString(), @params.AppId)
                : new CardsClient(jwt.ToString(), @params.AppId, @params.ApiUrl);

            this.validator = @params.Validator;
        }

        /// <summary>
        /// Gets the card by specified ID.
        /// </summary>
        /// <param name="cardId">The card ID to be found.</param>
        /// <returns>The instance of found <see cref="Card"/>.</returns>
        public async Task<Card> GetCardAsync(string cardId)
        {
            var rawCard = await this.client.GetByIdAsync(cardId);
            var card = Card.Parse(this.crypto, rawCard);

            this.ValidateCards(new[] { card });
            
            return card;
        }

        /// <summary>
        /// Searches for cards by specified identity.
        /// </summary>
        /// <param name="identity">The identity to be found.</param>
        public async Task<IList<Card>> SearchCardsAsync(string identity)
        {
            var rawCards = await this.client.SearchAsync(new SearchCriteria
            {
                Identity = identity
            });
            
            var cards = Card.Parse(this.crypto, rawCards).ToArray();
            this.ValidateCards(cards);
 
            return ActualCards(cards);
        }

        internal IList<Card> ActualCards(Card[] cards)
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
        /// Searches for cards by specified list of identities.
        /// </summary>
        /// <param name="identities">The list of identities.</param>
        //public async Task<IList<Card>> SearchCardsAsync(IEnumerable<string> identities)
        //{
        //    var rawCards = await this.client.SearchAsync(new SearchCriteria
        //    {
        //        Identities = identities
        //    });
            
        //    var cards = Card.Parse(this.crypto, rawCards);
        //    this.ValidateCards(cards);
            
        //    return cards;
        //}
        
        /// <summary>
        /// Publish a new Card using specified CSR.
        /// </summary>
        /// <param name="csr">The instance of <see cref="CSR"/> class.</param>
        /// <returns>The instance of newly created <see cref="Card"/> class.</returns>
        public async Task<Card> PublishCardAsync(CSR csr)
        {
            var rawCard = await this.client.PublishCardAsync(csr.RawCard).ConfigureAwait(false);
            var card = Card.Parse(this.crypto, rawCard);
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
        public CSR GenerateCSR(CSRParams csrParams)
        {
            return CSR.Generate(this.crypto, csrParams);
        }

        /// <summary>
        /// Signs the CSR using specified signer parameters included private key.
        /// </summary>
        /// <param name="csr">The <see cref="CSR"/> to be signed.</param>
        /// <param name="params">The signer parameters.</param>
        public void SignCSR(CSR csr, SignParams @params)
        {
            csr.Sign(this.crypto, @params);
        }

        /// <summary>
        /// Imports the CSR from string. 
        /// </summary>
        /// <param name="csr">The CSR in string representation.</param>
        /// <returns>The instance of CSR object.</returns>
        public CSR ImportCSR(string csr)
        {
            return CSR.Import(this.crypto, csr);
        }

        private void ValidateCards(IEnumerable<Card> cards)
        {
            if (this.validator == null)
            {
                return;
            }

            var errors = new List<string>();
            foreach (var card in cards)
            {
                var result = this.validator.Validate(this.crypto, card);
                if (!result.IsValid)
                {
                    errors.AddRange(result.Errors);
                }
            }
            
            throw new CardValidationException(errors);
        }
    }
}