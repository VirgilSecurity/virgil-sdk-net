namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Exceptions.VirgilException" />
    public class IdentityServiceException : VirgilException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentityServiceException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public IdentityServiceException(string message) : base(message)
        {
        }
    }
}