namespace Virgil.SDK.Keys.Exceptions
{
    using System.Net;

    public class KeysServiceAuthException : KeysServiceException
    {
        public KeysServiceAuthException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) 
            : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }
}