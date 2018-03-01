#region Copyright (C) Virgil Security Inc.
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Client;

    /// <summary>
    /// This class provides a methods for validating <see cref="CardModel"/>, by default 
    /// it validates self and service signatures.
    /// </summary>
    public class CardValidator : ICardValidator
    {
        /// <summary>
        /// Validator should not validate card v3 by default.
        /// </summary>
        public bool VerifyV3Cards { get; set; }

        private readonly ICrypto crypto;
        private readonly Dictionary<string, IPublicKey> verifiers;   

        private const string ServiceCardId    = "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853";
        private const string ServicePublicKey = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUNvd0JRWURLMlZ3QXlFQVlSNTAx" +
                                                "a1YxdFVuZTJ1T2RrdzRrRXJSUmJKcmMyU3lhejVWMWZ1RytyVnM9Ci0tLS0tRU5E" +
                                                "IFBVQkxJQyBLRVktLS0tLQo=";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CardValidator"/> class.
        /// </summary>
        public CardValidator(ICrypto crypto)
        {
			this.crypto = crypto;
			this.verifiers = new Dictionary<string, IPublicKey>();
        }

		/// <summary>
		///  Add default service verifiers to validator
		/// </summary>
		public void AddDefaultVerifiers()
		{
			var servicePublicKey = crypto.ImportPublicKey(Convert.FromBase64String(ServicePublicKey));
			this.verifiers.Add(ServiceCardId, servicePublicKey);
		}

        /// <summary>
        /// Adds the signature verifier.
        /// </summary>
        public void AddVerifier(string verifierCardId, byte[] verifierPublicKey)
        {
            if (string.IsNullOrWhiteSpace(verifierCardId))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(verifierCardId));

            if (verifierPublicKey == null)
                throw new ArgumentNullException(nameof(verifierPublicKey));
            
            var publicKey = this.crypto.ImportPublicKey(verifierPublicKey);
            this.verifiers.Add(verifierCardId, publicKey);
        }       

        /// <summary>
        /// Validates a <see cref="CardModel"/> using pined Public Keys.
        /// </summary>
        public virtual bool Validate(CardModel card)
        {
            // Support for legacy Cards.
            if (card.Meta.Version == "3.0" && VerifyV3Cards && card.SnapshotModel.Scope == CardScope.Global)
            {
                return true;
            }
            
            var fingerprint = this.crypto.CalculateFingerprint(card.Snapshot);
            var fingerprintHex = fingerprint.ToHEX();

            if (fingerprintHex != card.Id)
            {
                return false;
            }

            // add self signature verifier

            var allVerifiers = this.verifiers.ToDictionary(it => it.Key, it => it.Value);
            allVerifiers.Add(fingerprintHex, this.crypto.ImportPublicKey(card.SnapshotModel.PublicKeyData));

            foreach (var verifier in allVerifiers)
            {
                if (!card.Meta.Signatures.ContainsKey(verifier.Key))
                {
                    return false;
                }
                
                var isValid = this.crypto.Verify(fingerprint.GetValue(), 
                    card.Meta.Signatures[verifier.Key], verifier.Value);

                if (!isValid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}