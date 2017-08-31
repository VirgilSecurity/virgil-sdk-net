namespace Virgil.SDK.Validation
{
    using Virgil.CryptoApi;

    public interface ICardValidator
    {
        ValidationResult Validate(ICrypto crypto, Card card);
    }
}