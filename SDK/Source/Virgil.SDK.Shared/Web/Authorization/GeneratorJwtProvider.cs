using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Web.Authorization
{
    public class GeneratorJwtProvider : IAccessTokenProvider
    {
        public readonly JwtGenerator Generator;

        public readonly Dictionary<string, string> AdditionalData;

        public GeneratorJwtProvider(JwtGenerator generator, Dictionary<string, string> additionalData)
        {
            Generator = generator ?? throw new ArgumentNullException(nameof(generator));
            AdditionalData = additionalData ?? throw new ArgumentNullException(nameof(additionalData));
        }
        public Task<IAccessToken> GetTokenAsync(TokenContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Func<Task<IAccessToken>> obtainToken = async () =>
            {
                var token = Generator.GenerateToken(context.Identity, AdditionalData);
                return token;
            };
            return obtainToken.Invoke();
        }
    }
}
