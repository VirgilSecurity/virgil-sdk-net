namespace Virgil.SDK.Exceptions
{
    public class ServiceNotRegisteredException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotRegisteredException"/> class.
        /// </summary>
        public ServiceNotRegisteredException(string message) : base(message)
        {
        }
    }
}