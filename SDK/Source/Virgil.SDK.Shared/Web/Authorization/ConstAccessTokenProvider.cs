using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Virgil.SDK.Web.Authorization
{
    class ConstAccessTokenProvider : IAccessTokenProvider
    {
        private readonly IAccessToken accessToken;

        public ConstAccessTokenProvider(IAccessToken accessToken)
        {
            this.accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
        }

        public Task<IAccessToken> GetTokenAsync(TokenContext context)
        {
           if (accessToken.IsExpired())
            {
                throw new ExpiredAccessTokenException();
            }

            Func<Task<IAccessToken>> obtainToken = async () =>
            {
                return accessToken;
            };
            return obtainToken.Invoke();
        }
    }
}
