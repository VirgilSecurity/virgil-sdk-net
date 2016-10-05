namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an key pair is already exists.
    /// </summary>
    public class KeyEntryAlreadyExistsException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEntryAlreadyExistsException"/> class.
        /// </summary>
        public KeyEntryAlreadyExistsException() : base(Localization.ExceptionKeyPairAlreadyExistsException)
        {
        }
    }
}