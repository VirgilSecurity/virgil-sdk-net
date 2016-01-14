namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// Occurs when service response sign is invalid
    /// </summary>
    /// <seealso cref="Virgil.SDK.Exceptions.VirgilException" />
    public class ServiceSignVerificationException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceSignVerificationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServiceSignVerificationException(string message) : base(message)
        {
        }
    }
}