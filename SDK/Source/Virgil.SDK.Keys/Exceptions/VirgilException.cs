using System;

namespace Virgil.SDK.Keys.Exceptions
{
    public class VirgilException : Exception
    {
        public int ErrorCode { get; }

        public VirgilException(int errorCode, string errorMessage) : base(errorMessage)
        {
            this.ErrorCode = errorCode;
        }
    }
}