namespace Virgil.SDK
{
    using System;

    using Virgil.SDK.Clients;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;

    /// <summary>
    /// Represents all exposed virgil services
    /// </summary>
    public class ServiceHub
    {
        private readonly ServiceHubConfig hubConfig;
        
        private readonly Lazy<IPrivateKeysServiceClient> privateKeysClient;
        private readonly Lazy<ICardsServiceClient> cardsClient;
        private readonly Lazy<IIdentityServiceClient> identityClient;

        private IServiceKeyCache keysCache;
        private PublicServiceConnection publicServiceConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHub"/> class.
        /// </summary>
        private ServiceHub(ServiceHubConfig hubConfig)
        {
            this.hubConfig = hubConfig;
            
            this.privateKeysClient = new Lazy<IPrivateKeysServiceClient>(this.BuildPrivateKeysClient);
            this.cardsClient = new Lazy<ICardsServiceClient>(this.BuildCardsClient);
            this.identityClient = new Lazy<IIdentityServiceClient>(this.BuildIdentityClient);
        }
        
        /// <summary>
        /// Gets a client that handle requests for <c>PrivateKey</c> resources.
        /// </summary>
        public IPrivateKeysServiceClient PrivateKeys => this.privateKeysClient.Value;

        /// <summary>
        /// Gets a client that handle requests for <c>Card</c> resources.
        /// </summary>
        public ICardsServiceClient Cards => this.cardsClient.Value;

        /// <summary>
        /// Gets a client that handle requests Identity service resources.
        /// </summary>
        public IIdentityServiceClient Identity => this.identityClient.Value;
        
        /// <summary>
        /// Creates new <see cref="ServiceHub"/> instances with default configuration 
        /// for specified access token.
        /// </summary>
        public static ServiceHub Create(string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(accessToken, nameof(accessToken));
            return Create(ServiceHubConfig.UseAccessToken(accessToken));
        }

        /// <summary>
        /// Creates new <see cref="ServiceHub"/> instances with default configuration 
        /// for specified configuration.
        /// </summary>
        public static ServiceHub Create(ServiceHubConfig hubConfig)
        {
            var services = new ServiceHub(hubConfig);
            services.Initialize();

            return services;
        }

        /// <summary>
        /// Initializes an implementation of the ServiceHub class.
        /// </summary>
        private void Initialize()
        {
            // initialize Public Keys service connection first.  

            this.publicServiceConnection = new PublicServiceConnection(this.hubConfig.AccessToken, this.hubConfig.PublicServiceAddress);
            this.keysCache = new DynamicKeyCache(this.publicServiceConnection);
        }

        /// <summary>
        /// Builds a Private Key client instance.
        /// </summary>
        private IPrivateKeysServiceClient BuildPrivateKeysClient()
        {
            var connection = new PrivateKeysConnection(this.hubConfig.AccessToken, this.hubConfig.PrivateServiceAddress);
            var client = new PrivateKeysServiceClient(connection, this.keysCache);

            return client;
        }

        /// <summary>
        /// Builds a Cards client instance.
        /// </summary>
        private ICardsServiceClient BuildCardsClient()
        {
            var client = new CardsServiceClient(this.publicServiceConnection, this.keysCache);
            return client;
        }

        /// <summary>
        /// Builds a Identity Service client instance.
        /// </summary>
        private IIdentityServiceClient BuildIdentityClient()
        {
            var connection = new IdentityConnection(this.hubConfig.IdentityServiceAddress);
            var client = new IdentityServiceClient(connection, this.keysCache);

            return client;
        }
    }
}