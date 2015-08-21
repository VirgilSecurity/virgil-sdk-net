namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Model;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public interface IPublicKeysClient
    {
        /// <summary>
        /// Creates the new public key.
        /// </summary>
        /// <param name="publicKey">The public key value.</param>
        /// <param name="privateKey">The private key value. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <param name="userData">The user data collection</param>
        /// <returns><see cref="PublicKeyExtended"/></returns>
        Task<PublicKeyExtended> Create(byte[] publicKey, byte[] privateKey, IEnumerable<UserData> userData);

        /// <summary>
        /// Creates the new public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <param name="userData">The user data.</param>
        /// <returns><see cref="PublicKeyExtended"/></returns>
        Task<PublicKeyExtended> Create(byte[] publicKey, byte[] privateKey, UserData userData);

        /// <summary>
        /// Updates the specified public key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="newPublicKey">The new public key value.</param>
        /// <param name="newPrivateKey">The new private key value. Private key is not being sent, but used to sign the part of HTTP request body.</param>
        /// <param name="oldPrivateKey">The old private key. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <returns><see cref="PublicKey"/></returns>
        Task<PublicKey> Update(
            Guid publicKeyId,
            byte[] newPublicKey,
            byte[] newPrivateKey,
            byte[] oldPrivateKey);

        /// <summary>
        /// Deletes public key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key valye. Private key is not being sent, but used to sign the HTTP request body.</param>
        Task Delete(Guid publicKeyId, byte[] privateKey);

        /// <summary>
        /// Deletes public key without HTTP request sign by known private key. 
        /// Should be used when private key is lost.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns><see cref="PublicKeyOperationRequest"/></returns>
        Task<PublicKeyOperationRequest> Delete(Guid publicKeyId);

        /// <summary>
        /// Confirms the delete operation.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="comfirmation">The <see cref="PublicKeyOperationComfirmation"/> object.</param>
        Task ConfirmDelete(Guid publicKeyId, PublicKeyOperationComfirmation comfirmation);

        /// <summary>
        /// Gets the PublicKey by its identifier.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns><see cref="PublicKey"/></returns>
        Task<PublicKey> GetById(Guid publicKeyId);

        /// <summary>
        /// Searches PublicKey by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns><see cref="PublicKey"/></returns>
        Task<PublicKey> Search(string userId);

        /// <summary>
        /// Resets the specified old public key with new value.
        /// </summary>
        /// <param name="oldPublicKeyId">The old public key identifier.</param>
        /// <param name="newPublicKey">The new public key value.</param>
        /// <param name="newPrivateKey">The new private key value. Private key is not being sent, but used to sign the HTTP request body</param>
        /// <returns><see cref="PublicKeyOperationRequest"/></returns>
        Task<PublicKeyOperationRequest> Reset(Guid oldPublicKeyId, byte[] newPublicKey, byte[] newPrivateKey);

        /// <summary>
        /// Confirms the reset.
        /// </summary>
        /// <param name="oldPublicKeyId">The old public key identifier.</param>
        /// <param name="newPrivateKey">The new private key value. Private key is not being sent, but used to sign the HTTP request body</param>
        /// <param name="comfirmation">The <see cref="PublicKeyOperationComfirmation"/> object.</param>
        /// <returns><see cref="PublicKey"/></returns>
        Task<PublicKey> ConfirmReset(Guid oldPublicKeyId, byte[] newPrivateKey,  PublicKeyOperationComfirmation comfirmation);

        /// <summary>
        /// Gets extended public key data by signing request with private key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is not being sent, but used to sign the HTTP request body</param>
        /// <returns><see cref="PublicKeyExtended"/></returns>
        Task<PublicKeyExtended> SearchExtended(Guid publicKeyId, byte[] privateKey);
    }
}