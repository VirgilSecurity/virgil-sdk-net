namespace Virgil.SDK.Exceptions
{
    public class TypeNotRegisteredException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeNotRegisteredException"/> class.
        /// </summary>
        public TypeNotRegisteredException(string message) : base(message)
        {
        }
    }
}