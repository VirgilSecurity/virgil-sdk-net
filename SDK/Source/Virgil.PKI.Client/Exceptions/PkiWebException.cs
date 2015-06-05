namespace Virgil.PKI.Exceptions
{
    using System;
    using System.Net;

    public class PkiWebException : Exception
    {
        public PkiWebException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorMessage)
        {
            this.ErrorCode = errorCode;
            this.StatusCode = statusCode;
            this.Content = content;
        }

        public int ErrorCode { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }

        public string Content { get; private set; }
    }
}