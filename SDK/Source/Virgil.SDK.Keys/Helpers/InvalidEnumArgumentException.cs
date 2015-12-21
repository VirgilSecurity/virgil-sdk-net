namespace Virgil.SDK.Keys.Helpers
{
    using System;

    /// <summary>
    ///     The exception thrown when using invalid arguments that are enumerators.
    /// </summary>
    public class InvalidEnumArgumentException : ArgumentException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class without
        ///     a message.
        /// </summary>
        public InvalidEnumArgumentException()
            : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with
        ///     the specified message.
        /// </summary>
        /// <param name="message">The message to display with this exception. </param>
        public InvalidEnumArgumentException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with
        ///     the specified detailed description and the specified exception.
        /// </summary>
        /// <param name="message">A detailed description of the error.</param>
        /// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
        public InvalidEnumArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with a
        ///     message generated from the argument, the invalid value, and an enumeration class.
        /// </summary>
        /// <param name="argumentName">The name of the argument that caused the exception. </param>
        /// <param name="invalidValue">The value of the argument that failed. </param>
        /// <param name="enumClass">A <see cref="T:System.Type" /> that represents the enumeration class with the valid values. </param>
        public InvalidEnumArgumentException(string argumentName, int invalidValue, Type enumClass)
            : base("InvalidEnumArgument: " + argumentName + " of : " + enumClass.Name)
        {
        }
    }
}