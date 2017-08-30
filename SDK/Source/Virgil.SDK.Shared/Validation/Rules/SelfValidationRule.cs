namespace Virgil.SDK.Validation.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Virgil.CryptoApi;

    public class SelfValidationRule : IValidationRule
    {
        public void Initialize(ICrypto crypto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> CheckForErrors(ICrypto crypto, Card card)
        {
            var signature = card.Signatures.SingleOrDefault(s => s.CardId == card.Id);
            if (signature == null)
            {
                return new []{ "The card doesn't contain a self-signature" };
            }

            return !crypto.VerifySignature(card.Fingerprint, signature.Signature, card.PublicKey) 
                ? null : new[] { "The card's self-signature is not valid"};
        }
    }
}