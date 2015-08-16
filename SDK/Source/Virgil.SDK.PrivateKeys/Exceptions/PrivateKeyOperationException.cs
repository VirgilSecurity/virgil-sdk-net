namespace Virgil.SDK.PrivateKeys.Exceptions
{
    using System.Net;

    public class PrivateKeyOperationException : PrivateKeysServiceException
    {
        private PrivateKeyOperationException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorCode, errorMessage, statusCode, content)
        {
            this.ErrorType = (PrivateKeyErrorType)errorCode;
        }

        public PrivateKeyErrorType ErrorType { get; private set; }

        internal static PrivateKeyOperationException Create(int errorCode, HttpStatusCode statusCode, string content)
        {
            var message = "";
            switch (errorCode)
            {
                case 50001: message = Localization.ExceptionPublicKeyIdInvalid; break;
                case 50002: message = Localization.ExceptionPublicKeyIdNotFound; break;
                case 50003: message = Localization.ExceptionPrivateKeyAlreadyExists; break;
                case 50004: message = Localization.ExceptionPrivateKeyInvalid; break;
                case 50005: message = Localization.ExceptionPrivateKeyInvalidBase64; break;
            }

            var exception = new PrivateKeyOperationException(errorCode, message, statusCode, content);
            return exception;
        }
    }
}