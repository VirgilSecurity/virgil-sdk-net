namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using Virgil.CryptoApi;

    public interface IValidationRule
    {
        void Initialize(ICrypto crypto);
        IEnumerable<string> CheckForErrors(ICrypto crypto, Card card);
    }
}