namespace Virgil.SDK.Keys.Exceptions
{
    using System.Net;

    /// <summary>
    /// The exception that is thrown when server returns request exception
    /// </summary>
    public class KeysServiceRequestException : KeysServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeysServiceRequestException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public KeysServiceRequestException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeysServiceRequestException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public KeysServiceRequestException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }
}