namespace Virgil.SDK.Tests
{
    using Client;
    using Common;
    using Cryptography;
    using NUnit.Framework;
    using System.Configuration;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Client.Requests;
    using System;

    public class RelationTests
    {
        [Test]
        public async Task AddOrDeleteRelation_ShouldAddOrDeleteEntryINRelations()
        {
            var crypto = new VirgilCrypto();
            var client = PredefinedClient(crypto);
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            // publish alice's card
            var aliceCardModel = await IntegrationHelper.PublishCard(client, crypto, "alice-" + Guid.NewGuid(), aliceKeys);

            var bobKeys = crypto.GenerateKeys();
            //publish bob's card
            var bobCardModel = await IntegrationHelper.PublishCard(client, crypto, "bob-" + Guid.NewGuid(), aliceKeys);
            aliceCardModel.Meta.Relations.Count.ShouldBeEquivalentTo(0);
           
            // add Bob's card to Alice's relations
            var addRelationRequest = new CreateCardRelationRequest {
                TrustyCardId = bobCardModel.Id,
                TrustyCardSnapshot = bobCardModel.Snapshot
            };
            addRelationRequest.OwnerSign(crypto, aliceCardModel.Id, aliceKeys.PrivateKey);
            var aliceCardModelWithRelation = await 
                client.CreateCardRelationAsync(addRelationRequest);
            
            aliceCardModelWithRelation.Meta.Relations.Count.ShouldBeEquivalentTo(1);
            var relationKey = aliceCardModelWithRelation.Meta.Relations.Keys.First();
            relationKey.ShouldBeEquivalentTo(bobCardModel.Id);

            //Delete Bob's card from Alice's relations
            var deleteRelationRequest = new RemoveCardRelationRequest
            {
                UntrustedCardId = bobCardModel.Id,
                UntrustedCardSnapshot = bobCardModel.Snapshot
            };

            deleteRelationRequest.OwnerSign(crypto, aliceCardModel.Id, aliceKeys.PrivateKey);
           
            var aliceCardModelWithoutRelation = await client.RemoveCardRelationAsync(deleteRelationRequest);
            aliceCardModelWithoutRelation.Meta.Relations.Count.ShouldBeEquivalentTo(0);

            // delete cards
            await IntegrationHelper.RevokeCard(aliceCardModel.Id);
            await IntegrationHelper.RevokeCard(bobCardModel.Id);
        }

        [Test]
        public async Task AddOrDeleteRelationWithoutAuthoritySign_ExceptionShouldOccur()
        {
            var crypto = new VirgilCrypto();
            var client = PredefinedClient(crypto);
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);
            var aliceKeys = crypto.GenerateKeys();

            // publish alice's card
            var aliceCardModel = await IntegrationHelper.PublishCard(client, crypto, "alice-" + Guid.NewGuid(), aliceKeys);

            var bobKeys = crypto.GenerateKeys();

            //publish bob's card
            var bobCardModel = await IntegrationHelper.PublishCard(client, crypto, "bob-" + Guid.NewGuid(), aliceKeys);

            aliceCardModel.Meta.Relations.Count.ShouldBeEquivalentTo(0);



            // add Bob's card to Alice's relations
            var addRelationRequest = new CreateCardRelationRequest
            {
                TrustyCardId = bobCardModel.Id,
                TrustyCardSnapshot = bobCardModel.Snapshot
            };

            Assert.ThrowsAsync<Exceptions.RelationException>(() => client.CreateCardRelationAsync(addRelationRequest));

            // Delete Bob's card from Alice's relations
            var deleteRelationRequest = new RemoveCardRelationRequest
            {
                UntrustedCardId = bobCardModel.Id,
                UntrustedCardSnapshot = bobCardModel.Snapshot
            };

            Assert.ThrowsAsync<Exceptions.RelationException>(() => client.RemoveCardRelationAsync(deleteRelationRequest));

            // delete cards
            await IntegrationHelper.RevokeCard(aliceCardModel.Id);
            await IntegrationHelper.RevokeCard(bobCardModel.Id);
        }

        private CardsClient PredefinedClient(VirgilCrypto crypto)
        {
            var client = IntegrationHelper.GetCardsClient();
            client.SetCardValidator(IntegrationHelper.GetCardValidator(crypto));

            return client;
        }
    }
}
