using Virgil.SDK.Cryptography;

namespace Virgil.SDK.Client.Requests
{
    class RevokeUserCardRequest : RevokeCardRequest
    {
        public void ApplicationSign(ICrypto crypto, string appId, IPrivateKey appKey)
        {
            this.Sign(crypto, appId, appKey);
        }
    }
}
