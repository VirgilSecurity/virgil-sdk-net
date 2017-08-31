namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using System.Linq;
 
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;

    public class ExtendedValidator : ICardValidator
    {
        private readonly List<SignerInfo> whitelist;
        private readonly Dictionary<string, IPublicKey> signersCache;
        
        private const string VirgilCardId          = "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853";
        private const string VirgilPublicKeyBase64 = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUNvd0JRWURLMlZ3QXlFQVlSNTAx"+
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

        public ValidationResult Validate(ICrypto crypto, Card card)
        {
            var result = new ValidationResult();
            
            if (!this.IgnoreSelfSignature)
            {
                ValidateSignerSignature(crypto, card, card.Id, card.PublicKey, "Self", result);
            }

            if (!this.IgnoreVirgilSignature)
            {
                var virgilPublicKey = this.GetCachedPublicKey(crypto, VirgilCardId, VirgilPublicKeyBase64);
                ValidateSignerSignature(crypto, card, VirgilCardId, virgilPublicKey, "Virgil", result);
            }

            if (!this.whitelist.Any())
            {
                return result;
            }
            
            // select a first intersected signer from whitelist. 
            var signerCardId = this.whitelist.Select(s => s.CardId)
                .Intersect(card.Signatures.Select(it => it.CardId)).FirstOrDefault();
                
            // if signer's signature is not exists in card's collection then this is to be regarded 
            // as a violation of the policy (at least one).
            if (signerCardId == null)
            {
                result.AddError("The card does not contain signature from specified Whitelist");
            }
            else
            {
                var signerInfo = this.whitelist.Single(s => s.CardId == signerCardId);
                var signerPublicKey = this.GetCachedPublicKey(crypto, signerCardId, signerInfo.PublicKeyBase64);
                
                ValidateSignerSignature(crypto, card, VirgilCardId, signerPublicKey, "Whitelist", result);
            }

            return result;
        }

        private IPublicKey GetCachedPublicKey(ICrypto crypto, string signerCardId, string signerPublicKeyBase64)
        {
            if (this.signersCache.ContainsKey(signerCardId))
            {
                return this.signersCache[signerCardId];
            }
                
            var publicKeyBytes = BytesConvert.FromString(signerPublicKeyBase64, StringEncoding.BASE64);
            var publicKey = crypto.ImportPublicKey(publicKeyBytes);

            this.signersCache.Add(signerCardId, publicKey);
                
            return publicKey;         
        }

        private static void ValidateSignerSignature(ICrypto crypto, Card card, string signerCardId, 
            IPublicKey signerPublicKey, string signerKind, ValidationResult result)
        {
            var signature = card.Signatures.SingleOrDefault(s => s.CardId == signerCardId);
            if (signature == null)
            {
                result.AddError($"The card does not contain the {signerKind} signature");
                return;
            }
                
            // validate verifier's signature 
            if (crypto.VerifySignature(card.Fingerprint, signature.Signature, signerPublicKey))
            {
                return;
            }
                
            result.AddError($"The {signerKind} signature is not valid");
        }
    }
}