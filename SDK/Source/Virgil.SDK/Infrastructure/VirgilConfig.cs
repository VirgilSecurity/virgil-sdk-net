namespace Virgil.SDK.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using Http;
    using Virgil.SDK.Clients;
    using Virgil.SDK.Helpers;

    /// <summary>
    /// Api endpoint configuration
    /// </summary>
    public class VirgilConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilConfig"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        private VirgilConfig(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        internal string AccessToken { get; }

        /// <summary>
        /// The public services address
        /// </summary>
        internal const string PublicServicesAddress = @"https://keys.virgilsecurity.com";

        /// <summary>
        /// The private services address
        /// </summary>
        internal const string PrivateServicesAddress = @"https://private-keys.virgilsecurity.com";

        /// <summary>
        /// The identity service address
        /// </summary>
        internal const string IdentityServiceAddress = @"https://identity.virgilsecurity.com";

        /// <summary>
        /// Gets the public services URI.
        /// </summary>
        /// <value>
        /// The public services URI.
        /// </value>
        internal Uri PublicServicesUri { get; private set; } = new Uri(PublicServicesAddress);

        /// <summary>
        /// Gets the private services URI.
        /// </summary>
        /// <value>
        /// The private services URI.
        /// </value>
        internal Uri PrivateServicesUri { get; private set; } = new Uri(PrivateServicesAddress);

        /// <summary>
        /// Gets the identity service URI.
        /// </summary>
        /// <value>
        /// The identity service URI.
        /// </value>
        internal Uri IdentityServiceUri { get; private set; } = new Uri(IdentityServiceAddress);

        /// <summary>
        /// Sets staging endpoint
        /// </summary>
        /// <returns></returns>
        internal VirgilConfig WithStagingEndpoints()
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
        public VirgilConfig WithCustomPublicServiceUri(Uri publicServicesUri)
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
        public VirgilConfig WithCustomPrivateServiceUri(Uri privateServicesUri)
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
        public VirgilConfig WithCustomIdentityServiceUri(Uri identityServiceUri)
        {
            Ensure.ArgumentNotNull(identityServiceUri, nameof(identityServiceUri));
            this.IdentityServiceUri = identityServiceUri;
            return this;
        }

        private readonly Dictionary<Type, object> customInstances = new Dictionary<Type, object>();

        /// <summary>
        /// Withes the custom service instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceInstance">The service instance.</param>
        /// <returns><see cref="VirgilConfig"/> instance.</returns>
        public VirgilConfig WithCustomServiceInstance<T>(T serviceInstance) where T : IVirgilService
        {
            this.customInstances[typeof(T)] = serviceInstance;
            return this;
        }

        /// <summary>
        /// Builds all service instances.
        /// </summary>
        /// <returns><see cref="VirgilHub"/> instance.</returns>
        public VirgilHub Build()
        {
            var publicServicesConnection = new PublicServicesConnection(this.AccessToken, this.PublicServicesUri);

            var keyCache = this.GetService<IServiceKeyCache>() ?? new DynamicKeyCache(publicServicesConnection);// new StaticKeyCache();

            var virgilCardClient = this.GetService<IVirgilCardsClient>() ?? new VirgilCardsClient(publicServicesConnection, keyCache);
            var publicKeysClient = this.GetService<IPublicKeysClient>() ?? new PublicKeysClient(publicServicesConnection, keyCache);

            var privateKeysClient = this.GetService<IPrivateKeysClient>() ??
                new PrivateKeysClient(new PrivateKeysConnection(this.AccessToken, this.PrivateServicesUri), keyCache);

            var identityService = this.GetService<IIdentityClient>() ?? new IdentityClient(new IdentityConnection(this.IdentityServiceUri), keyCache);

            var services = new VirgilHub
            {
                Identity = identityService,
                PublicKeys = publicKeysClient,
                Cards = virgilCardClient,
                PrivateKeys = privateKeysClient
            };

            return services;
        }

        /// <summary>
        /// Initiates services configuration with provided access token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns><see cref="VirgilConfig"/> instance</returns>
        public static VirgilConfig UseAccessToken(string accessToken)
        {
            return new VirgilConfig(accessToken);
        }

        private T GetService<T>()
        {
            object value = null;
            this.customInstances.TryGetValue(typeof(T), out value);
            return (T)value;
        }
    }
}