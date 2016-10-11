namespace Virgil.SDK.Exceptions
{
    public class SignatureIsNotValidException : CryptoException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureIsNotValidException"/> class.
        /// </summary>
        public SignatureIsNotValidException() : base("Digital signature is not valid")
        {
        }
    }
}