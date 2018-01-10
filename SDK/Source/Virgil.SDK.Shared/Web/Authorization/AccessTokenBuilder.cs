using System;
using System.Collections.Generic;
using System.Text;
using Virgil.CryptoApi;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Web.Authorization
{
    public class AccessTokenBuilder
    {

        public string AccountId { get; private set; }

        public string AppId { get; private set; }

        public TimeSpan LifeTime { get; private set; }

        private JsonWebTokenSignatureGenerator JwtSignatureGenerator { get; set; }

        public AccessTokenBuilder(
            string accountId, 
            string appId, 
            TimeSpan lifeTime,
            IPrivateKey apiKey, 
            ICardCrypto cardCrypto
            )
        {
            this.AccountId = accountId;
            this.AppId = appId;
            this.LifeTime = lifeTime;

            this.JwtSignatureGenerator = new JsonWebTokenSignatureGenerator()
            {
                CardCrypto = cardCrypto,
                PrivateKey = apiKey
            };
        }
        public string Build(string identity, Dictionary<string, string> data = null)
        {
            var jwtBody = new JsonWebTokenBody(
                AccountId, 
                new string[] { AppId }, 
                "1.0", 
                LifeTime,
                identity,
                data);
            var jwtHeader = new JsonWebTokenHeader("VIRGIL", "JWT");
            var jwt = new JsonWebToken(jwtHeader, jwtBody, JwtSignatureGenerator);
            return jwt.ToString();
        }

    }
}
