using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Virgil.SDK.Web.Authorization
{
    public class VirgilAccessTokenProvider : IAccessTokenProvider
    {
        private Jwt accessToken;
        private Func<Task<string>> obtainAccessTokenFunction;
        private JwtParser jwtParser;

        public VirgilAccessTokenProvider(Func<Task<string>> obtainTokenFunc)
        {
            this.obtainAccessTokenFunction = obtainTokenFunc ??
                                             throw new ArgumentNullException(nameof(obtainTokenFunc));
        }
        public Task<IAccessToken> GetTokenAsync(bool forceReload=false)
        {
            return GetVirgilTokenAsync(forceReload);
        }

        private async Task<IAccessToken> GetVirgilTokenAsync(bool forceReload)
        {
            if (forceReload || !ValidateAccessToken())
            {
                var jwt = await this.obtainAccessTokenFunction.Invoke();
                this.accessToken = JwtParser.Parse(jwt);
            }
            return this.accessToken;
        }

        private bool ValidateAccessToken()
        {
            return (this.accessToken != null && !this.accessToken.IsExpired());
        }
    }
}
