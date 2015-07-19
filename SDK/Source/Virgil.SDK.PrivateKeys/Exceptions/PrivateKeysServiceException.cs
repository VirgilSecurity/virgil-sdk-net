namespace Virgil.SDK.PrivateKeys.Exceptions
{
    using System.Net;

    /// <summary>
    /// The exception that is thrown when Private Keys API returned error in response.
    /// </summary>
    public class PrivateKeysServiceException : PrivateKeysException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysServiceException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public PrivateKeysServiceException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorMessage)
        {
            this.ErrorCode = errorCode;
            this.StatusCode = statusCode;
            this.Content = content;
        }

        /// <summary>
        /// Gets the http status code of faulted response.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the error code returned from server.
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Gets the string content representation of the faulted response.
        /// </summary>
        public string Content { get; private set; }
    }
}