namespace Virgil.SDK.PrivateKeys.Exceptions
{
    using System.Net;

    public class AuthenticationException : PrivateKeysServiceException
    {
        private AuthenticationException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorCode, errorMessage, statusCode, content)
        {
            this.ErrorType = (AuthenticationErrorType)errorCode;
        }

        public AuthenticationErrorType ErrorType { get; private set; }

        internal static AuthenticationException Create(int errorCode, HttpStatusCode statusCode, string content)
        {
            var message = "";
            switch (errorCode)
            {
                case 20001: message = Localization.ExceptionAuthenticationCredentialsInvalid; break;
                case 20002: message = Localization.ExceptionAuthenticationCredentialsInvalid; break;
                case 20003: message = Localization.ExceptionAuthenticationContainerNotFound; break;
                case 20004: message = Localization.ExceptionAuthenticationTokenNotValid; break;
                case 20005: message = Localization.ExceptionAuthenticationTokenNotFound; break;
                case 20006: message = Localization.ExceptionAuthenticationTokenHasExpired; break;
            }

            var exception = new AuthenticationException(errorCode, message, statusCode, content);
            return exception;
        }
    }
}