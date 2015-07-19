namespace Virgil.SDK.PrivateKeys.Exceptions
{
    /// <summary>
    /// The exception that is thrown when Private Keys is not found.
    /// </summary>
    public class PrivateKeyNotFoundException : PrivateKeysException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeyNotFoundException"/> class.
        /// </summary>
        public PrivateKeyNotFoundException() : base(Localization.ExceptionPrivateKeyNotFound)
        {
        }
    }
}