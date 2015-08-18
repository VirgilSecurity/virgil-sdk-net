namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.PrivateKeys.Model;

    /// <summary>
    /// Provides common methods to interact with private keys resource endpoints.
    /// </summary>
    public interface IPrivateKeysClient
    {
        /// <summary>
        /// Gets the private key by public key ID.
        /// </summary>
        /// <param name="publicKeyId">Public key identifier.</param>
        /// <returns>The instance of <see cref="PrivateKey"/></returns>
        Task<PrivateKey> Get(Guid publicKeyId);
        
        /// <summary>
        /// Adds new private key for storage.
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        /// <param name="privateKey">The private key associated for this public key. It should be encrypted if
        /// account type is <c>Normal</c></param>
        /// <returns></returns>
        Task Add(Guid publicKeyId, byte[] privateKey);

        /// <summary>
        /// Removes the private key from service by specified public key id.
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        Task Remove(Guid publicKeyId, byte[] sign);
    }
}