namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class VerificationRequestIsNotSentServiceException : VirgilServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerificationRequestIsNotSentServiceException"/> class.
        /// </summary>
        public VerificationRequestIsNotSentServiceException() : base(Localization.ExceptionIdentityVerificationIsNotSent)
        {
        }
    }
}