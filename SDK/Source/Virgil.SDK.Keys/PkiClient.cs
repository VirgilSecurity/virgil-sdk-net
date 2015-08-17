namespace Virgil.SDK.Keys
{
    using System;

    using Clients;
    using Http;

    public class PkiClient : IPkiClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PkiClient" /> class with the default implemetations
        /// </summary>
        /// <param name="appToken">The application token to be used for requests authorisation.</param>
        public PkiClient(string appToken)
        {
            if (string.IsNullOrWhiteSpace(appToken)) 
                throw new ArgumentNullException(nameof(appToken));

            var connection = new Connection(appToken, new Uri(@"https://keys.virgilsecurity.com/"));
            PublicKeys = new PublicKeysClient(connection);
            UserData = new UserDataClient(connection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PkiClient" /> class with the default implemetations
        /// </summary>
        public PkiClient(IConnection connection)
        {
            if (connection == null) 
                throw new ArgumentNullException(nameof(connection));
            
            PublicKeys = new PublicKeysClient(connection);
            UserData = new UserDataClient(connection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PkiClient" /> class.
        /// </summary>
        /// <param name="publicKeys">The public keys client.</param>
        /// <param name="userData">The user data client.</param>
        public PkiClient(
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