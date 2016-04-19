namespace Virgil.SDK
{
    using Common;

    using Virgil.SDK.Clients;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;

    /// <summary>
    /// Represents all exposed virgil services
    /// </summary>
    public class ServiceHub
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHub"/> class.
        /// </summary>
        internal ServiceHub()
        {
        }

        /// <summary>
        /// Gets a client that handle requests for <c>PublicKey</c> resources.
        /// </summary>
        public IPublicKeysClient PublicKeys { get; internal set; }

        /// <summary>
        /// Gets a client that handle requests for <c>PrivateKey</c> resources.
        /// </summary>
        public IPrivateKeysClient PrivateKeys { get; internal set; }

        /// <summary>
        /// Gets a client that handle requests for <c>Card</c> resources.
        /// </summary>
        public ICardsClient Cards { get; internal set; }

        /// <summary>
        /// Creates new <see cref="ServiceHub"/> instances with default configuration 
        /// for specified access token.
        /// </summary>
        public static ServiceHub Create(string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(accessToken, nameof(accessToken));
            return Create(ServiceConfig.WithAccessToken(accessToken));
        }

        /// <summary>
        /// Creates new <see cref="ServiceHub"/> instances with default configuration 
        /// for specified configuration.
        /// </summary>
        public static ServiceHub Create(ServiceConfig config)
        {
            var publicServiceConnection = new PublicServicesConnection(config.AccessToken, config.PublicServicesAddress);
            var privateServiceConnection = new PrivateKeysConnection(config.AccessToken, config.PrivateServicesAddress);

            var keyCache = new DynamicKeyCache(publicServiceConnection);

            var cardsClient = new CardsClient(publicServiceConnection, keyCache);
            var publicKeysClient = new PublicKeysClient(publicServiceConnection, keyCache);
            var privateKeysClient = new PrivateKeysClient(privateServiceConnection, keyCache);

            var services = new ServiceHub
            {
                PublicKeys = publicKeysClient,
                Cards = cardsClient,
                PrivateKeys = privateKeysClient
            };

            return services;
        }
    }
}