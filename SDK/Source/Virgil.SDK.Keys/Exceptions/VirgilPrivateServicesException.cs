namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    ///     Private service exception
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Exceptions.VirgilException" />
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