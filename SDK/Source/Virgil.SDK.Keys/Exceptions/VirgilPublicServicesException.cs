namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    ///     Public service exception
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Exceptions.VirgilException" />
    public class VirgilPublicServicesException : VirgilException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VirgilPublicServicesException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public VirgilPublicServicesException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}