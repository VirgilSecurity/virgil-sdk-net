namespace Virgil.SDK.PublicKeys
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Virgil.SDK.Common;
    using Virgil.SDK.Identities;
    using Virgil.SDK.Models;

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
        Task<PublicKeyModel> Get(Guid publicKeyId);

        /// <summary>
        /// Revoke a  Public Key  endpoint. To revoke the  Public Key  it's mandatory to pass validation tokens obtained on  Virgil Identity  service for all confirmed  Virgil Cards  for this  Public Key .
        /// </summary>
        /// <param name="publicKeyId">The public key ID to be revoked.</param>
        /// <param name="identityInfos">The list of confirmed identities with associated with this public key.</param>
        /// <param name="cardId">The private/public keys associated card identifier</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="privateKeyPassword">The private key password</param>
        Task Revoke(Guid publicKeyId, IEnumerable<IdentityInfo> identityInfos, Guid cardId, byte[] privateKey, string privateKeyPassword = null);
    }
}