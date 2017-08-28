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

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;
    using Virgil.SDK.Validation;
    using Virgil.SDK.Web;
   
    public class CardManager  
    {
        private readonly ICrypto crypto;
        private readonly CardsClient client;
        private readonly ExtendedValidator validator;
        
        public CardManager(CardsManagerParams @params)
        {
            if (string.IsNullOrWhiteSpace(@params.ApiToken))
            {
                throw new ArgumentException($"{@params.ApiToken} property is mandatory");
            }

            this.crypto = @params.Crypto ?? throw new ArgumentException($"{@params.Crypto} property is mandatory");
            
            this.client = new CardsClient(@params.ApiToken);
            this.validator = new ExtendedValidator(this.crypto, @params.Validation);
        }

        public async Task<Card> GetCardAsync(string cardId)
        {
            var rawCard = await this.client.GetByIdAsync(cardId);
            var card = CardUtils.ParseRawCard(this.crypto, rawCard);
            
            this.ValidateCards(new[] { card });
            
            return card;
        }

        public async Task<IList<Card>> SearchCardsAsync(string identity, string identityType = null)
        {
            var rawCards = await this.client.SearchAsync(new SearchCriteria
            {
                Identities = new[] { identity },
                IdentityType = identityType
            });
            
            var cards = CardUtils.ParseRawCards(this.crypto, rawCards);
            this.ValidateCards(cards);
            
            return cards;
        }
        
        public async Task<IList<Card>> SearchCardsAsync(IEnumerable<string> identities, string identityType = null)
        {
            var rawCards = await this.client.SearchAsync(new SearchCriteria
            {
                Identities = identities,
                IdentityType = identityType
            });
            
            var cards = CardUtils.ParseRawCards(this.crypto, rawCards);
            this.ValidateCards(cards);
            
            return cards;
        }

        public async Task<Card> CreateCardAsync(CreateCardRequest request)
        {
            var rawCard = await this.client.CreateCardAsync(request);
            var card = CardUtils.ParseRawCard(this.crypto, rawCard);
            this.ValidateCards(new[] { card });

            return card;
        }

        public async Task RevokeCardAsync(RevokeCardRequest request)
        {
            await this.client.RevokeCardAsync(request);
        }

        private void ValidateCards(IEnumerable<Card> cards)
        {
            var errors = new List<ValidationError>();
            foreach (var card in cards)
            {
                var result = this.validator.Validate(card);
                if (!result.IsValid)
                {
                    errors.AddRange(result.Errors);
                }
            }
            
            throw new CardValidationException(errors);
        }
    }
}