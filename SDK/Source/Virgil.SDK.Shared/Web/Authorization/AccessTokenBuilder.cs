using System;
using System.Collections.Generic;
using System.Text;
using Virgil.CryptoApi;

namespace Virgil.SDK.Shared.Web.Authorization
{
    public class AccessTokenBuilder
    {

        public string AccountId { get; private set; }

        public string AppId { get; private set; }

        public TimeSpan LifeTime { get; private set; }

        public Dictionary<string, string> Data { get; private set; }

        public AccessTokenBuilder(string accountId, 
            string appId, 
            TimeSpan lifeTime, 
            Dictionary<string, string> data = null)
        {
            this.AccountId = accountId;
            this.AppId = appId;
            this.LifeTime = lifeTime;
            this.Data = data;
        }
        public string Build(IPrivateKey apiKey, ICrypto crypto)
        {
            var jwtBody = new JsonWebTokenBody(this.AccountId, 
                new string[] { this.AppId }, 
                "1.0", this.LifeTime, this.Data);
            var jwtSignatureGenerator = new JsonWebTokenSignatureGenerator()
            {
                Crypto = crypto,
                PrivateKey = apiKey
            };
            var jwt = new JsonWebToken(jwtBody, jwtSignatureGenerator);
            return jwt.ToString();
        }

    }
}
