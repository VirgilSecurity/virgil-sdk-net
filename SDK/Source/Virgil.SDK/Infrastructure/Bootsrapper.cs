namespace Virgil.SDK.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using Virgil.SDK.Clients;
    using Virgil.SDK.Http;

    /// <summary>
    /// Represents exposed virgil services bootsrap entry point
    /// </summary>
    public class Bootsrapper
    {
        private readonly ApiConfig apiConfig;

        private readonly Dictionary<Type, object> customInstances = new Dictionary<Type, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootsrapper"/> class.
        /// </summary>
        /// <param name="apiConfig">The API configuration.</param>
        public Bootsrapper(ApiConfig apiConfig)
        {
            this.apiConfig = apiConfig;
        }

        /// <summary>
        /// Withes the custom service instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceInstance">The service instance.</param>
        /// <returns><see cref="Bootsrapper"/> instance.</returns>
        public Bootsrapper WithCustomServiceInstance<T>(T serviceInstance) where T : IVirgilService
        {
            this.customInstances[typeof (T)] = serviceInstance;
            return this;
        }

        /// <summary>
        /// Builds all service instances.
        /// </summary>
        /// <returns><see cref="VirgilHub"/> instance.</returns>
        public VirgilHub Build()
        {
            var publicServicesConnection = new PublicServicesConnection(this.apiConfig.AccessToken, this.apiConfig.PublicServicesUri);

            var virgilCardClient = this.GetService<IVirgilCardsClient>() ?? new VirgilCardsClient(publicServicesConnection);

            var keyCache = this.GetService<IServiceKeyCache>() ?? new StaticKeyCache();

            var publicKeysClient = this.GetService<IPublicKeysClient>() ?? new PublicKeysClient(publicServicesConnection, keyCache);

            var privateKeysClient = this.GetService<IPrivateKeysClient>() ?? 
                new PrivateKeysClient(new PrivateKeysConnection(this.apiConfig.AccessToken, this.apiConfig.PrivateServicesUri), keyCache);
            
            var identityService = this.GetService<IIdentityClient>() ?? new IdentityClient(new IdentityConnection(this.apiConfig.IdentityServiceUri), keyCache);

            var services = new VirgilHub
            {
                Identity = identityService,
                PublicKeys = publicKeysClient,
                Cards = virgilCardClient,
                PrivateKeys = privateKeysClient
            };

            ServiceLocator.Services = services;

            return services;
        }

        /// <summary>
        /// Initiates services configuration with provided access token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns><see cref="ApiConfig"/> instance</returns>
        public static ApiConfig UseAccessToken(string accessToken)
        {
            return new ApiConfig(accessToken);
        }

        private T GetService<T>()
        {
            object value = null;
            this.customInstances.TryGetValue(typeof (T), out value);
            return (T) value;
        }
    }
}