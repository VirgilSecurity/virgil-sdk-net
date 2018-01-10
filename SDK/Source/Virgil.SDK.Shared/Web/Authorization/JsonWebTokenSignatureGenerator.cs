using Virgil.CryptoApi;

namespace Virgil.SDK.Web.Authorization
{
    public class JsonWebTokenSignatureGenerator
    {
        public ICardCrypto CardCrypto { get; set; }
        public IPrivateKey PrivateKey { get; set; }
    }
}