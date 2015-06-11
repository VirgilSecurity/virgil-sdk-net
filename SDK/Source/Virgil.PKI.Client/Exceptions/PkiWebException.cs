namespace Virgil.PKI.Exceptions
{
    using System;
    using System.Net;

    /// <summary>
    /// Base class for all Public Keys Api service exceptions
    /// </summary>
    public class PkiWebException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PkiWebException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public PkiWebException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : base(errorMessage)
        {
            this.ErrorCode = errorCode;
            this.StatusCode = statusCode;
            this.Content = content;
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