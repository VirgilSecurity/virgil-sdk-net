namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an key is not found.
    /// </summary>
    public class KeyEntryNotFoundException : KeyStorageException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEntryNotFoundException"/> class.
        /// </summary>
        public KeyEntryNotFoundException() : base("Key with specified name is not found.")
        {
        }
    }
}