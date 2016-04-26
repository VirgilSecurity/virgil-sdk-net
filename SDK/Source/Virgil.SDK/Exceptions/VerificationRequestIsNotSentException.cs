namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class VerificationRequestIsNotSentException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerificationRequestIsNotSentException"/> class.
        /// </summary>
        public VerificationRequestIsNotSentException() : base(Localization.ExceptionIdentityVerificationIsNotSent)
        {
        }
    }
}