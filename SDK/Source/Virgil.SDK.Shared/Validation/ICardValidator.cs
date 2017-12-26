namespace Virgil.SDK.Validation
{
    using Virgil.CryptoApi;

    public interface ICardValidator
    {
        ValidationResult Validate(ICardManagerCrypto cardManagerCrypto, Card card);
    }
}