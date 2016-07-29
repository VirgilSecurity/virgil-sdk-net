namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// Base exception for all Identity Service exceptions
    /// </summary>
    /// <seealso cref="VirgilServiceException" />
    public class IdentityServiceServiceException : VirgilServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityServiceServiceException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public IdentityServiceServiceException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}