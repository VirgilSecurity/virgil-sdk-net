namespace Virgil.SDK
{
    using Common;

    using Virgil.SDK.Clients;

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
            return ServiceConfig.UseAccessToken(accessToken).Build();
        }

        /// <summary>
        /// Creates new <see cref="ServiceHub"/> instances with default configuration 
        /// for specified configuration
        /// </summary>
        public static ServiceHub Create(ServiceConfig config)
        {
            return config.Build();
        }
    }
}