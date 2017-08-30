namespace Virgil.SDK.Validation.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;

    public class WhitelistValidationRule : IValidationRule
    {
        private readonly Whitelist whitelist;
        private readonly IDictionary<string, IPublicKey> signers;

        public WhitelistValidationRule(Whitelist whitelist)
        {
            this.whitelist = whitelist;
            this.signers = new Dictionary<string, IPublicKey>();
        }

        public void Initialize(ICrypto crypto)
        {
            foreach (var signer in this.whitelist.Signers)
            {
                var publicKeyBytes = BytesConvert.FromString(signer.PublicKeyBase64, StringEncoding.BASE64);
                var publicKey = crypto.ImportPublicKey(publicKeyBytes);

                this.signers.Add(signer.CardId, publicKey);
            }
        }

        public IEnumerable<string> CheckForErrors(ICrypto crypto, Card card)
        {
            switch (this.whitelist.Strategy)
            {
                case WhitelistStrategy.AtLeastOne: 
                    return this.VerifyAtLeastOne(crypto, card);
                case WhitelistStrategy.All:
                    return this.VerifyAll(crypto, card);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<string> VerifyAll(ICrypto crypto, Card card)
        {
            IList<string> errors = new List<string>();
            
            // validate all verifier's signature
            foreach (var signer in this.signers)
            {
                // if verifier is not exists then this is to be regarded as a violation 
                // of the policy (all valid)
                var signature = card.Signatures.SingleOrDefault(s => signer.Key == card.Id);
                if (signature == null)
                {
                    errors.Add("The card does not contain a signature for one of specified verifiers");
                    continue;
                }

                // validate verifier's signature 
                if (!crypto.VerifySignature(card.Fingerprint, signature.Signature, signer.Value))
                {
                    continue;
                }
                
                errors.Add("The card's signature is not valid for one of specified verifiers");
            }

            return errors.Any() ? errors : null;
        }

        private IEnumerable<string> VerifyAtLeastOne(ICrypto crypto, Card card)
        {
            // get a first verifier matched with card signatures
            var verifierCardId = this.signers.Select(it => it.Key)
                .Intersect(card.Signatures.Select(it => it.CardId)).FirstOrDefault();
                
            // if verifier is not exists then this is to be regarded as a violation     
            // of the policy (at least one valid)
            if (verifierCardId == null)
            {
                return new[] { "The card does not contain any matched signature for specified verifiers" };
            }

            var signature = card.Signatures.Single(it => it.CardId == verifierCardId).Signature;
            var publicKey = this.signers[verifierCardId];
                
            // validate verifier's signature 
            return crypto.VerifySignature(card.Fingerprint, signature, publicKey) 
                ? null 
                : new[] { "The card's signature is not valid for specified verifier" };
        }
    }
}