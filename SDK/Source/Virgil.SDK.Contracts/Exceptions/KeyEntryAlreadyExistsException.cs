namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an key is already exists.
    /// </summary>
    public class KeyEntryAlreadyExistsException : KeyStorageException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEntryAlreadyExistsException"/> class.
        /// </summary>
        public KeyEntryAlreadyExistsException() : base("Key with specified name is already exists.")
        {
        }
    }
}