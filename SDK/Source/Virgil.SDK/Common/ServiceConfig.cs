namespace Virgil.SDK.Common
{
    /// <summary>
    /// Represents a configuration file that is applicable to a particular <see cref="ServiceHub"/>. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ServiceConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfig"/> class.
        /// </summary>
        private ServiceConfig(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        internal string AccessToken { get; private set; }

        /// <summary>
        /// Sets the application token to access to the Virgil Security services. This token has to 
        /// be generated on Virgil Security development portal.
        /// </summary>
        public static ServiceConfig UseAccessToken(string accessToken)
        {
            var serviceConfig = new ServiceConfig(accessToken);
            return serviceConfig;
        }
    }
}