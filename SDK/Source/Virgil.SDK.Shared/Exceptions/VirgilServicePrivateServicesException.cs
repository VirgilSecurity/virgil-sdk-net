namespace Virgil.SDK.Exceptions
{
    /// <summary>
    ///     Private service exception
    /// </summary>
    /// <seealso cref="VirgilServiceException" />
    public class VirgilServicePrivateServicesException : VirgilServiceException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VirgilServicePrivateServicesException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public VirgilServicePrivateServicesException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}