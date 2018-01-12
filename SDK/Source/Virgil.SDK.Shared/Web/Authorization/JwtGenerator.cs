using System;
using System.Collections.Generic;
using System.Text;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Web.Authorization
{
    public class JwtGenerator
    {

        public IPrivateKey AccessPrivateKey { get; private set; }

        public string AccessKeyId { get; private set; }

        public string AppId { get; private set; }

        public TimeSpan LifeTime { get; private set; }

        public IAccessTokenSigner AccessTokenSigner { get; private set; }

        public JwtGenerator(
            string appId, 
            IPrivateKey accessPrivateKey, 
            string accessKeyId,
            TimeSpan lifeTime,
            IAccessTokenSigner accessTokenSigner
            )
        {
            this.AppId = appId;
            this.AccessPrivateKey = accessPrivateKey;
            this.LifeTime = lifeTime;
            this.AccessKeyId = accessKeyId;
            this.AccessTokenSigner = accessTokenSigner;
        }
        public string GenerateToken(string identity, Dictionary<string, string> data = null)
        {
            var jwtBody = new JwtBodyContent(
                AppId, 
                identity,
                LifeTime,
                data);

            var jwtHeader = new JwtHeaderContent(AccessTokenSigner.GetAlgorithm(), AccessKeyId);
           
            var jwt = new Jwt(jwtHeader, jwtBody);
            var jwtBytes = Bytes.FromString(jwt.ToString());

            jwt.SignatureData = AccessTokenSigner.GenerateTokenSignature(jwtBytes, AccessPrivateKey);
            return jwt.ToString();
        }

    }
}
