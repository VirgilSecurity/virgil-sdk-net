namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// Represents errors occurred during application authentication.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Exceptions.VirgilException" />
    public class AppCredentialsException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCredentialsException"/> class.
        /// </summary>
        public AppCredentialsException() : 
            base("This action requires AppID and AppKey retrieved from development deshboard.")
        {
        }
    }
}
