namespace Virgil.SDK.Identities
{
    using System;
    using System.Threading.Tasks;
    
    /// <summary>
    /// Represents a class for email address verification.
    /// </summary>
    internal class EmailVerifier : IEmailVerifier
    {
        private readonly IIdentityClient identityClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailVerifier"/> class.
        /// </summary>
        public EmailVerifier(Guid actionId, IIdentityClient identityClient)
        {
            this.ActionId = actionId;
            this.identityClient = identityClient;
        }

        /// <summary>
        /// Gets the verification process ID.
        /// </summary>
        public Guid ActionId { get; }

        /// <summary>
        /// Confirms the identity using confirmation code, that has been generated to confirm an identity.
        /// </summary>
        /// <param name="code">The confirmation code that was recived on email box.</param>
        /// <param name="timeToLive">
        /// The parameter is used to limit the lifetime of the token in seconds 
        /// (maximum value is 60 * 60 * 24 * 365 = 1 year)
        /// </param>
        /// <param name="countToLive">
        /// The parameter is used to restrict the number of token 
        /// usages (maximum value is 100)
        /// </param>
        public async Task<IdentityInfo> Confirm(string code, int timeToLive = 3600, int countToLive = 1)
        {
            var result = await this.identityClient.Confirm(this.ActionId, code, timeToLive, countToLive);

            var confirmedInfo = new IdentityInfo
            {
                Value = result.Value,
                Type = "email",
                ValidationToken = result.ValidationToken
            };

            return confirmedInfo;
        }
    }
}