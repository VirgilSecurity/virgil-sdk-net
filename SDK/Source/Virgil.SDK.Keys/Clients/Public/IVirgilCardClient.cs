namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TransferObject;

    public interface IVirgilCardClient
    {
        /// <summary>
        ///     Creates new virgil card.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="type">The type of virgil card.</param>
        /// <param name="value">The value of identity.</param>
        /// <param name="customData">The custom data.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns>Virgil card DTO.</returns>
        Task<VirgilCardDto> Create(
            byte[] publicKey,
            IdentityType type,
            string value,
            Dictionary<string, string> customData,
            byte[] privateKey);

        /// <summary>
        ///     Attaches new virgil card to existing public key
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="type">The type of virgil card.</param>
        /// <param name="value">The value of identity.</param>
        /// <param name="customData">The custom data.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns>Virgil card DTO.</returns>
        Task<VirgilCardDto> CreateAttached(
            Guid publicKeyId,
            IdentityType type,
            string value,
            Dictionary<string, string> customData,
            byte[] privateKey);

        /// <summary>
        ///     Signs virgil card.
        /// </summary>
        /// <param name="signedVirgilCardId">The signed virgil card identifier.</param>
        /// <param name="signedVirgilCardHash">The signed virgil card hash.</param>
        /// <param name="signerVirgilCardId">The signer virgil card identifier.</param>
        /// <param name="signerPrivateKey">
        ///     The signer private key. Private key is used to produce sign. It is not transfered over
        ///     network
        /// </param>
        /// <returns></returns>
        Task<VirgilSignResponse> Sign(
            Guid signedVirgilCardId,
            string signedVirgilCardHash,
            Guid signerVirgilCardId,
            byte[] signerPrivateKey);

        /// <summary>
        ///     Unsigns the specified signed virgil card identifier.
        /// </summary>
        /// <param name="signedVirgilCardId">The signed virgil card identifier.</param>
        /// <param name="signerVirgilCardId">The signer virgil card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        Task Unsign(
            Guid signedVirgilCardId,
            Guid signerVirgilCardId,
            byte[] privateKey);

        /// <summary>
        ///     Searches the specified value.
        /// </summary>
        /// <param name="value">The value of identifier. Required.</param>
        /// <param name="type">The type of identifier. Optional.</param>
        /// <param name="relations">Relations between Virgil cards. Optional</param>
        /// <param name="includeUnconfirmed">Unconfirmed Virgil cards will be included in output. Optional</param>
        /// <returns>List of virgil card dtos</returns>
        Task<List<VirgilCardDto>> Search(
            string value,
            IdentityType? type,
            IEnumerable<Guid> relations,
            bool? includeUnconfirmed);
    }
}