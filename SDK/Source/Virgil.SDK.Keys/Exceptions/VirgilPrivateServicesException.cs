namespace Virgil.SDK.Exceptions
{
    /// <summary>
    ///     Private service exception
    /// </summary>
    /// <seealso cref="VirgilException" />
    public class VirgilPrivateServicesException : VirgilException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VirgilPrivateServicesException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public VirgilPrivateServicesException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}