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


    public class RelationTests
    {
        [Test]
        public async Task AddOrDeleteRelation_ShouldAddOrDeleteEntryINRelations()
        {
            const string identityType = "member";
            var crypto = new VirgilCrypto();
            var client = PredefinedClient(crypto);
            var requestSigner = new RequestSigner(crypto);

            var aliceKeys = crypto.GenerateKeys();
            var aliceExportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
            var aliceRequest = new PublishCardRequest("alice", identityType, aliceExportedPublicKey);

            var bobKeys = crypto.GenerateKeys();
            var bobExportedPublicKey = crypto.ExportPublicKey(bobKeys.PublicKey);
            var bobRequest = new PublishCardRequest("bob", identityType, bobExportedPublicKey);

            var appId = ConfigurationManager.AppSettings["virgil:AppID"];
            var appKey = crypto.ImportPrivateKey(
                VirgilBuffer.FromFile(ConfigurationManager.AppSettings["virgil:AppKeyPath"]).GetBytes(),
                ConfigurationManager.AppSettings["virgil:AppKeyPassword"]);


            // publish cards
            requestSigner.SelfSign(aliceRequest, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(aliceRequest, appId, appKey);
            var aliceCardModel = await client
                .PublishCardAsync(aliceRequest).ConfigureAwait(false);

            requestSigner.SelfSign(bobRequest, bobKeys.PrivateKey);
            requestSigner.AuthoritySign(bobRequest, appId, appKey);
            var bobCardModel = await client
               .PublishCardAsync(bobRequest).ConfigureAwait(false);

            aliceCardModel.Meta.Relations.Count.ShouldBeEquivalentTo(0);
           
            // add Bob's card to Alice's relations
            var addRelationRequest = new AddRelationRequest(bobCardModel.SnapshotModel);
            requestSigner.AuthoritySign(addRelationRequest, aliceCardModel.Id, aliceKeys.PrivateKey);
            var aliceCardModelWithRelation = await client.AddRelationAsync(addRelationRequest);
            
            aliceCardModelWithRelation.Meta.Relations.Count.ShouldBeEquivalentTo(1);
            var relationKey = aliceCardModelWithRelation.Meta.Relations.Keys.First();
            relationKey.ShouldBeEquivalentTo(bobCardModel.Id);

            //Delete Bob's card from Alice's relations
            var deleteRelationRequest = new DeleteRelationRequest(bobCardModel.Id, RevocationReason.Unspecified);
            requestSigner.AuthoritySign(deleteRelationRequest, aliceCardModelWithRelation.Id, aliceKeys.PrivateKey);

            var aliceCardModelWithoutRelation = await client.DeleteRelationAsync(deleteRelationRequest);
            aliceCardModelWithoutRelation.Meta.Relations.Count.ShouldBeEquivalentTo(0);

            // delete cards
            var revokeBobRequest = new RevokeCardRequest(bobCardModel.Id, RevocationReason.Unspecified);
            requestSigner.AuthoritySign(revokeBobRequest, appId, appKey);
            await client.RevokeCardAsync(revokeBobRequest);

            var revokeAliceRequest = new RevokeCardRequest(aliceCardModel.Id, RevocationReason.Unspecified);
            requestSigner.AuthoritySign(revokeAliceRequest, appId, appKey);
            await client.RevokeCardAsync(revokeAliceRequest);        
        }

        [Test]
        public async Task AddOrDeleteRelationWithoutAuthoritySign_ExceptionShouldOccur()
        {

            const string identityType = "member";
            var crypto = new VirgilCrypto();
            var client = PredefinedClient(crypto);
            var requestSigner = new RequestSigner(crypto);

            var aliceKeys = crypto.GenerateKeys();
            var aliceExportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
            var aliceRequest = new PublishCardRequest("alice", identityType, aliceExportedPublicKey);

            var bobKeys = crypto.GenerateKeys();
            var bobExportedPublicKey = crypto.ExportPublicKey(bobKeys.PublicKey);
            var bobRequest = new PublishCardRequest("bob", identityType, bobExportedPublicKey);

            var appId = ConfigurationManager.AppSettings["virgil:AppID"];
            var appKey = crypto.ImportPrivateKey(
                VirgilBuffer.FromFile(ConfigurationManager.AppSettings["virgil:AppKeyPath"]).GetBytes(),
                ConfigurationManager.AppSettings["virgil:AppKeyPassword"]);


            // publish cards
            requestSigner.SelfSign(aliceRequest, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(aliceRequest, appId, appKey);
            var aliceCardModel = await client
                .PublishCardAsync(aliceRequest).ConfigureAwait(false);

            requestSigner.SelfSign(bobRequest, bobKeys.PrivateKey);
            requestSigner.AuthoritySign(bobRequest, appId, appKey);
            var bobCardModel = await client
               .PublishCardAsync(bobRequest).ConfigureAwait(false);

            aliceCardModel.Meta.Relations.Count.ShouldBeEquivalentTo(0);


            // add Bob's card to Alice's relations
            var addRelationRequest = new AddRelationRequest(bobCardModel.SnapshotModel);
            Assert.ThrowsAsync<Exceptions.RelationException>(() => client.AddRelationAsync(addRelationRequest));

            // Delete Bob's card from Alice's relations
            var deleteRelationRequest = new DeleteRelationRequest(bobCardModel.Id, RevocationReason.Unspecified);
            Assert.ThrowsAsync<Exceptions.RelationException>(() => client.DeleteRelationAsync(deleteRelationRequest));

            // delete cards
            var revokeBobRequest = new RevokeCardRequest(bobCardModel.Id, RevocationReason.Unspecified);
            requestSigner.AuthoritySign(revokeBobRequest, appId, appKey);
            await client.RevokeCardAsync(revokeBobRequest);

            var revokeAliceRequest = new RevokeCardRequest(aliceCardModel.Id, RevocationReason.Unspecified);
            requestSigner.AuthoritySign(revokeAliceRequest, appId, appKey);
            await client.RevokeCardAsync(revokeAliceRequest);
        }

        private CardsClient PredefinedClient(VirgilCrypto crypto)
        {
            var cardsClientParams = new CardsClientParams(ConfigurationManager.AppSettings["virgil:AppAccessToken"]);
            cardsClientParams.SetCardsServiceAddress(ConfigurationManager.AppSettings["virgil:CardsServicesAddress"]);
            cardsClientParams.SetRAServiceAddress(ConfigurationManager.AppSettings["virgil:RAServicesAddress"]);
            cardsClientParams.SetReadCardsServiceAddress(ConfigurationManager.AppSettings["virgil:CardsReadServicesAddress"]);

            var identityClientParams = new IdentityClientParams();
            identityClientParams.SetIdentityServiceAddress(ConfigurationManager.AppSettings["virgil:IdentityServiceAddress"]);

            var validator = new CardValidator(crypto);

            // To use staging Verifier instead of default verifier
            var cardVerifier = new CardVerifierInfo
            {
                CardId = ConfigurationManager.AppSettings["virgil:ServiceCardId"],
                PublicKeyData = VirgilBuffer.From(ConfigurationManager.AppSettings["virgil:ServicePublicKeyDerBase64"],
                StringEncoding.Base64)
            };
            validator.AddVerifier(cardVerifier.CardId, cardVerifier.PublicKeyData.GetBytes());

            var client = new CardsClient(cardsClientParams);
            client.SetCardValidator(validator);

            return client;
        }
    }
}
