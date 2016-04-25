namespace Virgil.SDK.Cards
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
    public interface ICardsClient : IVirgilService
    {
        /// <summary>
        /// Creates a new card with specified identity and existing public key.
        /// </summary>
        /// <param name="identityInfo">The information about identity.</param>
        /// <param name="publicKeyId">The public key identifier in Virgil Services.</param>
        /// <param name="privateKey">
        /// The private key. Private key is used to produce sign. It is not transfered over network
        /// </param>
        /// <param name="privateKeyPassword">
        /// The private key password. Pass this parameter if your private key is encrypted with password</param>
        /// <param name="customData">
        /// The dictionary of key/value pairs with custom values that can be used by different applications
        /// </param>
        Task<CardModel> Create(IdentityInfo identityInfo, Guid publicKeyId, byte[] privateKey, string privateKeyPassword = null, IDictionary<string, string> customData = null);

        /// <summary>
        /// Creates a new card with specified identity and public key.
        /// </summary>
        /// <param name="identityInfo">The information about identity.</param>
        /// <param name="publicKey">The generated public key value.</param>
        /// <param name="privateKey">
        /// The private key. Private key is used to produce sign. It is not transfered over network
        /// </param>
        /// <param name="privateKeyPassword">
        /// The private key password. Pass this parameter if your private key is encrypted with password</param>
        /// <param name="customData">
        /// The dictionary of key/value pairs with custom values that can be used by different applications
        /// </param>
        Task<CardModel> Create(IdentityInfo identityInfo, byte[] publicKey, byte[] privateKey, string privateKeyPassword = null, IDictionary<string, string> customData = null);

        /// <summary>
        /// Performs the search by search criteria.
        /// </summary>
        /// <param name="identityValue">The value of identifier. Required.</param>
        /// <param name="identityType">The type of identifier. Optional.</param>
        /// <param name="relations">
        /// The relations parameter is optional and contains the list of Virgil Cards UDIDs 
        /// to perform the search within
        /// </param>
        /// <param name="includeUnconfirmed">
        /// The request parameter specifies whether an unconfirmed Virgil Cards 
        /// should be included in the search result.
        /// </param>
        /// <returns>The collection of Virgil Cards.</returns>
        Task<IEnumerable<CardModel>> Search(string identityValue, IdentityType? identityType = null, IEnumerable<Guid> relations = null, bool? includeUnconfirmed = null);
        
        /// <summary>
        /// Trusts the specified card by signing the card's Hash attribute.
        /// </summary>
        /// <param name="trustedCardId">The trusting Virgil Card.</param>
        /// <param name="trustedCardHash">The trusting Virgil Card Hash value.</param>
        /// <param name="ownerCardId">The signer virgil card identifier.</param>
        /// <param name="privateKey">The signer private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        Task<SignModel> Trust(Guid trustedCardId, string trustedCardHash, Guid ownerCardId, byte[] privateKey, string privateKeyPassword = null);

        /// <summary>
        /// Stops trusting the specified card by deleting the sign digest.
        /// </summary>
        /// <param name="trustedCardId">The trusting Virgil Card.</param>
        /// <param name="ownerCardId">The owner Virgil Card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        Task Untrust (Guid trustedCardId, Guid ownerCardId, byte[] privateKey, string privateKeyPassword = null);

        /// <summary>
        /// Gets the application card.
        /// </summary>
        /// <param name="applicationIdentity">The application identity.</param>
        /// <returns>Virgil card dto <see cref="CardModel"/></returns>
        Task<IEnumerable<CardModel>> GetApplicationCard(string applicationIdentity);

        /// <summary>
        /// Gets the card by ID.
        /// </summary>
        /// <param name="cardId">The card ID.</param>
        /// <returns>Virgil card model.</returns>
        Task<CardModel> Get(Guid cardId);
        
        /// <summary>
        /// Revokes the specified public key.
        /// </summary>
        /// <param name="cardId">The card ID.</param>
        /// <param name="identityInfo">Validation identityInfo for card's identity.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        Task Revoke(Guid cardId, IdentityInfo identityInfo, byte[] privateKey, string privateKeyPassword = null);

        /// <summary>
        /// Gets the cards by specified public key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="cardId">The private/public keys associated card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. 
        /// It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        Task<IEnumerable<CardModel>> GetRelatedCards(Guid publicKeyId, Guid cardId, byte[] privateKey, string privateKeyPassword = null);
    }
}