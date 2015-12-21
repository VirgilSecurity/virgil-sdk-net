namespace Virgil.SDK.Keys.Exceptions
{
    using System;

    /// <summary>
    ///     Base exception class for all Virgil Services operations
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class VirgilException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VirgilException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public VirgilException(int errorCode, string errorMessage) : base(errorMessage)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VirgilException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public VirgilException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Gets the error code.
        /// </summary>
        /// <value>
        ///     The error code.
        /// </value>
        public int ErrorCode { get; }
    }
}