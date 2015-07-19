namespace Virgil.SDK.PrivateKeys
{
    using System;

    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Clients;

    /// <summary>
    /// Provides common methods for sending data to and receiving data from a Private Keys API service.
    /// </summary>
    public class KeyringClient : IKeyringClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyringClient"/> class.
        /// </summary> 
        public KeyringClient()
        {
            this.Connection = new Connection(new Uri("https://keys-private.virgilsecurity.com/v2/"));

            this.Accounts = new PrivateKeysAccountsClient(this.Connection);
            this.PrivateKeys = new PrivateKeysClient(this.Connection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyringClient"/> class.
        /// </summary> 
        /// <param name="connection">The connection.</param>
        public KeyringClient(IConnection connection)
        {
            this.Connection = connection;

            this.Accounts = new PrivateKeysAccountsClient(this.Connection);
            this.PrivateKeys = new PrivateKeysClient(this.Connection);
        }

        public IConnection Connection { get; private set; }

        /// <summary>
        /// Gets the accounts resource endpoint client.
        /// </summary>
        public IPrivateKeysAccountsClient Accounts { get; private set; }

        /// <summary>
        /// Gets the private keys resource endpoint client.
        /// </summary>
        public IPrivateKeysClient PrivateKeys { get; private set; }
    }
}