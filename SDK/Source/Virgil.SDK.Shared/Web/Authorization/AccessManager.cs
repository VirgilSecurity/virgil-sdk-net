using System;
using System.Threading.Tasks;

namespace Virgil.SDK.Shared.Web.Authorization
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

        //todo: check app signature
        public async Task<JsonWebToken> GetAccessTokenAsync()
        {
            if (this.accessToken == null || this.accessToken.IsExpired())
            {
                var jwt = await this.obtainAccessTokenFunction.Invoke();
                this.accessToken = JsonWebToken.From(jwt);
            }

            return this.accessToken;
        }
    }
}
