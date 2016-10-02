#region Copyright (C) 2016 Virgil Security Inc.
// Copyright (C) 2016 Virgil Security Inc.
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

namespace Virgil.SDK.Common
{
    using System.Collections.Generic;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Client;

    public class CardValidator : ICardValidator
    {
        private readonly Crypto crypto;
        private readonly Dictionary<string, PublicKey> verifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardValidator"/> class.
        /// </summary>
        public CardValidator(Crypto crypto)
        {
            this.crypto = crypto;
            this.verifiers = new Dictionary<string, PublicKey>();
        }

        public void PinPublicKey(string cardId, byte[] publicKeyData)
        {
            var publicKey = this.crypto.ImportPublicKey(publicKeyData);
            this.verifiers.Add(cardId, publicKey);
        }

        /// <summary>
        /// Validates a <see cref="Card"/> using pined Public Keys.
        /// </summary>
        public bool Validate(Card card)
        {
            // Support for legacy Cards.
            if (card.Version == "3.0")
            {
                return true;
            }

            var fingerprint = this.crypto.CalculateFingerprint(card.Snapshot);
            
            foreach (var verifier in this.verifiers)
            {
                if (!card.Signatures.ContainsKey(verifier.Key))
                    return false;

                var isValid = this.crypto.Verify(fingerprint.GetValue(), 
                    card.Signatures[verifier.Key], verifier.Value);
                
                if (!isValid)
                    return false;
            }

            return true;
        }
    }
}