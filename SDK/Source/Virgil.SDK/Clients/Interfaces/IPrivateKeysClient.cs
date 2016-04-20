namespace Virgil.SDK.Clients
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Private Keys resource endpoints.
    /// </summary>
    public interface IPrivateKeysClient : IVirgilService
    {
        /// <summary>
        /// Uploads private key to private key store.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        Task Stash(Guid virgilCardId, byte[] privateKey, string privateKeyPassword = null);

        /// <summary>
        /// Downloads private part of key by its public id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="token"></param>
        /// <remarks>Random password will be generated to encrypt server response</remarks>
        Task<GrabResponse> Get(Guid virgilCardId, IdentityTokenDto token);

        /// <summary>
        /// Downloads private part of key by its public id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="token"></param>
        /// <param name="responsePassword"></param>
        Task<GrabResponse> Get(Guid virgilCardId, IdentityTokenDto token, string responsePassword);

        /// <summary>
        /// Deletes the private key from service by specified card ID.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        Task Destroy(Guid virgilCardId, byte[] privateKey, string privateKeyPassword = null);
    }
}