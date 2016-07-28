namespace Virgil.SDK.Exceptions
{
    using System;

    /// <summary>
    /// Represents errors occurred during interaction with SDK components.
    /// </summary>
    public class VirgilException : Exception
    {
        public VirgilException(string message) : base(message)
        {
        }
    }
}