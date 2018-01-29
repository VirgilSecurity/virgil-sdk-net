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

using Virgil.Crypto;

namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using System.Linq;
 
    using Virgil.CryptoAPI;
    using Virgil.SDK.Common;

    public class VirgilCardVerifier : ICardVerifier
    {
        private List<WhiteList> whiteLists;
        private readonly Dictionary<string, IPublicKey> signersCache;
        private readonly ICardCrypto cardCrypto;
        
        private string VirgilCardId          = "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853";
        private string VirgilPublicKeyBase64 = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUNvd0JRWURLMlZ3QXlFQVlSNTAx"+
                                                     "a1YxdFVuZTJ1T2RrdzRrRXJSUmJKcmMyU3lhejVWMWZ1RytyVnM9Ci0tLS0tRU5E"+
                                                     "IFBVQkxJQyBLRVktLS0tLQo=";

        public VirgilCardVerifier(ICardCrypto crypto)
        {
            this.whiteLists = new List<WhiteList>();
            this.signersCache = new Dictionary<string, IPublicKey>();
            this.cardCrypto = crypto;
        }

        public VirgilCardVerifier() : this(new VirgilCardCrypto())
        {
        }

        public bool VerifySelfSignature { get; set; }
        
        public bool VerifyVirgilSignature { get; set; }

        public IEnumerable<WhiteList> WhiteLists
        {
            get => this.whiteLists;
            set
            {
                this.whiteLists.Clear();
                this.signersCache.Clear();
                
                if (value != null)
                {
                    this.whiteLists.AddRange(value);
                }
            }
        }

        public bool VerifyCard(Card card)
        {
            var result = new ValidationResult();
            
            if (this.VerifySelfSignature)
            {
                ValidateSignerSignature(card, card.Id, card.PublicKey, "Self", result);
            }

            if (this.VerifyVirgilSignature)
            {
                var virgilPublicKey = this.GetCachedPublicKey(VirgilCardId, VirgilPublicKeyBase64);
                ValidateSignerSignature(card, VirgilCardId, virgilPublicKey, "Virgil", result);
            }

            if (!this.whiteLists.Any())
            {
                return result.IsValid;
            }

            // select a signers' ids from card signatures. 
            var signerIds = card.Signatures.Select(x => x.SignerId);

            // choose all nonempty whitelists 
            var verifiersCredentialsLists = whiteLists
                .Select(s => s.VerifiersCredentials);

            foreach (var verifiersCredentials in verifiersCredentialsLists)
            {
                var intersectedCreds = verifiersCredentials.Where(x => signerIds.Contains(x.CardId));

                // if card doesn't have signer's signature from the whitelist then 
                //this is to be regarded as a violation of the policy (at least one).
                if (!intersectedCreds.Any())
                {
                    result.AddError("The card does not contain signature from specified Whitelist");
                    break;
                }
                foreach (var intersectedCred in intersectedCreds)
                {
                    var res = new ValidationResult();
                    var signerPublicKey = this.GetCachedPublicKey(intersectedCred.CardId, intersectedCred.PublicKey);
                    ValidateSignerSignature(card, intersectedCred.CardId, signerPublicKey, "Whitelist", res);
                    if (res.IsValid)
                    {
                        break;
                    }
                    if (intersectedCred == intersectedCreds.Last())
                    {
                        result.AddError(res.Errors.Last());
                    }
                }
            }
            return result.IsValid;
        }

        private IPublicKey GetCachedPublicKey(string signerCardId, string signerPublicKeyBase64)
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

     
        private void ValidateSignerSignature(Card card, string signerCardId, 
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