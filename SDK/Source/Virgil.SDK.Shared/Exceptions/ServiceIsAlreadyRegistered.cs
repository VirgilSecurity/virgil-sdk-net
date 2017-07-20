namespace Virgil.SDK.Exceptions
{
    public class ServiceIsAlreadyRegistered : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityServiceException"/> class.
        /// </summary>
        public ServiceIsAlreadyRegistered() : base("Type is already registered")
        {
        }
    }
}