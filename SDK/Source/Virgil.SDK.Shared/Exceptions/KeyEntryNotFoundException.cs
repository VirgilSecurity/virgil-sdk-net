namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an key pair is not found.
    /// </summary>
    public class KeyEntryNotFoundException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEntryNotFoundException"/> class.
        /// </summary>
        public KeyEntryNotFoundException() : base(Localization.ExceptionKeyPairNotFound)
        {
        }
    }
}