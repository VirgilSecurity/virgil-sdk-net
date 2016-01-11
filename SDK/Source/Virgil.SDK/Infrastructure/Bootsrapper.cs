namespace Virgil.SDK.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using Virgil.SDK.Clients;
    using Virgil.SDK.Http;

    public class Bootsrapper
    {
        private readonly ApiConfig apiConfig;

        public Bootsrapper(ApiConfig apiConfig)
        {
            this.apiConfig = apiConfig;
        }

        private readonly Dictionary<Type, object> customInstances = new Dictionary<Type, object>();

        private T GetService<T>()
        {
            object value = null;
            this.customInstances.TryGetValue(typeof (T), out value);
            return (T) value;
        }
        
        public Bootsrapper WithCustomServiceInstance<T>(T serviceInstance) where T : IVirgilService
        {
            this.customInstances[typeof (T)] = serviceInstance;
            return this;
        }

        public VirgilHub Build()
        {
            var publicServicesConnection = new PublicServicesConnection(this.apiConfig.AccessToken, this.apiConfig.PublicServicesUri);

            var virgilCardClient = this.GetService<IVirgilCardsClient>() ?? new VirgilCardsClient(publicServicesConnection);

            var keyCache = this.GetService<IServiceKeyCache>() ?? new ServiceKeyCache(virgilCardClient);

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

        public static ApiConfig UseAccessToken(string accessToken)
        {
            return new ApiConfig(accessToken);
        }
    }
}