namespace Virgil.SDK.Validation
{
    using Virgil.CryptoAPI;

    public interface ICardValidator
    {
        bool Validate(ICardCrypto cardCrypto, Card card);
    }
}