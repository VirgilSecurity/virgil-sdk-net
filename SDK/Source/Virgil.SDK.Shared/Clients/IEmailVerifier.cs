namespace Virgil.SDK.Identities
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a class for identity confirmation.
    /// </summary>
    public interface IEmailVerifier
    {
        /// <summary>
        /// Gets the ID of identity verification process.
        /// </summary>
        Guid ActionId { get; }

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
        Task<IdentityInfo> Confirm(string code, int timeToLive = 3600, int countToLive = 1);
    }
}