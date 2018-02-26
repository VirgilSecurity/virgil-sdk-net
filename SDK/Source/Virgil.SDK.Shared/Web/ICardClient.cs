using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Web
{
    /// <summary>
    /// The <see cref="ICardClient"/> interface defines a list of operations with Virgil Cards service.
    /// </summary>
    public interface ICardClient
    {
        /// <summary>
        /// Searches a cards on Virgil Services by specified identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="token">The string representation of <see cref="Jwt"/> token.</param>
        /// <returns>A list of found cards in raw form.</returns>
        Task<IEnumerable<RawSignedModel>> SearchCardsAsync(string identity, string token);

        /// <summary>
        /// Gets a card from Virgil Services by specified card ID.
        /// </summary>
        /// <param name="cardId">The card ID</param>
        /// <param name="token">The string representation of <see cref="Jwt"/> token.</param>
        /// <returns>An instance of <see cref="RawSignedModel"/> class and flag, 
        /// which determines whether or not this raw card is superseded.</returns>
        Task<Tuple<RawSignedModel, bool>> GetCardAsync(string cardId, string token);

        /// <summary>
        /// Publishes card in Virgil Cards service.
        /// </summary>
        /// <param name="request">An instance of <see cref="RawSignedModel"/> class</param>
        /// <param name="token">The string representation of <see cref="Jwt"/> token.</param>
        /// <returns>published raw card.</returns>
        Task<RawSignedModel> PublishCardAsync(RawSignedModel request, string token);
    }
}
