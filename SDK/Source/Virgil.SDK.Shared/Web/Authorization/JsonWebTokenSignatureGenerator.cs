using Virgil.CryptoApi;

namespace Virgil.SDK.Web.Authorization
{
    public class JsonWebTokenSignatureGenerator
    {
        public ICardManagerCrypto CardManagerCrypto { get; set; }
        public IPrivateKey PrivateKey { get; set; }
    }
}