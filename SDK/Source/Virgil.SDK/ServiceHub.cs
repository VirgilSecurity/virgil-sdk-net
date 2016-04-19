namespace Virgil.SDK
{
    using Common;

    using Virgil.SDK.Clients;
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
        /// Gets the public keys client.
        /// </summary>
        public IPublicKeysClient PublicKeys { get; internal set; }

        /// <summary>
        /// Gets the private keys client.
        /// </summary>
        public IPrivateKeysClient PrivateKeys { get; internal set; }

        /// <summary>
        /// Gets the Virgil cards client.
        /// </summary>
        public IVirgilCardsClient Cards { get; internal set; }

        /// <summary>
        /// Creates new <see cref="ServiceHub"/> instances with default configuration 
        /// for specified access token.
        /// </summary>
        public static ServiceHub Create(string accessToken)
        {
            return Create(ServiceConfig.UseAccessToken(accessToken));
        }

        /// <summary>
        /// Creates new <see cref="ServiceHub"/> instances with default configuration 
        /// for specified configuration
        /// </summary>
        public static ServiceHub Create(ServiceConfig config)
        {
            var publicServicesConnection = new PublicServicesConnection(config.AccessToken, config.PublicServicesAddress);

            var keyCache = new DynamicKeyCache(publicServicesConnection);

            var cardsClient = new VirgilCardsClient(publicServicesConnection, keyCache);
            var publicKeysClient = new PublicKeysClient(publicServicesConnection, keyCache);
            var privateKeysClient = new PrivateKeysClient(new PrivateKeysConnection(config.AccessToken, config.PrivateServicesAddress), keyCache);

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