namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    ///     Public keys service exception
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Exceptions.VirgilException" />
    public class VirgilPublicKeysException : VirgilException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VirgilPublicKeysException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public VirgilPublicKeysException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}