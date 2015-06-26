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
            var connection = new Connection(appToken, new Uri(@"https://keys.virgilsecurity.com/v1/"));
            Accounts = new AccountsClient(connection);
            PublicKeys = new PublicKeysClient(connection);
            UserData = new UserDataClient(connection);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeysClient" /> class.
        /// </summary>
        /// <param name="accounts">The accounts client.</param>
        /// <param name="publicKeys">The public keys client.</param>
        /// <param name="userData">The user data client.</param>
        public KeysClient(
            IAccountsClient accounts,
            IPublicKeysClient publicKeys,
            IUserDataClient userData)
        {
            Accounts = accounts;
            PublicKeys = publicKeys;
            UserData = userData;
        }

        /// <summary>
        /// Gets the accounts client implementation.
        /// </summary>
        /// <value>
        /// The accounts client.
        /// </value>
        public IAccountsClient Accounts { get; private set; }

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