namespace Virgil.SDK.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;

    public class VirgilValidationRule : IValidationRule
    {
        private const string VirgilCardId          = "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853";
        private const string VirgilPublicKeyBase64 = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUNvd0JRWURLMlZ3QXlFQVlSNTAx"+
                                                     "a1YxdFVuZTJ1T2RrdzRrRXJSUmJKcmMyU3lhejVWMWZ1RytyVnM9Ci0tLS0tRU5E"+
                                                     "IFBVQkxJQyBLRVktLS0tLQo=";
        private IDictionary<string, IPublicKey> virgilVerifiers;
        
        public void Initialize(ICrypto crypto)
        {
            this.virgilVerifiers = new Dictionary<string, IPublicKey>();
            
            var publicKeyBytes = BytesConvert.FromString(VirgilPublicKeyBase64, StringEncoding.BASE64);
            this.virgilVerifiers.Add(VirgilCardId, crypto.ImportPublicKey(publicKeyBytes));
        }

        public IEnumerable<string> CheckForErrors(ICrypto crypto, Card card)
        {
            // get a first verifier matched with card signatures
            var verifierCardId = this.virgilVerifiers.Select(it => it.Key)
                .Intersect(card.Signatures.Select(it => it.CardId)).FirstOrDefault();
                
            if (verifierCardId == null)
            {
                return new []{ "The card does not contain any Virgil's signatures" };
            }

            var signature = card.Signatures.Single(it => it.CardId == verifierCardId).Signature;
            var publicKey = this.virgilVerifiers[verifierCardId];
                
            // validate verifier's signature 
            return crypto.VerifySignature(card.Fingerprint, signature, publicKey) 
                ? null : new []{ "The Virgil's signature is not valid" };
        }
    }
}