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

namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using System.Linq;
 
    using Virgil.CryptoAPI;
    using Virgil.SDK.Common;

    public class ExtendedValidator : ICardValidator
    {
        private readonly List<SignerInfo> whitelist;
        private readonly Dictionary<string, IPublicKey> signersCache;
        
        private string VirgilCardId          = "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853";
        private string VirgilPublicKeyBase64 = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUNvd0JRWURLMlZ3QXlFQVlSNTAx"+
                                                     "a1YxdFVuZTJ1T2RrdzRrRXJSUmJKcmMyU3lhejVWMWZ1RytyVnM9Ci0tLS0tRU5E"+
                                                     "IFBVQkxJQyBLRVktLS0tLQo=";

        public ExtendedValidator()
        {
            this.whitelist = new List<SignerInfo>();
            this.signersCache = new Dictionary<string, IPublicKey>();
        }

        public bool IgnoreSelfSignature { get; set; }
        
        public bool IgnoreVirgilSignature { get; set; }

        public IEnumerable<SignerInfo> Whitelist
        {
            get => this.whitelist;
            set
            {
                this.whitelist.Clear();
                this.signersCache.Clear();
                
                if (value != null)
                {
                    this.whitelist.AddRange(value);
                }
            }
        }

        public bool Validate(ICardCrypto cardCrypto, Card card)
        {
            var result = new ValidationResult();
            
            if (!this.IgnoreSelfSignature)
            {
                ValidateSignerSignature(cardCrypto, card, card.Id, card.PublicKey, "Self", result);
            }

            if (!this.IgnoreVirgilSignature)
            {
                var virgilPublicKey = this.GetCachedPublicKey(cardCrypto, VirgilCardId, VirgilPublicKeyBase64);
                ValidateSignerSignature(cardCrypto, card, VirgilCardId, virgilPublicKey, "Virgil", result);
            }

            if (!this.whitelist.Any())
            {
                return result.IsValid;
            }
            
            // select a first intersected signer from whitelist. 
            var signerCardId = this.whitelist.Select(s => s.CardId)
                .Intersect(card.Signatures.Select(it => it.SignerId)).FirstOrDefault();
                
            // if signer's signature is not exists in card's collection then this is to be regarded 
            // as a violation of the policy (at least one).
            if (signerCardId == null)
            {
                result.AddError("The card does not contain signature from specified Whitelist");
            }
            else
            {
                var signerInfo = this.whitelist.Single(s => s.CardId == signerCardId);
                var signerPublicKey = this.GetCachedPublicKey(cardCrypto, signerCardId, signerInfo.PublicKeyBase64);
                
                ValidateSignerSignature(cardCrypto, card, signerCardId, signerPublicKey, "Whitelist", result);
            }

            return result.IsValid;
        }

        private IPublicKey GetCachedPublicKey(ICardCrypto cardCrypto, string signerCardId, string signerPublicKeyBase64)
        {
            if (this.signersCache.ContainsKey(signerCardId))
            {
                return this.signersCache[signerCardId];
            }
                
            var publicKeyBytes = Bytes.FromString(signerPublicKeyBase64, StringEncoding.BASE64);
            var publicKey = cardCrypto.ImportPublicKey(publicKeyBytes);

            this.signersCache.Add(signerCardId, publicKey);
                
            return publicKey;         
        }

     
        private static void ValidateSignerSignature(ICardCrypto cardCrypto, Card card, string signerCardId, 
            IPublicKey signerPublicKey, string signerKind, ValidationResult result)
        {
            var signature = card.Signatures.SingleOrDefault(s => s.SignerId == signerCardId);
            if (signature == null)
            {
                result.AddError($"The card does not contain the {signerKind} signature");
                return;
            }
            // validate verifier's signature 
            if (cardCrypto.VerifySignature(signature.Signature, card.Fingerprint, signerPublicKey))
            {
                return;
            }
            result.AddError($"The {signerKind} signature is not valid");
        }

        internal void ChangeServiceCreds(string cardId, string publicKey)
        {
            this.VirgilCardId = cardId;
            this.VirgilPublicKeyBase64 = publicKey;
        }
    }
}