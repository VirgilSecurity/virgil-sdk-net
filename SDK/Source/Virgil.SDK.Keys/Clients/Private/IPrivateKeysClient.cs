namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides common methods to interact with Private Keys resource endpoints.
    /// </summary>
    public interface IPrivateKeysClient
    {
        /// <summary>
        ///     Uploads private key to private key store.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns></returns>
        Task Put(Guid publicKeyId, byte[] privateKey);

        /// <summary>
        ///     Downloads private part of key by its public id.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns></returns>
        Task Get(Guid publicKeyId);

        /// <summary>
        ///     Deletes private key by its id.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns></returns>
        Task Delete(Guid publicKeyId, byte[] privateKey);
    }
}