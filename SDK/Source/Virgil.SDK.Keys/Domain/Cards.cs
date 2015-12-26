namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Crypto;
    using Virgil.SDK.Keys.Infrastructure;
    using TransferObject;

    public class Cards : IReadOnlyCollection<RecipientCard>
    {
        private readonly List<RecipientCard> cards;

        public Cards(IEnumerable<RecipientCard> collection)
        {
            this.cards = new List<RecipientCard>(collection);
        }

        public IEnumerator<RecipientCard> GetEnumerator()
        {
            return this.cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cards.GetEnumerator();
        }

        public int Count => this.cards.Count;

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

        public string Encrypt(string data)
        {
            return Convert.ToBase64String(this.Encrypt(data.GetBytes()));
        }

        internal static async Task<Cards> Search(SearchBuilder builder)
        {
            var services = ServiceLocator.Services;

            var virgilCardDtos = await services.Cards.Search(
                builder.IdentityValue,
                builder.IdentityType,
                builder.Relations,
                builder.IncludeUnconfirmed);

            return new Cards(virgilCardDtos.Select(it => new RecipientCard(it)));
        }

        public static async Task<Cards> Search(string value,
            IdentityType? type = null,
            IEnumerable<Guid> relations = null,
            bool? includeUnconfirmed = null)
        {
            var services = ServiceLocator.Services;

            var virgilCardDtos = await services.Cards.Search(
                value,
                type,
                relations,
                includeUnconfirmed);

            return new Cards(virgilCardDtos.Select(it => new RecipientCard(it)));
        }

        public static SearchBuilder PrepareSearch(string identity)
        {
            return new SearchBuilder(identity);
        }
    }
}