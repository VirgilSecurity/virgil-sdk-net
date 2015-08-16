namespace Virgil.SDK.PrivateKeys.Exceptions
{
    using System.Net;

    public class ApplicationTokenInvalidExcepton : PrivateKeysServiceException
    {
        public ApplicationTokenInvalidExcepton(int errorCode, string content)
            : base(errorCode, Localization.ExceptionApplicationTokenInvalid, HttpStatusCode.BadRequest, content)
        {
        }
    }
}