namespace Virgil.SDK.Exceptions
{
    public class VirgilKeyIsAlreadyExistsException : VirgilException
    {
        public VirgilKeyIsAlreadyExistsException() : base("Virgil Key is already exists.")
        {
        }
    }

    public class VirgilKeyIsNotFoundException : VirgilException
    {
        public VirgilKeyIsNotFoundException() : base("Virgil Key is not found.")
        {
        }
    }
}