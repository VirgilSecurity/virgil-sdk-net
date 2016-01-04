namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    /// <seealso cref="IVirgilService" />
    public interface IVirgilCardsClient : IVirgilService
    {
        /// <summary>
        /// Creates a new Virgil Card attached to known public key with unconfirmed identity.
        /// </summary>
        /// <param name="identityValue">The string that represents the value of identity.</param>
        /// <param name="identityType">The type of identity.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="cardsHash">The collection of hashes of card that need to trust.</param>
        /// <param name="customData">The collection of custom user information.</param>
        /// <returns>An instance of <see cref="VirgilCardDto" /></returns>
        /// <remarks>This card will not be searchable by default.</remarks>
        Task<VirgilCardDto> Create
        (
            string identityValue,
            IdentityType identityType,
            Guid publicKeyId,
            byte[] privateKey,
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHash = null,
            IDictionary<string, string> customData = null
        );

        /// <summary>
        /// Creates a new Virgil Card with unconfirmed identity.
        /// </summary>
        /// <param name="identityValue">The value of identity.</param>
        /// <param name="identityType">The type of virgil card.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="cardsHash">The collection of hashes of card that need to trust.</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto" /></returns>
        /// <remarks>This card will not be searchable by default.</remarks>
        Task<VirgilCardDto> Create
        (
            string identityValue,
            IdentityType identityType,
            byte[] publicKey,
            byte[] privateKey,
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHash = null,
            IDictionary<string, string> customData = null
        );

        /// <summary>
        /// Creates a new Virgil Card attached to known public key with confirmed identity.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="cardsHash">The collection of hashes of card that need to trust.</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto" /></returns>
        Task<VirgilCardDto> Create
        (
            IndentityTokenDto token,
            Guid publicKeyId,
            byte[] privateKey,
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHash = null,
            IDictionary<string, string> customData = null
        );

        /// <summary>
        /// Creates a new Virgil Card with confirmed identity and specified public key.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="cardsHash">The collection of hashes of card that need to trust.</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        Task<VirgilCardDto> Create
        (
            IndentityTokenDto token,
            byte[] publicKey,
            byte[] privateKey,
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHash = null,
            IDictionary<string, string> customData = null
        );

        /// <summary>
        /// Searches the cards by specified criteria.
        /// </summary>
        /// <param name="value">The value of identifier. Required.</param>
        /// <param name="type">The type of identifier. Optional.</param>
        /// <param name="relations">Relations between Virgil cards. Optional</param>
        /// <param name="includeUnconfirmed">Unconfirmed Virgil cards will be included in output. Optional</param>
        /// <returns>The collection of Virgil Cards.</returns>
        Task<IEnumerable<VirgilCardDto>> Search
        (
            string value,
            IdentityType? type = null,
            IEnumerable<Guid> relations = null,
            bool? includeUnconfirmed = null
        );

        /// <summary>
        /// Trusts the specified card by signing the card's Hash attribute.
        /// </summary>
        /// <param name="trustedCardId">The trusting Virgil Card.</param>
        /// <param name="trustedCardHash">The trusting Virgil Card Hash value.</param>
        /// <param name="ownerCardId">The signer virgil card identifier.</param>
        /// <param name="privateKey">The signer private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        Task<TrustCardResponse> Trust
        (
            Guid trustedCardId, 
            string trustedCardHash, 
            Guid ownerCardId, 
            byte[] privateKey,
            string privateKeyPassword = null
        );

        /// <summary>
        /// Stops trusting the specified card by deleting the sign digest.
        /// </summary>
        /// <param name="trustedCardId">The trusting Virgil Card.</param>
        /// <param name="ownerCardId">The owner Virgil Card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        Task Untrust
        (
            Guid trustedCardId,
            Guid ownerCardId,
            byte[] privateKey,
            string privateKeyPassword = null
        );

        /// <summary>
        /// Gets the application card.
        /// </summary>
        /// <param name="applicationIdentity">The application identity.</param>
        /// <returns>Virgil card dto <see cref="VirgilCardDto"/></returns>
        Task<IEnumerable<VirgilCardDto>> GetApplicationCard(string applicationIdentity);

        /// <summary>
        /// Revokes the specified public key.
        /// </summary>
        /// <param name="publicKeyId">Id of public key to revoke.</param>
        /// <param name="tokens">List of all tokens for this public key.</param>
        Task Revoke(Guid publicKeyId, IEnumerable<IndentityTokenDto> tokens);
    }
}