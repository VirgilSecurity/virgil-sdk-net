namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    
    using Virgil.CryptoApi;

    public class AllValidPolicy : ValidationPolicy
    {
        public override IEnumerable<string> Diagnose(
            ICrypto crypto, byte[] fingerprint, Card card, IDictionary<string, IPublicKey> verifiers)
        {
            IList<string> errors = new List<string>();
            
            // validate all verifier's signature
            foreach (var verifier in verifiers)
            {
                // if verifier is not exists then this is to be regarded as a violation 
                // of the policy (all valid)
                var signature = card.Signatures.SingleOrDefault(s => verifier.Key == card.Id);
                if (signature == null)
                {
                    errors.Add("The card does not contain a signature for one of specified verifiers");
                    continue;
                }

                // validate verifier's signature 
                if (!crypto.VerifySignature(fingerprint, signature.Signature, verifier.Value))
                {
                    continue;
                }
                
                errors.Add("The card's signature is not valid for one of specified verifiers");
            }

            return errors.Any() ? errors : null;
        }
    }
}