namespace Virgil.SDK.PrivateKeys.Exceptions
{
    using System.Net;

    public class RequestSignIsNotValidException : PrivateKeysServiceException
    {
        public RequestSignIsNotValidException(int errorCode, string content)
            : base(errorCode, Localization.ExceptionRequestSignIsNotValid, HttpStatusCode.BadRequest, content)
        {
        }
    }
}