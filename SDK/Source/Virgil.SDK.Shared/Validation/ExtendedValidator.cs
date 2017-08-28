namespace Virgil.SDK.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;

    public class ExtendedValidator : ICardValidator
    {
        private readonly ICrypto crypto;
        private readonly Dictionary<string, IPublicKey> verifiers;
        private readonly Dictionary<string, IPublicKey> virgilVerifiers;
        
        private readonly bool ignoreVirgilSignatures;
        private readonly bool ignoreSelfSignature;
        private readonly ValidationPolicy behaviour;
        
        private const string VirgilAPICardId          = "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853";
        private const string VirgilAPIPublicKeyBase64 = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUNvd0JRWURLMlZ3QXlFQVlSNTAx" +
                                                        "a1YxdFVuZTJ1T2RrdzRrRXJSUmJKcmMyU3lhejVWMWZ1RytyVnM9Ci0tLS0tRU5E" +
                                                        "IFBVQkxJQyBLRVktLS0tLQo=";
        
        public ExtendedValidator(ICrypto crypto, ValidationRules rules)
        {
            if (crypto == null)
            {
                throw new ArgumentNullException(nameof(crypto));
            }

            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules));
            }

            this.crypto = crypto;

            this.behaviour = rules.Policy;
            this.ignoreSelfSignature = rules.IgnoreSelfSignature;
            this.ignoreVirgilSignatures = rules.IgnoreVirgilSignatures;
            this.verifiers = new Dictionary<string, IPublicKey>();
            this.virgilVerifiers = new Dictionary<string, IPublicKey>
            {
                [VirgilAPICardId] = this.crypto.ImportPublicKey(
                    BytesConvert.FromString(VirgilAPIPublicKeyBase64, StringEncoding.BASE64))
            };

            if (rules.Verifiers == null)
            {
                return;
            }
            
            foreach (var verifierInfo in rules.Verifiers)
            {
                var publicKeyBytes = BytesConvert.FromString(verifierInfo.PublicKeyBase64, StringEncoding.BASE64);
                var publicKey = this.crypto.ImportPublicKey(publicKeyBytes);

                this.verifiers.Add(verifierInfo.CardId, publicKey);
            }
        }

        public ValidationResult Validate(Card card)
        {
            var result = new ValidationResult();
            var fingerprint = this.crypto.CalculateFingerprint(card.Snapshot);

            if (!this.ignoreSelfSignature)
            {
                this.ValidateSelfSignature(card, fingerprint, result);
            }

            if (!this.ignoreVirgilSignatures)
            {
                this.ValidateVirgilSignatures(card, fingerprint, result);
            }

            if (!this.verifiers.Any())
            {
                return result;
            }
            
            switch (this.behaviour)
            {
                case ValidationPolicy.AtLeastOneValid: 
                    this.ValidateAtLeastOneVerifiersSignature(card, fingerprint, result);
                    break;
                case ValidationPolicy.AllValid:
                    this.ValidateAllVerifiersSignatures(card, fingerprint, result);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        private void ValidateSelfSignature(Card card, byte[] fingerprint, ValidationResult result)
        {
            var signature = card.Signatures.SingleOrDefault(s => s.CardId == card.Id);
            if (signature == null)
            {
                result.AddError(card, "The card doesn't contain a self-signature");
                return;
            }

            if (!this.crypto.VerifySignature(fingerprint, signature.Signature, card.PublicKey))
            {
                return;
            }
                
            result.AddError(card, "The card's self-signature is not valid");
        }

        private void ValidateVirgilSignatures(Card card, byte[] fingerprint, ValidationResult result)
        {
            // get a first verifier matched with card signatures
            var verifierCardId = this.virgilVerifiers.Select(it => it.Key)
                .Intersect(card.Signatures.Select(it => it.CardId)).FirstOrDefault();
                
            if (verifierCardId == null)
            {
                result.AddError(card, "The card does not contain any Virgil's signatures");
                return;
            }

            var signature = card.Signatures.Single(it => it.CardId == verifierCardId).Signature;
            var publicKey = this.verifiers[verifierCardId];
                
            // validate verifier's signature 
            if (this.crypto.VerifySignature(fingerprint, signature, publicKey))
            {
                return;
            }
                
            result.AddError(card, "The Virgil's signature is not valid");
        }

        private void ValidateAtLeastOneVerifiersSignature(Card card, byte[] fingerprint, ValidationResult result)
        {
            // get a first verifier matched with card signatures
            var verifierCardId = this.verifiers.Select(it => it.Key)
                .Intersect(card.Signatures.Select(it => it.CardId)).FirstOrDefault();
                
            // if verifier is not exists then this is to be regarded as a violation 
            // of the policy (at least one valid)
            if (verifierCardId == null)
            {
                result.AddError(card, "The card does not contain any matched signature for specified verifiers");
                return;
            }

            var signature = card.Signatures.Single(it => it.CardId == verifierCardId).Signature;
            var publicKey = this.verifiers[verifierCardId];
                
            // validate verifier's signature 
            if (this.crypto.VerifySignature(fingerprint, signature, publicKey))
            {
                return;
            }
                
            result.AddError(card, "The card's signature is not valid for specified verifier");
        }

        private void ValidateAllVerifiersSignatures(Card card, byte[] fingerprint, ValidationResult result)
        {
            // validate all verifier's signature
            foreach (var verifier in this.verifiers)
            {
                // if verifier is not exists then this is to be regarded as a violation 
                // of the policy (all valid)
                var signature = card.Signatures.SingleOrDefault(s => verifier.Key == card.Id);
                if (signature == null)
                {
                    result.AddError(card, "The card does not contain a signature for one of specified verifiers");
                    continue;
                }

                // validate verifier's signature 
                if (!this.crypto.VerifySignature(fingerprint, signature.Signature, verifier.Value))
                {
                    continue;
                }
                
                result.AddError(card, "The card's signature is not valid for one of specified verifiers");
            }
        }
    }
}