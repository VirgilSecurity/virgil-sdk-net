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
        [Obsolete("This method is obsolete, please use PublishAsPrivateAsync or PublishAsGlobalAsync instead.")]
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
        [Obsolete("This method is obsolete, please use PublishAsPrivateAsync or PublishAsGlobalAsync instead.")]
        Task<CardModel> Create(IdentityInfo identityInfo, byte[] publicKey, byte[] privateKey, string privateKeyPassword = null, IDictionary<string, string> customData = null);

        /// <summary>
        /// Publishes a new Virgil private Card by specified <see cref="VirgilCardTicket"/> 
        /// ticket to Virgil Cards Service.
        /// </summary>
        Task<VirgilCard> PublishAsPrivateAsync(VirgilCardTicket ticket);

        /// <summary>
        /// Publishes a new Virgil global Card by specified <see cref="VirgilCardTicket"/> 
        /// ticket to Virgil Cards Service.
        /// </summary>
        Task<VirgilCard> PublishAsGlobalAsync(VirgilCardTicket ticket);

        /// <summary>
        /// Searches for the Virgil global Cards by specified criteria.
        /// </summary>
        /// <param name="identity">The user's identity value.</param>
        /// <param name="identityType">The user's identity type.</param>
        /// <returns>The collection of Virgil Cards.</returns>
        Task<IEnumerable<VirgilCard>> SearchGlobalAsync(string identity, IdentityType identityType);

        /// <summary>
        /// Searches for the Virgil private Cards by specified criteria.
        /// </summary>
        /// <param name="identity">The user's identity value.</param>
        /// <param name="identityType">The user's identity type.</param>
        /// <param name="includeUnsigned">The request parameter specifies whether an unsigned Virgil Cards 
        /// should be included in the search result.</param>
        /// <returns>The collection of Virgil Cards</returns>
        Task<IEnumerable<VirgilCard>> SearchPrivateAsync(string identity, string identityType, bool? includeUnsigned = false);

        /// <summary>
        /// Searches the private cards by specified criteria.
        /// </summary>
        /// <param name="identityValue">The value of identifier. Required.</param>
        /// <param name="identityType">The value of identity type. Optional.</param>
        /// <param name="includeUnauthorized">
        /// The request parameter specifies whether an unconfirmed Virgil Cards 
        /// should be included in the search result.
        /// </param>
        /// <returns>The collection of Virgil Cards.</returns>
        [Obsolete("This method is obsolete, please use SearchPrivateAsync or SearchGlobalAsync instead.")]
        Task<IEnumerable<CardModel>> Search(string identityValue, string identityType = null, bool? includeUnauthorized = null);

        /// <summary>
        /// Searches the global cards by specified criteria.
        /// </summary>
        /// <param name="identityValue">The value of identifier. Required.</param>
        /// <param name="identityType">The type of identifier. Optional.</param>
        /// <returns>The collection of Virgil Cards.</returns>
        [Obsolete("This method is obsolete, please use SearchPrivateAsync or SearchGlobalAsync instead.")]
        Task<IEnumerable<CardModel>> Search(string identityValue, IdentityType identityType);
        
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
    }
}