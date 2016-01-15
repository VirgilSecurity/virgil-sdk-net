namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.Crypto;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Domain entity that represents a list of recipients Virgil Cards.
    /// </summary>
    /// <seealso cref="RecipientCard" />
    public class Cards : IReadOnlyCollection<RecipientCard>
    {
        private readonly List<RecipientCard> cards;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cards"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public Cards(IEnumerable<RecipientCard> collection)
        {
            this.cards = new List<RecipientCard>(collection);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of Virgil Cards.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<RecipientCard> GetEnumerator()
        {
            return this.cards.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection of recipient Cards.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cards.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        public int Count => this.cards.Count;

        /// <summary>
        /// Encrypts the specified data for all recipient cards in the collection.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        /// <returns>Encrypted array of bytes.</returns>
        public byte[] Encrypt(byte[] data)
        {
            using (var cipher = new VirgilCipher())
            {
                foreach (var card in this)
                {
                    cipher.AddKeyRecipient(card.GetRecepientId(), card.PublicKey.Data);
                }

                return cipher.Encrypt(data);
            }
        }

        /// <summary>
        /// Encrypts the specified text for all recipient cards in the collection.
        /// </summary>
        /// <param name="text">The text to be encrypted.</param>
        /// <returns>Encrypted text.</returns>
        public string Encrypt(string text)
        {
            return Convert.ToBase64String(this.Encrypt(text.GetBytes()));
        }
        
        internal static async Task<Cards> Search(SearchOptions options)
        {
            var services = ServiceLocator.Services;

            var virgilCardDtos = await services.Cards.Search(
                options.IdentityValue,
                options.IdentityType,
                options.Relations,
                options.IncludeUnconfirmed).ConfigureAwait(false);

            return new Cards(virgilCardDtos.Select(it => new RecipientCard(it)));
        }

        /// <summary>
        /// Searches the cards with specified value, and additional parameters.
        /// </summary>
        /// <param name="value">The string that represents identity value.</param>
        /// <param name="type">The type of identity.</param>
        /// <param name="relations">The a list of relations.</param>
        /// <param name="includeUnconfirmed">Indicates wherever unconfirmed cards should be included to search.</param>
        /// <returns>The collection of found cards.</returns>
        public static async Task<Cards> Search(string value,
            IdentityType? type = null,
            IEnumerable<Guid> relations = null,
            bool? includeUnconfirmed = null)
        {
            var services = ServiceLocator.Services;

            var cards = await services.Cards
                .Search(value,type,relations,includeUnconfirmed)
                .ConfigureAwait(false);

            return new Cards(cards.Select(it => new RecipientCard(it)));
        }

        /// <summary>
        /// Initializes search builder with fluent interface.
        /// </summary>
        /// <param name="identity">The string that represents an identity.</param>
        /// <returns>The instance of <see cref="SearchOptions"/></returns>
        public static SearchOptions PrepareSearch(string identity)
        {
            return new SearchOptions(identity);
        }
    }
}