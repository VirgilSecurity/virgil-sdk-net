namespace Virgil.SDK.PrivateKeys
{
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Clients;

    /// <summary>
    /// Provides clients for management private keys.
    /// </summary>
    public interface IKeyringClient
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        IConnection Connection { get; }

        /// <summary>
        /// Gets the accounts resource endpoint client.
        /// </summary>
        IContainerClient Accounts { get; }

        /// <summary>
        /// Gets the private keys resource endpoint client.
        /// </summary>
        IPrivateKeysClient PrivateKeys { get; }
    }
}