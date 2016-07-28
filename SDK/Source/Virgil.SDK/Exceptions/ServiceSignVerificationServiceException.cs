namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// Occurs when service response sign is invalid
    /// </summary>
    /// <seealso cref="VirgilServiceException" />
    public class ServiceSignVerificationServiceException : VirgilServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceSignVerificationServiceException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServiceSignVerificationServiceException(string message) : base(message)
        {
        }
    }
}