namespace Virgil.SDK.PrivateKeys.Exceptions
{
    using System.Net;

    public class VerificationException : PrivateKeysServiceException
    {
        private VerificationException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorCode, errorMessage, statusCode, content)
        {
            this.ErrorType = (VerificationErrorType)errorCode;
        }

        public VerificationErrorType ErrorType { get; private set; }

        internal static VerificationException Create(int errorCode, HttpStatusCode statusCode, string content)
        {
            var message = "";
            switch (errorCode)
            {
                case 60001: message = Localization.ExceptionVerificationTokenNotFound; break;
                case 60002: message = Localization.ExceptionVerificationUserIdentityInvalid; break;
                case 60003: message = Localization.ExceptionVerificationContainerNotFound; break;
                case 60004: message = Localization.ExceptionVerificationTokenExpired; break;
            }

            var exception = new VerificationException(errorCode, message, statusCode, content);
            return exception;
        }
    }
}