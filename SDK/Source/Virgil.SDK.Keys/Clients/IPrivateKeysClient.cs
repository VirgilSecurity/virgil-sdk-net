namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;
    using TransferObject;

    /// <summary>
    ///     Provides common methods to interact with Private Keys resource endpoints.
    /// </summary>
    public interface IPrivateKeysClient : IVirgilService
    {
        /// <summary>
        ///     Uploads private key to private key store.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        Task Put(Guid virgilCardId, byte[] privateKey);

        /// <summary>
        ///     Downloads private part of key by its public id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="token"></param>
        /// <remarks>Random password will be generated to encrypt server response</remarks>
        Task<GrabResponse> Get(Guid virgilCardId, IndentityTokenDto token);

        /// <summary>
        ///     Downloads private part of key by its public id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="token"></param>
        /// <param name="responsePassword"></param>
        Task<GrabResponse> Get(Guid virgilCardId, IndentityTokenDto token, string responsePassword);

        /// <summary>
        ///     Deletes private key by its id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        Task Delete(Guid virgilCardId, byte[] privateKey);
    }
}