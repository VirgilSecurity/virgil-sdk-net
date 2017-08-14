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

namespace Virgil.SDK.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Virgil.SDK.Utils;
    using Virgil.Crypto.Interfaces;

    public class CardValidator
    {
        private readonly ICrypto crypto;
        private readonly Dictionary<string, IPublicKey> verifiers;

        public CardValidator(ICrypto crypto)
        {
            this.crypto = crypto;
            this.verifiers = new Dictionary<string, IPublicKey>();
        }

        public bool IgnoreSelfSignature { get; set; }

        public void AddVerifier(string cardId, IPublicKey publicKey)
        {
            if (string.IsNullOrWhiteSpace(cardId))
            {
                throw new ArgumentNullException(cardId);
            }

            if (publicKey == null)
            {
                throw new ArgumentNullException(nameof(publicKey));
            }

            this.verifiers.Add(cardId, publicKey);
        }

        public void AddVerifier(CardVerifier verifier)
        {
            if (verifier == null)
            {
                throw new ArgumentNullException(nameof(verifier));
            }

            if (string.IsNullOrWhiteSpace(verifier.CardId))
            {
                throw new ArgumentException($"{verifier.CardId} property is mandatory");
            }

            if (string.IsNullOrWhiteSpace(verifier.PublicKeyBase64))
            {
                throw new ArgumentException($"{verifier.PublicKeyBase64} property is mandatory");
            }

            var publicKeyBytes = BytesConvert.FromString(verifier.PublicKeyBase64, StringEncoding.BASE64);
            var publicKey = this.crypto.ImportPublicKey(publicKeyBytes);

            this.AddVerifier(verifier.CardId, publicKey);
        }

        public ValidationResult ValidateAll(IEnumerable<Card> cards)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards));
            }
            
            var validationResult = new ValidationResult();
            cards.ToList().ForEach(c => this.Validate(c, validationResult));

            return validationResult;
        }

        public ValidationResult Validate(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            var validationResult = new ValidationResult(); 
            this.Validate(card, validationResult);

            return validationResult;
        }

        private void Validate(Card card, ValidationResult result)
        {
            // validate card ID

            var fingerprint = this.crypto.CalculateFingerprint(card.Snapshot);

            // validate self-signature if this option is set

            if (!this.IgnoreSelfSignature)
            {
                var signature = card.Signatures.SingleOrDefault(s => s.CardId == card.Id);
                if (signature == null)
                {
                    result.AddError(card, "The Card doesn't contain a self-signature");
                    return;
                }

                if (!this.crypto.VerifySignature(fingerprint, signature.Signature, card.PublicKey))
                {
                    return;
                }
                
                result.AddError(card, "The Card's self-signature is not valid");
                return;
            }

            // validate signatures with additional verifiers

            foreach (var verifier in this.verifiers)
            {
                var signature = card.Signatures.SingleOrDefault(s => verifier.Key == card.Id);
                if (signature == null)
                {
                    result.AddError(card, "The Card doesn't contain a signature");
                    continue;
                }

                if (!this.crypto.VerifySignature(fingerprint, signature.Signature, verifier.Value))
                {
                    continue;
                }
                
                result.AddError(card, "The Card's signature is not valid");
            }
        }
    }
}
