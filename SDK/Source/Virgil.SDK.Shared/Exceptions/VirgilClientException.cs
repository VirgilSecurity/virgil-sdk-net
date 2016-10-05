namespace Virgil.SDK.Exceptions
{
    /// <summary>
    ///     Public service exception
    /// </summary>
    /// <seealso cref="VirgilServiceException" />
    public class VirgilClientException : VirgilServiceException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VirgilClientException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public VirgilClientException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}