namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// Base exception class for all Virgil Services operations
    /// </summary>
    public class VirgilServiceException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilServiceException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public VirgilServiceException(int errorCode, string errorMessage) : base(errorMessage)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilServiceException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public VirgilServiceException(string message) : base(message)
        {
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int ErrorCode { get; }
    }
}