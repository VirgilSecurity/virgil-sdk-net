namespace Virgil.SDK.Keys
{
    using System;

    using Clients;
    using Http;

    public class KeysClient : IKeysClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeysClient" /> class with the default implemetations
        /// </summary>
        /// <param name="appToken">The application token to be used for requests authorisation.</param>
        public KeysClient(string appToken)
        {
            if (string.IsNullOrWhiteSpace(appToken)) 
                throw new ArgumentNullException(nameof(appToken));

            var connection = new Connection(appToken, new Uri(@"https://keys.virgilsecurity.com/"));
            PublicKeys = new PublicKeysClient(connection);
            UserData = new UserDataClient(connection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeysClient" /> class with the default implemetations
        /// </summary>
        public KeysClient(IConnection connection)
        {
            if (connection == null) 
                throw new ArgumentNullException(nameof(connection));
            
            PublicKeys = new PublicKeysClient(connection);
            UserData = new UserDataClient(connection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeysClient" /> class.
        /// </summary>
        /// <param name="publicKeys">The public keys client.</param>
        /// <param name="userData">The user data client.</param>
        public KeysClient(
            IPublicKeysClient publicKeys,
            IUserDataClient userData)
        {
            PublicKeys = publicKeys;
            UserData = userData;
        }

        /// <summary>
        /// Gets the public keys client implementation.
        /// </summary>
        /// <value>
        /// The public keys client.
        /// </value>
        public IPublicKeysClient PublicKeys { get; private set; }

        /// <summary>
        /// Gets the user data client implementation.
        /// </summary>
        /// <value>
        /// The user data client.
        /// </value>
        public IUserDataClient UserData { get; private set; }
    }
}