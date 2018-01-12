using Virgil.CryptoAPI;
using Virgil.SDK.Common;

namespace Virgil.SDK.Web.Authorization
{
    public class JwtVerifier
    {
        public IAccessTokenSigner AccessTokenSigner { get; private set; }
        public IPublicKey AccessPublicKey { get; private set; }
        public string AccessPublicKeyId { get; private set; }
        public JwtVerifier(IAccessTokenSigner accessTokenSigner, IPublicKey accessPublicKey, string accessPublicKeyId)
        {
            this.AccessTokenSigner = accessTokenSigner;
            this.AccessPublicKey = accessPublicKey;
            this.AccessPublicKeyId = accessPublicKeyId;
        }

        public bool VerifyToken(Jwt jwToken)
        {
            if (jwToken.HeaderContent.AccessKeyId != AccessPublicKeyId)
            {
                return false;
            }
            var jwtBytes = Bytes.FromString(jwToken.ToString());
            return this.AccessTokenSigner.VerifyTokenSignature(
                jwToken.SignatureData, 
                jwtBytes,
                AccessPublicKey);
        }
    }
}