namespace Virgil.SDK.Exceptions
{
    public class TypeIsAlreadyRegistered : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityServiceServiceException"/> class.
        /// </summary>
        public TypeIsAlreadyRegistered() : base("Type is already registered")
        {
        }
    }
}