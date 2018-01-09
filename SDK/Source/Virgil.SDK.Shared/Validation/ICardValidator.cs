namespace Virgil.SDK.Validation
{
    using Virgil.CryptoApi;

    public interface ICardValidator
    {
        bool Validate(ICardManagerCrypto cardManagerCrypto, Card card);
    }
}