namespace Virgil.SDK.Keys.Exceptions
{
    using System.Net;

    public class KeysServiceRequestException : KeysServiceException
    {
        public KeysServiceRequestException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }
}