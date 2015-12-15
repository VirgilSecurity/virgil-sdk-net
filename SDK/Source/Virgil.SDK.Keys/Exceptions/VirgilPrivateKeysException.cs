namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    /// Private keys service exception
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Exceptions.VirgilException" />
    public class VirgilPrivateKeysException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilPrivateKeysException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public VirgilPrivateKeysException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}