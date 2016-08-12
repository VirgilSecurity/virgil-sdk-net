namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an key pair parameters type is not supported.
    /// </summary>
    public class KeyPairParametersTypeInvalidException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPairParametersTypeInvalidException"/> class.
        /// </summary>
        public KeyPairParametersTypeInvalidException() : base(Localization.ExceptionKeyPairParametersIsNotValid)
        {
        }
    }
}