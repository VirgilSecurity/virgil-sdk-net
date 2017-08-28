namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using Virgil.CryptoApi;

    public abstract class ValidationPolicy
    {
        public abstract IEnumerable<string> Diagnose(
            ICrypto crypto, byte[] fingerprint, Card card, IDictionary<string, IPublicKey> verifiers);
    }
}