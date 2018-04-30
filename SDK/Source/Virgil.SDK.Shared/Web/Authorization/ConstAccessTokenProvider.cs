namespace Virgil.SDK.Web.Authorization
{
    using System;
    using System.Threading.Tasks;
    
    /// <summary>
    /// The <see cref="ConstAccessTokenProvider"/> class provides an opportunity to  
    /// use constant access token.
    /// </summary>
    public class ConstAccessTokenProvider : IAccessTokenProvider
    {
        private readonly IAccessToken accessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstAccessTokenProvider" /> class.
        /// </summary>
        /// <param name="accessToken">an instance of <see cref="IAccessToken"/></param>
        public ConstAccessTokenProvider(IAccessToken accessToken)
        {
            this.accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
        }

        /// <summary>
        /// Gets access token.
        /// </summary>
        /// <param name="context">can be null as it does not affect the result.</param>
        /// <returns>the specified token in the
        public Task<IAccessToken> GetTokenAsync(TokenContext context = null)
        {
            Func<Task<IAccessToken>> obtainToken = () =>
            {
                return Task.FromResult(accessToken);
            };

            Task.FromResult(this.accessToken);

            return obtainToken.Invoke();
        }
    }
}
