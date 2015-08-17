namespace Virgil.SDK.Keys.Exceptions
{
    using System.Net;

    public class KeysServiceServerException : KeysServiceException
    {
        public KeysServiceServerException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) 
            : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }
}