namespace Virgil.SDK.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Common;
    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;

    public class VirgilCardSearchTest
    {
        [Test]
        public async Task SearchForVirgilCards_ValidationWithServiceKey_ShouldPassValidation()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetVirgilClient();
            client.SetCardValidator(new CardValidator(crypto));

            // CREATING A VIRGIL CARD

            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var request = new PublishCardRequest(aliceIdentity, "member", exportedPublicKey);

            var requestSigner = new RequestSigner(crypto);

            requestSigner.SelfSign(request, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(request, IntegrationHelper.AppID, appKey);
            
            var aliceCard = await client.PublishCardAsync(request);

            // VALIDATING A VIRGIL CARD
            
            var cards = await client.SearchCardsAsync(SearchCriteria.ByIdentity(aliceIdentity));

            aliceCard.ShouldBeEquivalentTo(cards.Single());

            await IntegrationHelper.RevokeCard(aliceCard.Id);
        }

        [Test]
        public async Task SearchForTheVirgilCards_MultipleIdentitiesGiven_ShouldReturnVirgilCards()
        {
            // Initialization

            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetVirgilClient();
            var requestSigner = new RequestSigner(crypto);
            
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            // Prepare Requests

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var aliceKeys = crypto.GenerateKeys();
            var alicePublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var bobIdentity = "bob-" + Guid.NewGuid();
            var bobKeys = crypto.GenerateKeys();
            var bobPublicKey = crypto.ExportPublicKey(bobKeys.PublicKey);

            var aliceRequest = new PublishCardRequest(aliceIdentity, "member", alicePublicKey);
            var bobRequest = new PublishCardRequest(bobIdentity, "member", bobPublicKey);

            requestSigner.SelfSign(aliceRequest, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(aliceRequest, IntegrationHelper.AppID, appKey);

            requestSigner.SelfSign(bobRequest, bobKeys.PrivateKey);
            requestSigner.AuthoritySign(bobRequest, IntegrationHelper.AppID, appKey);

            // Publish Virgil Cards 

            var aliceCard = await client.PublishCardAsync(aliceRequest);
            var bobCard = await client.PublishCardAsync(bobRequest);
            
            // Search for the Virgil Cards

            var foundCards = await client.SearchCardsAsync(new SearchCriteria
            {
                Identities = new[] { bobIdentity, aliceIdentity }
            });

            // Assertions
            
            foundCards.Should().HaveCount(2);

            foundCards.Single(it => it.Id == aliceCard.Id).ShouldBeEquivalentTo(aliceCard);
            foundCards.Single(it => it.Id == bobCard.Id).ShouldBeEquivalentTo(bobCard);

            await IntegrationHelper.RevokeCard(aliceCard.Id);
            await IntegrationHelper.RevokeCard(bobCard.Id);
        }

        [Test]
        public async Task GetSignleVirgilCard_ByGivenId_ShouldReturnVirgilCard()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetVirgilClient();
            var requestSigner = new RequestSigner(crypto);

            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            // Prepare Requests

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var aliceKeys = crypto.GenerateKeys();
            var alicePublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceRequest = new PublishCardRequest(aliceIdentity, "member", alicePublicKey);

            requestSigner.SelfSign(aliceRequest, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(aliceRequest, IntegrationHelper.AppID, appKey);

            var aliceCard = await client.PublishCardAsync(aliceRequest);
            var foundAliceCard = await client.GetCardAsync(aliceCard.Id);

            aliceCard.ShouldBeEquivalentTo(foundAliceCard);

            await IntegrationHelper.RevokeCard(aliceCard.Id);
        }
    }
}