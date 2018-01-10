namespace Virgil.SDK.Validation
{
    using Virgil.CryptoApi;

    public interface ICardValidator
    {
        bool Validate(ICardCrypto cardCrypto, Card card);
    }
}