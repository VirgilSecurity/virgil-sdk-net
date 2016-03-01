namespace Virgil.SDK.Domain.Exceptions
{
    using SDK.Exceptions;

    public class CardNotFoundException : VirgilException
    {
        public CardNotFoundException(string message) : base(message)
        {
        }
    }
}