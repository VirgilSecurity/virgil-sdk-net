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
    using Client.Requests;
    using System.Configuration;

    public class VirgilCardSearchTest
    {
        [Test]
        public async Task SearchForVirgilCards_ValidationWithServiceKey_ShouldPassValidation()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetCardsClient();
            client.SetCardValidator(new CardValidator(crypto));

            // CREATING A VIRGIL CARD
            var aliceKeys = crypto.GenerateKeys();
            
            var aliceIdentity = "alice-" + Guid.NewGuid();

            // publish alice's card
            var aliceCard = await IntegrationHelper.PublishCard(client, crypto, aliceIdentity, aliceKeys);

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
            var client = IntegrationHelper.GetCardsClient();
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var bobKeys = crypto.GenerateKeys();

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var bobIdentity = "bob-" + Guid.NewGuid();

            // publish alice's card
            var aliceCard = await IntegrationHelper.PublishCard(client, crypto, aliceIdentity, aliceKeys);

            // publish bob's card
            var bobCard = await IntegrationHelper.PublishCard(client, crypto, bobIdentity, bobKeys);

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
            var client = IntegrationHelper.GetCardsClient();
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var aliceKeys = crypto.GenerateKeys();

            // publish alice's card
            var aliceCard = await IntegrationHelper.PublishCard(client, crypto, aliceIdentity, aliceKeys);

            var foundAliceCard = await client.GetCardAsync(aliceCard.Id);

            aliceCard.ShouldBeEquivalentTo(foundAliceCard);

            await IntegrationHelper.RevokeCard(aliceCard.Id);
        }
    }
}