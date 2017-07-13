namespace Virgil.SDK.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FluentAssertions;
    using NUnit.Framework;
    using Client.Requests;
    using Cryptography;
    using Client;
    using Exceptions;

    public class VirgilCardCreationTest
    {
        [Test]
        public async Task CreateNewVirgilCard_DuplicateCardCreation_ShouldThrowException()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetCardsClient();
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();

            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
            var request = new CreateUserCardRequest
            {
                Identity = "alice-" + Guid.NewGuid(),
                PublicKeyData = exportedPublicKey
            };

            request.SelfSign(crypto, aliceKeys.PrivateKey);

            var exportedRequest = request.Export();

            // transfer alice's request to the server

            var importedRequest = new CreateUserCardRequest();
            importedRequest.Import(exportedRequest);

            importedRequest.ApplicationSign(crypto, IntegrationHelper.AppID, appKey);

            // publish alice's card
            var cardModel = await client.CreateUserCardAsync(importedRequest);

            Assert.ThrowsAsync<VirgilClientException>(async () => await client.CreateUserCardAsync(request));

            await IntegrationHelper.RevokeCard(cardModel.Id);
        }

        [Test]
        public async Task CreateNewVirgilCard_IdentityAndPublicKeyGiven_ShouldBeFoundByIdentity()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetCardsClient();
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var aliceIdentity = "alice-" + Guid.NewGuid();

            // publish alice's card
            var aliceCard = await IntegrationHelper.PublishCard(client, crypto, aliceIdentity, aliceKeys);

            var cards = await client.SearchCardsAsync(new SearchCriteria { Identities = new[] { aliceIdentity } });
            
            cards.Should().HaveCount(1);
            var foundCard = cards.Single();

            aliceCard.ShouldBeEquivalentTo(foundCard);

            await IntegrationHelper.RevokeCard(aliceCard.Id);
        }

        [Test]
        public async Task CreateNewVirgilCard_SignatureValidation_ShouldPassValidation()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetCardsClient();

            // CREATING A VIRGIL CARD
            var aliceKeys = crypto.GenerateKeys();
            var aliceIdentity = "alice-" + Guid.NewGuid();

            // publish alice's card
            var aliceCard = await IntegrationHelper.PublishCard(client, crypto, aliceIdentity, aliceKeys);

            // VALIDATING A VIRGIL CARD
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);
            var appPublicKey = crypto.ExtractPublicKey(appKey);
            var exportedAppPublicKey = crypto.ExportPublicKey(appPublicKey);

            var validator = new CardValidator(crypto);
            validator.AddVerifier(IntegrationHelper.AppID, exportedAppPublicKey);

            validator.Validate(aliceCard).Should().BeTrue();

            await IntegrationHelper.RevokeCard(aliceCard.Id);
        }

    }
}