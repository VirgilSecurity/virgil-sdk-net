namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an key pair is already exists.
    /// </summary>
    public class KeyPairAlreadyExistsException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPairAlreadyExistsException"/> class.
        /// </summary>
        public KeyPairAlreadyExistsException() : base(Localization.ExceptionKeyPairAlreadyExistsException)
        {
        }
    }
}