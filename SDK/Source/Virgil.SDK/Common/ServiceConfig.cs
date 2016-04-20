namespace Virgil.SDK.Common
{
    using System;

    using Virgil.SDK.Helpers;

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

            this.PublicServiceAddress = new Uri(@"https://keys.virgilsecurity.com");
            this.PrivateServiceAddress = new Uri(@"https://keys-private.virgilsecurity.com");
            this.IdentityServiceAddress = new Uri(@"https://identity.virgilsecurity.com");
        }
        
        internal string AccessToken { get; private set; }

        internal Uri PublicServiceAddress { get; private set; }

        internal Uri PrivateServiceAddress { get; private set; }

        internal Uri IdentityServiceAddress { get; private set; }

        /// <summary>
        /// Overrides default Virgil Public Keys service address.  
        /// </summary>
        public ServiceConfig WithPublicServicesAddress(string url)
        {
            Ensure.ArgumentNotNullOrEmptyString(url, nameof(url));
            this.PublicServiceAddress = new Uri(url);

            return this;
        }

        /// <summary>
        /// Overrides default Virgil Private Keys service address.  
        /// </summary>
        public ServiceConfig WithPrivateServicesAddress(string url)
        {
            Ensure.ArgumentNotNullOrEmptyString(url, nameof(url));
            this.PrivateServiceAddress = new Uri(url);

            return this;
        }

        /// <summary>
        /// Overrides default Virgil Identity service address.  
        /// </summary>
        public ServiceConfig WithIdentityServiceAddress(string url)
        {
            Ensure.ArgumentNotNullOrEmptyString(url, nameof(url));
            this.IdentityServiceAddress = new Uri(url);

            return this;
        }

        /// <summary>
        /// Initializes Virgil Securtity services with staging urls.
        /// </summary>
        internal ServiceConfig WithStagingEnvironment()
        {
            this.PublicServiceAddress = new Uri(@"https://keys-stg.virgilsecurity.com");
            this.PrivateServiceAddress = new Uri(@"https://keys-private-stg.virgilsecurity.com");
            this.IdentityServiceAddress = new Uri(@"https://identity-stg.virgilsecurity.com");

            return this;
        }

        /// <summary>
        /// Sets the application token to access to the Virgil Security services. This token has to 
        /// be generated with application private key on Virgil Security portal or manually with SDK Utils.
        /// </summary>
        public static ServiceConfig UseAccessToken(string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(accessToken, nameof(accessToken));
            var serviceConfig = new ServiceConfig(accessToken);

            return serviceConfig;
        }
    }
}