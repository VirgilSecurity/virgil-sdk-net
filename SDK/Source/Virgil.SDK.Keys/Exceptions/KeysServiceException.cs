namespace Virgil.SDK.Keys.Exceptions
{
    using System.Net;

    /// <summary>
    /// The exception that is thrown when Public Keys API returned error in response.
    /// </summary>
    public class KeysServiceException : KeysException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeysServiceException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public KeysServiceException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorMessage)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
            Content = content;
        }

        /// <summary>
        /// Gets the error code returned from server.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Gets the http status code of faulted response.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the string content representation of the faulted response.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; private set; }
    }
}