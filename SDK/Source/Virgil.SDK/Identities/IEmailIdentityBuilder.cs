namespace Virgil.SDK.Identities
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an email identity builder.
    /// </summary>
    public interface IEmailIdentityBuilder : IIdentityBuilder
    {
        /// <summary>
        /// Initiates a process to verify the email address and confirm an identity.
        /// </summary>
        /// <param name="extraFields">
        /// In some cases it could be necessary to pass some parameters and receive them back 
        /// in an email. For this special case an optional <c>extraFields</c> dictionary can be used. 
        /// All values passed in <c>extraFields</c> parameter will be passed back in an email 
        /// in a hidden form with extra hidden fields.
        /// </param>
        /// <remarks>
        /// Use method <see cref="Confirm"/> to confirm building identity.
        /// </remarks>
        Task Verify(Dictionary<string, string> extraFields = null);

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
        Task Confirm(string code, int timeToLive = 3600, int countToLive = 1);
    }
}