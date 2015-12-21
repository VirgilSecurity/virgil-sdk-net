namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Crypto;
    using TransferObject;

    public class Cards : IReadOnlyCollection<RecipientCard>
    {
        private readonly List<RecipientCard> cards;

        public Cards(IEnumerable<RecipientCard> collection)
        {
            cards = new List<RecipientCard>(collection);
        }

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
            return Convert.ToBase64String(Encrypt(data.GetBytes()));
        }
        
        public static async Task<Cards> Search(SearchCriteria criteria)
        {
            var services = ServiceLocator.GetServices();

            var virgilCardDtos = await services.VirgilCardClient.Search(
                criteria.IdentityValue,
                criteria.IdentityType,
                criteria.Relations,
                criteria.IncludeUnconfirmed);

            return new Cards(virgilCardDtos.Select(it => new RecipientCard(it)));
        }

        public static async Task<Cards> Search(string value,
            IdentityType? type = null,
            IEnumerable<Guid> relations = null,
            bool? includeUnconfirmed = null)
        {
            var services = ServiceLocator.GetServices();

            var virgilCardDtos = await services.VirgilCardClient.Search(
                value,
                type,
                relations,
                includeUnconfirmed);

            return new Cards(virgilCardDtos.Select(it => new RecipientCard(it)));
        }

        public static SearchCriteria PrepareSearch(string identity)
        {
            return new SearchCriteria(identity);
        }

        public IEnumerator<RecipientCard> GetEnumerator()
        {
            return cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cards.GetEnumerator();
        }

        public int Count => cards.Count;
    }
}