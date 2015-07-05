namespace Virgil.SDK.PrivateKeys.Http
{
    /// <summary>
    /// A connection credentials for Private Keys API access.
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials" /> class. 
        /// </summary>
        public Credentials(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password { get; private set; }
    }
}