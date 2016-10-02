namespace Virgil.SDK.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Virgil.SDK.Client;

    public static class VirgilClientExtensions
    {
        public static async Task<IList<Card>> SearchAndValidateCardsAsync(this VirgilClient client, SearchCriteria criteria, ICardValidator validator)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            var cards = await client.SearchCardsAsync(criteria).ConfigureAwait(false);
            var foundCards = cards.ToList();

            var invalidCards = foundCards.Where(c => !validator.Validate(c)).ToList();
            if (invalidCards.Any())
            {
                throw new CardValidationException(invalidCards);
            }

            return foundCards;
        }
    }
}