namespace Virgil.SDK
{
    using System;
    
    using Virgil.SDK.Cards;
    using Virgil.SDK.Common;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;
    using Virgil.SDK.Identities;
    using Virgil.SDK.PrivateKeys;
    using Virgil.SDK.PublicKeys;

    /// <summary>
    /// Represents all exposed virgil services
    /// </summary>
    public class ServiceHub
    {
        private readonly ServiceHubConfig hubConfig;

        private readonly Lazy<IPublicKeysClient> publicKeysClient;
        private readonly Lazy<IPrivateKeysClient> privateKeysClient;
        private readonly Lazy<ICardsClient> cardsClient;
        private readonly Lazy<IIdentityClient> identityClient;

        private IServiceKeyCache keysCache;
        private PublicServiceConnection publicServiceConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHub"/> class.
        /// </summary>
        private ServiceHub(ServiceHubConfig hubConfig)
        {
            this.hubConfig = hubConfig;

            this.publicKeysClient = new Lazy<IPublicKeysClient>(this.BuildPublicKeysClient);
            this.privateKeysClient = new Lazy<IPrivateKeysClient>(this.BuildPrivateKeysClient);
            this.cardsClient = new Lazy<ICardsClient>(this.BuildCardsClient);
            this.identityClient = new Lazy<IIdentityClient>(this.BuildIdentityClient);
        }
        
        /// <summary>
        /// Gets a client that handle requests for <c>PublicKey</c> resources.
        /// </summary>
        public IPublicKeysClient PublicKeys => this.publicKeysClient.Value;

        /// <summary>
        /// Gets a client that handle requests for <c>PrivateKey</c> resources.
        /// </summary>
        public IPrivateKeysClient PrivateKeys => this.privateKeysClient.Value;

        /// <summary>
        /// Gets a client that handle requests for <c>Card</c> resources.
        /// </summary>
        public ICardsClient Cards => this.cardsClient.Value;

        /// <summary>
        /// Gets a client that handle requests Identity service resources.
        /// </summary>
        public IIdentityClient Identity => this.identityClient.Value;
        
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
        private IPrivateKeysClient BuildPrivateKeysClient()
        {
            var connection = new PrivateKeysConnection(this.hubConfig.AccessToken, this.hubConfig.PrivateServiceAddress);
            var client = new PrivateKeysClient(connection, this.keysCache);

            return client;
        }

        /// <summary>
        /// Builds a Cards client instance.
        /// </summary>
        private ICardsClient BuildCardsClient()
        {
            var client = new CardsClient(this.publicServiceConnection, this.keysCache);
            return client;
        }

        /// <summary>
        /// Builds a Identity Service client instance.
        /// </summary>
        private IIdentityClient BuildIdentityClient()
        {
            var connection = new IdentityConnection(this.hubConfig.IdentityServiceAddress);
            var client = new IdentityClient(connection, this.keysCache);

            return client;
        }

        /// <summary>
        /// Builds a Public Key client instance.
        /// </summary>
        private IPublicKeysClient BuildPublicKeysClient()
        {
            var client = new PublicKeysClient(this.publicServiceConnection, this.keysCache);
            return client;
        }
    }
}