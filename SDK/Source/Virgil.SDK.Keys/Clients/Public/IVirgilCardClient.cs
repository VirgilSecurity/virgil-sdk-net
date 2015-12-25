namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TransferObject;

    public interface IVirgilCardClient : IVirgilService
    {
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
        /// Creates a new Virgil Card attached to known public key with unconfirmed ientity.
        /// </summary>
        /// <param name="value">The value of identity.</param>
        /// <param name="type">The type of virgil card.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="customData">The custom data.</param>
        /// <remarks>This card will not be searchable by default.</remarks>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        Task<VirgilCardDto> Create(
            string value,
            IdentityType type,
            Guid publicKeyId,
            byte[] privateKey,
            Dictionary<string, string> customData = null);

        /// <summary>
        /// Creates a new Virgil Card with unconfirmed identity.
        /// </summary>
        /// <param name="value">The value of identity.</param>
        /// <param name="type">The type of virgil card.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="customData">The custom data.</param>
        /// <remarks>This card will not be searchable by default.</remarks>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        Task<VirgilCardDto> Create(
            string value,
            IdentityType type,
            byte[] publicKey,
            byte[] privateKey,
            Dictionary<string, string> customData = null);

        /// <summary>
        /// Creates a new Virgil Card attached to known public key with confirmed ientity.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        Task<VirgilCardDto> Create(
            IndentityTokenDto token,
            Guid publicKeyId,
            byte[] privateKey,
            Dictionary<string, string> customData = null);

        /// <summary>
        /// Creates a new Virgil Card with confirmed identity.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        Task<VirgilCardDto> Create(
            IndentityTokenDto token,
            byte[] publicKey,
            byte[] privateKey,
            Dictionary<string, string> customData = null);

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