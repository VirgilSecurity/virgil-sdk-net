namespace Virgil.SDK.Exceptions
{
    public class CryptoException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CryptoException(string message) : base(message)
        {
        }
    }
}