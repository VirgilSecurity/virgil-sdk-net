namespace Virgil.SDK.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public interface IPublicKeysClient : IVirgilService
    {
        /// <summary>
        /// Gets the specified public key by it identifier.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns>Public key dto</returns>
        Task<PublicKeyDto> Get(Guid publicKeyId);

        /// <summary>
        /// Gets the specified public key by it identifier with extended data.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="virgilCardId">The virgil card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns>List of virgil cards.</returns>
        Task<IEnumerable<VirgilCardDto>> GetExtended(Guid publicKeyId, Guid virgilCardId, byte[] privateKey, string privateKeyPassword = null);

        /// <summary>
        /// Revoke a  Public Key  endpoint. To revoke the  Public Key  it's mandatory to pass validation tokens obtained on  Virgil Identity  service for all confirmed  Virgil Cards  for this  Public Key .
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="tokens">The identity tokens.</param>
        /// <param name="virgilCardId">The virgil card identifier.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        Task Revoke(
            Guid publicKeyId, 
            IEnumerable<IdentityTokenDto> tokens, 
            Guid virgilCardId,
            byte[] privateKey,
            string privateKeyPassword = null);
    }
}