namespace Virgil.SDK.Common
{
    using System;

    using Virgil.SDK.Clients;
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

            this.PublicServicesAddress = new Uri(@"https://keys.virgilsecurity.com");
            this.PrivateServicesAddress = new Uri(@"https://keys-private.virgilsecurity.com");
        }
        
        internal string AccessToken { get; private set; }

        internal Uri PublicServicesAddress { get; private set; }
        
        internal Uri PrivateServicesAddress { get; private set; }

        /// <summary>
        /// Overrides default Virgil Public Keys service address.  
        /// </summary>
        public ServiceConfig WithPublicServicesAddress(string url)
        {
            Ensure.ArgumentNotNullOrEmptyString(url, nameof(url));
            this.PublicServicesAddress = new Uri(url);
            return this;
        }

        /// <summary>
        /// Overrides default Virgil Private Keys service address.  
        /// </summary>
        public ServiceConfig WithPrivateServicesAddress(string url)
        {
            Ensure.ArgumentNotNullOrEmptyString(url, nameof(url));
            this.PrivateServicesAddress = new Uri(url);
            return this;
        }
        
        /// <summary>
        /// Initializes Virgil Securtity services with staging urls.
        /// </summary>
        internal ServiceConfig WithStagingEnvironment()
        {
            this.PublicServicesAddress = new Uri(@"https://keys-stg.virgilsecurity.com");
            this.PrivateServicesAddress = new Uri(@"https://keys-private-stg.virgilsecurity.com");
            return this;
        }

        /// <summary>
        /// Sets the application token to access to the Virgil Security services. This token has to 
        /// be generated with application private key on Virgil Security portal or manually with SDK Utils.
        /// </summary>
        public static ServiceConfig WithAccessToken(string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(accessToken, nameof(accessToken));
            var serviceConfig = new ServiceConfig(accessToken);
            return serviceConfig;
        }
    }
}