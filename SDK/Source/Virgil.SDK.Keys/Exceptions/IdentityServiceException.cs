namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Exceptions.VirgilException" />
    public class IdentityServiceException : VirgilException
    {
      

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityServiceException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public IdentityServiceException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}