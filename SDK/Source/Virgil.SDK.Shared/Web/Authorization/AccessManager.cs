using System;
using System.Threading.Tasks;

namespace Virgil.SDK.Web.Authorization
{
    public class AccessManager : IAccessManager
    {
        private JsonWebToken accessToken;
        private Func<Task<string>> obtainAccessTokenFunction;

        public AccessManager(Func<Task<string>> obtainTokenFunc)
        {
            this.obtainAccessTokenFunction = obtainTokenFunc ??
                throw new ArgumentNullException(nameof(obtainTokenFunc));
        }

        public async Task<JsonWebToken> GetAccessTokenAsync()
        {
            if (!ValidateAccessToken())
            {
                var jwt = await this.obtainAccessTokenFunction.Invoke();
                this.accessToken = JsonWebToken.From(jwt);
            }

            return this.accessToken;
        }

        public bool ValidateAccessToken()
        {
            //todo: check app signature
            return (this.accessToken != null && !this.accessToken.IsExpired());
        }
    }
}
