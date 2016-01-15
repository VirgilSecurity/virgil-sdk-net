namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure;
    using TransferObject;

    public class PersonalCardLoader 
    {
        private readonly string identity;
        private readonly IdentityType type;
        private IEnumerable<CardIds> setup;

        private PersonalCardLoader(string identity, IdentityType type)
        {
            this.identity = identity;
            this.type = type;
        }

        private class CardIds
        {
            public Guid PublicKeyId { get; set; }
            public Guid Id { get; set; }
        }

        public static async Task<PersonalCardLoader> Start(string identity, IdentityType type)
        {
            var saga = new PersonalCardLoader(identity, type);

            var services = ServiceLocator.Services;
            var searchResult = await services.Cards.Search(saga.identity, saga.type).ConfigureAwait(false);

            saga.setup = searchResult
                .Select(it => new CardIds {PublicKeyId = it.PublicKey.Id, Id = it.Id})
                .Distinct();

            return saga;
        }

        public async Task<IdentityTokenRequest> Verify()
        {
            return await Identity.Verify(this.identity, this.type);
        }

        public async Task<IEnumerable<PersonalCard>> Finish(IdentityTokenRequest request, string confirmationCode)
        {
            var services = ServiceLocator.Services;

            var token = await request.Confirm(confirmationCode, new ConfirmOptions(3600, this.setup.Count()))
                .ConfigureAwait(false);

            var list = this.setup.Select(async card =>
            {
                var grabResponse = await services.PrivateKeys.Get(card.Id, token)
                    .ConfigureAwait(false);

                var privateKey = new PrivateKey(grabResponse.PrivateKey);

                var cards = await services.PublicKeys.GetExtended(card.PublicKeyId, card.Id, privateKey)
                    .ConfigureAwait(false);

                return cards.Select(it => new PersonalCard(it, privateKey)).ToList();

            }).ToList();

            await Task.WhenAll(list)
                .ConfigureAwait(false);

            var personalCards = list.SelectMany(i => i.Result).ToList();

            return personalCards;
        }
    }
}