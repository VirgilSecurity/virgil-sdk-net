namespace Virgil.SDK.Clients
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.Identities;
    using Virgil.SDK.Models;

    /// <summary>
    /// Provides common methods to interact with Private Keys resource endpoints.
    /// </summary>
    public interface IPrivateKeysServiceClient : IVirgilService
    {
        /// <summary>
        /// Uploads private key to private key store.
        /// </summary>
        /// <param name="cardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        Task Stash(Guid cardId, byte[] privateKey, string privateKeyPassword = null);

        /// <summary>
        /// Downloads private part of key by its public id.
        /// </summary>
        /// <param name="cardId">The public key identifier.</param>
        /// <param name="identityInfo"></param>
        /// <remarks>Random password will be generated to encrypt server response</remarks>
        Task<PrivateKeyModel> Get(Guid cardId, IdentityInfo identityInfo);

        /// <summary>
        /// Downloads private part of key by its public id.
        /// </summary>
        /// <param name="cardId">The public key identifier.</param>
        /// <param name="identityInfo"></param>
        /// <param name="responsePassword"></param>
        Task<PrivateKeyModel> Get(Guid cardId, IdentityInfo identityInfo, string responsePassword);

        /// <summary>
        /// Deletes the private key from service by specified card ID.
        /// </summary>
        /// <param name="cardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        Task Destroy(Guid cardId, byte[] privateKey, string privateKeyPassword = null);
    }
}