namespace Virgil.SDK.Infrastructure
{
    using System;
    using Virgil.SDK.Clients;
    using Virgil.SDK.Helpers;

    /// <summary>
    /// Api endpoint configuration
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConfig"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public ApiConfig(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// The public services address
        /// </summary>
        public const string PublicServicesAddress = @"https://keys.virgilsecurity.com";

        /// <summary>
        /// The private services address
        /// </summary>
        public const string PrivateServicesAddress = @"https://private-keys.virgilsecurity.com";

        /// <summary>
        /// The identity service address
        /// </summary>
        public const string IdentityServiceAddress = @"https://identity.virgilsecurity.com";

        /// <summary>
        /// Gets the public services URI.
        /// </summary>
        /// <value>
        /// The public services URI.
        /// </value>
        public Uri PublicServicesUri { get; private set; } = new Uri(PublicServicesAddress);

        /// <summary>
        /// Gets the private services URI.
        /// </summary>
        /// <value>
        /// The private services URI.
        /// </value>
        public Uri PrivateServicesUri { get; private set; } = new Uri(PrivateServicesAddress);

        /// <summary>
        /// Gets the identity service URI.
        /// </summary>
        /// <value>
        /// The identity service URI.
        /// </value>
        public Uri IdentityServiceUri { get; private set; } = new Uri(IdentityServiceAddress);
        
        internal ApiConfig WithStagingEndpoints()
        {
            this.PublicServicesUri = new Uri(@"https://keys-stg.virgilsecurity.com");
            this.PrivateServicesUri = new Uri(@"https://keys-private-stg.virgilsecurity.com");
            this.IdentityServiceUri = new Uri(@"https://identity-stg.virgilsecurity.com");

            return this;
        }

        /// <summary>
        /// Set custom public services URI.
        /// </summary>
        /// <param name="publicServicesUri">The public services URI.</param>
        /// <returns>Configured instance.</returns>
        public ApiConfig WithCustomPublicServiceUri(Uri publicServicesUri)
        {
            Ensure.ArgumentNotNull(publicServicesUri, nameof(publicServicesUri));
            this.PublicServicesUri = publicServicesUri;
            return this;
        }

        /// <summary>
        /// Set custom private service URI.
        /// </summary>
        /// <param name="privateServicesUri">The private services URI.</param>
        /// <returns>Configured instance.</returns>
        public ApiConfig WithCustomPrivateServiceUri(Uri privateServicesUri)
        {
            Ensure.ArgumentNotNull(privateServicesUri, nameof(privateServicesUri));
            this.PrivateServicesUri = privateServicesUri;
            return this;
        }

        /// <summary>
        /// Set custom identity service URI.
        /// </summary>
        /// <param name="identityServiceUri">The identity service URI.</param>
        /// <returns>Configured instance.</returns>
        public ApiConfig WithCustomIdentityServiceUri(Uri identityServiceUri)
        {
            Ensure.ArgumentNotNull(identityServiceUri, nameof(identityServiceUri));
            this.IdentityServiceUri = identityServiceUri;
            return this;
        }

        /// <summary>
        /// Continue to service configuration.
        /// </summary>
        /// <returns><see cref="Bootsrapper"/> instance</returns>
        public Bootsrapper PrepareServices()
        {
            return new Bootsrapper(this);
        }


        /// <summary>
        /// Finished configuation using default settings.
        /// </summary>
        /// <returns><see cref="VirgilHub"/> instance</returns>
        public VirgilHub Build()
        {
            return new Bootsrapper(this).Build();
        }

        /// <summary>
        /// Adds custom service implementation to the service list
        /// </summary>
        /// <param name="serviceInstance">The service instance to replace default implementation.</param>
        /// <returns><see cref="Bootsrapper"/> instance</returns>
        public Bootsrapper WithCustomServiceInstance<T>(T serviceInstance)
            where T : IVirgilService
        {
            var bootsrapper = new Bootsrapper(this).WithCustomServiceInstance(serviceInstance);
            return bootsrapper;
        }
    }
}