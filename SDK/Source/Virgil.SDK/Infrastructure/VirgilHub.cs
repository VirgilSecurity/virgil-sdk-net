namespace Virgil.SDK.Infrastructure
{
    using Virgil.SDK.Clients;

    /// <summary>
    /// Represents all exposed virgil services
    /// </summary>
    public class VirgilHub
    {
        internal VirgilHub()
        {
        }

        /// <summary>
        /// Creates new Virgil Hub instances with default configuration for specified access token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>New Virgil Hub instance.</returns>
        public static VirgilHub Create(string accessToken)
        {
            return Bootsrapper.UseAccessToken(accessToken).Build();
        }

        /// <summary>
        /// Gets the public keys client.
        /// </summary>
        /// <value>
        /// The public keys.
        /// </value>
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
        /// Gets the identity client.
        /// </summary>
        public IIdentityClient Identity { get; internal set; }
    }
}