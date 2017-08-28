namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    
    using Virgil.CryptoApi;

    public class AtLeastOneValidPolicy : ValidationPolicy
    {
        public override IEnumerable<string> Diagnose(
            ICrypto crypto, byte[] fingerprint, Card card, IDictionary<string, IPublicKey> verifiers)
        {
            // get a first verifier matched with card signatures
            var verifierCardId = verifiers.Select(it => it.Key)
                .Intersect(card.Signatures.Select(it => it.CardId)).FirstOrDefault();
                
            // if verifier is not exists then this is to be regarded as a violation     
            // of the policy (at least one valid)
            if (verifierCardId == null)
            {
                return new[] { "The card does not contain any matched signature for specified verifiers" };
            }

            var signature = card.Signatures.Single(it => it.CardId == verifierCardId).Signature;
            var publicKey = verifiers[verifierCardId];
                
            // validate verifier's signature 
            return crypto.VerifySignature(fingerprint, signature, publicKey) 
                ? null 
                : new[] { "The card's signature is not valid for specified verifier" };
        }
    }
}