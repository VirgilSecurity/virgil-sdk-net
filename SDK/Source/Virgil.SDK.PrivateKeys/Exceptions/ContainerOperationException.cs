namespace Virgil.SDK.PrivateKeys.Exceptions
{
    using System.Net;

    public class ContainerOperationException : PrivateKeysServiceException
    {
        internal ContainerOperationException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) 
            : base(errorCode, errorMessage, statusCode, content)
        {
            this.ErrorType = (ContainerErrorType)errorCode;
        }

        public ContainerErrorType ErrorType { get; private set; }

        internal static ContainerOperationException Create(int errorCode, HttpStatusCode statusCode, string content)
        {
            var message = "";
            switch (errorCode)
            {
                case 40001: message = Localization.ExceptionContainerInvalid; break;
                case 40002: message = Localization.ExceptionContainerNotFound; break;
                case 40003: message = Localization.ExceptionContainerAlreadyExists; break;
                case 40004: message = Localization.ExceptionContainerPasswordNotSpecified; break;
                case 40005: message = Localization.ExceptionContainerPasswordInvalid; break;
                case 40006: message = Localization.ExceptionContainerNotFound; break;
                case 40007: message = Localization.ExceptionContainerTypeInvalid; break;
            }

            var exception = new ContainerOperationException(errorCode, message, statusCode, content);
            return exception;
        }
    }
}