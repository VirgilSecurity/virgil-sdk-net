using Virgil.CryptoApi;

namespace Virgil.SDK.Web.Authorization
{
    public class JsonWebTokenSignatureGenerator
    {
        public ICrypto Crypto { get; set; }
        public IPrivateKey PrivateKey { get; set; }
    }
}