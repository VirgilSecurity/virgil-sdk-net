namespace Virgil.SDK.PrivateKeys.Exceptions
{
    using System;

    /// <summary>
    /// Represents errors that occur during using <see cref="KeyringClient"/> client.
    /// </summary>
    public class PrivateKeysException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysException"/> class.
        /// </summary>
        public PrivateKeysException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PrivateKeysException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PrivateKeysException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}