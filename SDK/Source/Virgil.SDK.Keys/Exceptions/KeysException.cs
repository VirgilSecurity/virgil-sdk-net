namespace Virgil.SDK.Keys.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class KeysException : Exception
    {
        protected KeysException()
        {
        }

        protected KeysException(string message) : base(message)
        {
        }

        protected KeysException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}