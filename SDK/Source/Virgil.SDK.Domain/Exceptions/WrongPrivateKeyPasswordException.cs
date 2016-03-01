namespace Virgil.SDK.Domain.Exceptions
{
    using SDK.Exceptions;
    public class WrongPrivateKeyPasswordException : VirgilException
    {
        public WrongPrivateKeyPasswordException(string message) : base(message)
        {
        }
    }
}