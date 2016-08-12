namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an key pair is not found.
    /// </summary>
    public class KeyPairNotFoundException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPairNotFoundException"/> class.
        /// </summary>
        public KeyPairNotFoundException() : base(Localization.ExceptionKeyPairNotFound)
        {
        }
    }
}