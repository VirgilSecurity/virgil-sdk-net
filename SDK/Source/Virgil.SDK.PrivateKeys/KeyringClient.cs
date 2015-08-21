namespace Virgil.SDK.PrivateKeys
{
    using System;
    using System.Threading.Tasks;

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
        public KeyringClient(string applicationToken)
        {
            this.Connection = new Connection(applicationToken, new Uri("https://keys-private.virgilsecurity.com"));
            
            this.Container = new ContainerClient(this.Connection);
            this.PrivateKeys = new PrivateKeysClient(this.Connection);
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyringClient"/> class.
        /// </summary> 
        /// <param name="connection">The connection.</param>
        public KeyringClient(IConnection connection)
        {
            this.Connection = connection;

            this.Container = new ContainerClient(this.Connection);
            this.PrivateKeys = new PrivateKeysClient(this.Connection);
        }

        /// <summary>
        /// Gets the private keys connection instncance.
        /// </summary>
        public IConnection Connection { get; }
        
        /// <summary>
        /// Gets the accounts resource endpoint client.
        /// </summary>
        public IContainerClient Container { get; }

        /// <summary>
        /// Gets the private keys resource endpoint client.
        /// </summary>
        public IPrivateKeysClient PrivateKeys { get; }
    }
}