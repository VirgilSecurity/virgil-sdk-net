using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Virgil.SDK.Web.Connection;

namespace Virgil.SDK.Web
{
    public interface ICardClient
    {
        /// <summary>
        /// Searches a cards on Virgil Services by specified identity.
        /// </summary>
        /// <param name="criteria">The search criteria</param>
        /// <returns>A list of found cards in raw form.</returns>
        /// <example>
        /// <code>
        ///     var client  = new CardsClient();
        ///     var rawCards = await client.SearchCardsAsync("Alice", "[YOUR_JWT_TOKEN_HERE]");
        /// </code>
        /// </example>
          Task<IEnumerable<RawSignedModel>> SearchCardsAsync(string identity, string token);

        /// <summary>
        /// Gets a card from Virgil Services by specified card ID.
        /// </summary>
        /// <param name="cardId">The card ID</param>
        /// <returns>An instance of <see cref="RawSignedModel"/> class.</returns>
        /// <example>
        /// <code>
        ///     var client  = new CardsClient();
        ///     var (cardRaw, isOutDated) = await client.GetCardAsync("[CARD_ID_HERE]", "[YOUR_JWT_TOKEN_HERE]");
        /// </code>
        /// </example>
        Task<Tuple<RawSignedModel, bool>> GetCardAsync(string cardId, string token);

        /// <summary>
        /// Publishes card in Virgil Cards service.
        /// </summary>
        /// <param name="request">An instance of <see cref="RawSignedModel"/> class</param>
        /// <example>
        /// <code>
        ///     var crypto  = new VirgilCrypto();
        ///     var manager = new CardManager(crypto);
        ///     var factory = new RequestFactory(crypto);
        ///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///     
        ///     // import app's information
        /// 
        ///     var appSigner = new CardSigner {
        ///         CardId = "[APP_CARD_ID_HERE]",
        ///         PrivateKey = crypto.ImportPrivateKey(File.ReadAllBytes(
        ///             "[YOUR_APP_KEY_PATH_HERE]"), 
        ///             "[YOUR_APP_KEY_PASSWORD_HERE]")
        ///     };
        /// 
        ///     // generate public/private key pair and create a new card
        /// 
        ///     var keypair = crypto.GenerateKeys();
        ///     var card = manager.CreateNew(new CardParams {
        ///         Identity = "Alice",
        ///         KeyPair  = keypair
        ///     });
        /// 
        ///     // publish just created card.
        /// 
        ///     var request = factory.CreatePublishRequest(card, appSigner);
        ///     await client.CreateCardAsync(request);
        /// </code>
        /// </example>
        Task<RawSignedModel> PublishCardAsync(RawSignedModel request, string token);
    }
}
