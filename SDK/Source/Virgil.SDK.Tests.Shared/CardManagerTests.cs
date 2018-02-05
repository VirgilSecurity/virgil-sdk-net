using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Virgil.SDK.Common;
using Virgil.SDK.Crypto;
using Virgil.SDK.Signer;
using Virgil.SDK.Validation;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests
{
    using Bogus;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.Crypto;
    using Virgil.CryptoAPI;
    using Virgil.SDK.Web;

    [NUnit.Framework.TestFixture]
    public class CardManagerTests
    {
        private readonly Faker faker = new Faker();


        [Test]
        public async Task CreateCard_Should_RegisterNewCardOnVirgilSerivice()
        {
            var card = await IntegrationHelper.PublishCard("alice-" + Guid.NewGuid());
            Assert.AreNotEqual(card, null);
            var gotCard = await IntegrationHelper.GetCard(card.Id);
            Assert.AreNotEqual(card, gotCard);
        }


        [Test]
        public async Task CreateCardWithPreviousCardId_Should_RegisterNewCardAndFillPreviouscardId()
        {
            // chain of cards for alice
            var aliceName = "alice-" + Guid.NewGuid();
            var aliceCard = await IntegrationHelper.PublishCard(aliceName);
            // override previous alice card
            var newAliceCard = await IntegrationHelper.PublishCard(aliceName, aliceCard.Id);
            Assert.AreEqual(newAliceCard.PreviousCardId, aliceCard.Id);
        }

        [Test]
        public async Task SearchCardByIdentityWhichHasTwoRelatedCards_Should_ReturnOneActualCard()
        {
            // chain of cards for alice
            var aliceName = "alice-" + Guid.NewGuid();
            var aliceCard = await IntegrationHelper.PublishCard(aliceName);
            // override previous alice card
            var newAliceCard = await IntegrationHelper.PublishCard(aliceName, aliceCard.Id);
            var cards = await IntegrationHelper.SearchCardsAsync(aliceName);
            Assert.AreEqual(cards.Count, 1);
            var actualCard = cards.First();
            Assert.AreEqual(actualCard.Id, newAliceCard.Id);
            actualCard.PreviousCard.Id.ShouldBeEquivalentTo(aliceCard.Id);
            actualCard.PreviousCard.IsOutdated.ShouldBeEquivalentTo(true);
        }

        [Test]
        public async Task SearchCardByIdentityWhichHasTwoUnrelatedCards_Should_ReturnTwoActualCards()
        {
            // list of cards for bob
            var bobName = "bob-" + Guid.NewGuid();
            // create two independent cards for bob
            await IntegrationHelper.PublishCard(bobName);
            await IntegrationHelper.PublishCard(bobName);

            var bobCards = await IntegrationHelper.SearchCardsAsync(bobName);
            Assert.AreEqual(bobCards.Count, 2);
        }

        [Test]
        public void CreateCardWithInvalidPreviousCardId_Should_RaiseException()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            Assert.ThrowsAsync<ClientException>(
                () => IntegrationHelper.PublishCard(aliceName, "InvalidPreviousCardId"));
        }

        [Test]
        public async Task CreateCardWithNonuniquePreviousCardId_Should_RaiseExceptionAsync()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            var prevCard = await IntegrationHelper.PublishCard(aliceName);
            // first card with previous_card
            await IntegrationHelper.PublishCard(aliceName, prevCard.Id);
            // second card with the same previous_card
            Assert.ThrowsAsync<ClientException>(
                () => IntegrationHelper.PublishCard(aliceName, prevCard.Id));
        }
        [Test]
        public async Task CreateCardWithWrongIdentityInPreviousCard_Should_RaiseExceptionAsync()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            var prevCard = await IntegrationHelper.PublishCard(aliceName);
            // identity and identity of previous card shouldn't be different
            Assert.ThrowsAsync<ClientException>(
                () => IntegrationHelper.PublishCard($"new-{aliceName}", prevCard.Id));
        }

        [Test]
        public void GetCardWithWrongId_Should_RaiseException()
        {
            Assert.ThrowsAsync<ClientException>(
                () => IntegrationHelper.GetCard("InvalidCardId"));
        }

        [Test]
        public async Task SearchCards_Should_ReturnTheSameCard()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            var card = await IntegrationHelper.PublishCard(aliceName);
            var aliceCards = await IntegrationHelper.SearchCardsAsync(aliceName);
            Assert.AreEqual(aliceCards.Count, 1);
            aliceCards.First().ShouldBeEquivalentTo(card);
        }

        
        [Test]
        public void ImportPureCardFromString_Should_CreateEquivalentCard()
        {
            //STC-3
            var rawSignedModel = faker.PredefinedRawSignedModel();
            var cardManager = faker.CardManager();
            var str = rawSignedModel.ExportAsString();
            var card = cardManager.ImportCardFromString(str);
            var exportedCardStr = cardManager.ExportCardAsString(card);

            exportedCardStr.ShouldBeEquivalentTo(str);
        }

        [Test]
        public void ImportPureCardFromJson_Should_CreateEquivalentCard()
        {
            //STC-3
            var rawSignedModel = faker.PredefinedRawSignedModel();
            var cardManager = faker.CardManager();
            var json = rawSignedModel.ExportAsJson();
            var card = cardManager.ImportCardFromJson(json);
            var exportedCardJson = cardManager.ExportCardAsJson(card);

            exportedCardJson.ShouldBeEquivalentTo(json);
        }



        [Test]
        public void ImportFullCardFromString_Should_CreateEquivalentCard()
        {
            //STC-4
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, true);
            var cardManager = faker.CardManager();
            var str = rawSignedModel.ExportAsString();
            var card = cardManager.ImportCardFromString(str);
            var exportedCardStr = cardManager.ExportCardAsString(card);

            exportedCardStr.ShouldBeEquivalentTo(str);
        }

        [Test]
        public void ImportFullCardFromJson_Should_CreateEquivalentCard()
        {
            //STC-4
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, true);
            var cardManager = faker.CardManager();
            var json = rawSignedModel.ExportAsJson();
            var card = cardManager.ImportCardFromJson(json);
            var exportedCardJson = cardManager.ExportCardAsJson(card);

            exportedCardJson.ShouldBeEquivalentTo(json);
        }
        [Test]
        public void CardManager_Should_RaiseException_IfExpiredToken()
        {
            // STC-26
            var jwtGenerator = new JwtGenerator(
                faker.AppId(),
                IntegrationHelper.ApiPrivateKey(),
                IntegrationHelper.ApiPublicKeyId,
                TimeSpan.Zero,
                Substitute.For<VirgilAccessTokenSigner>());
            var accessTokenProvider = Substitute.For<IAccessTokenProvider>();
            accessTokenProvider.GetTokenAsync(Arg.Any<TokenContext>()).Returns(
                jwtGenerator.GenerateToken(faker.Random.AlphaNumeric(20))
                );
            var cardManager = faker.CardManager(accessTokenProvider);

            Assert.ThrowsAsync<UnauthorizedClientException>(
                () => cardManager.GetCardAsync(faker.CardId()));
        }

        [Test]
        public async Task CardManager_Should_SendSecondRequestToCliet_IfTokenExpiredAndRetryOnUnauthorizedAsync()
        {
            // STC-26
            var expiredJwtGenerator = new JwtGenerator(
                faker.AppId(),
                IntegrationHelper.ApiPrivateKey(),
                IntegrationHelper.ApiPublicKeyId,
                TimeSpan.Zero,
                Substitute.For<VirgilAccessTokenSigner>());

            var jwtGenerator = new JwtGenerator(
                faker.AppId(),
                IntegrationHelper.ApiPrivateKey(),
                IntegrationHelper.ApiPublicKeyId,
                TimeSpan.FromMinutes(10),
                Substitute.For<VirgilAccessTokenSigner>());
            var accessTokenProvider = Substitute.For<IAccessTokenProvider>();
            // suppose we have got expired token at the first attempt
            // and we have got valid token at the second attempt
            accessTokenProvider.GetTokenAsync(Arg.Any<TokenContext>()
                ).Returns(
                args => 
                ((TokenContext)args[0]).ForceReload ? 
                jwtGenerator.GenerateToken(faker.Random.AlphaNumeric(20)) : 
                expiredJwtGenerator.GenerateToken(faker.Random.AlphaNumeric(20))
                );

            var cardManager = faker.CardManager(accessTokenProvider, true);
            var keypair = new VirgilCrypto().GenerateKeys();

            var card = await cardManager.PublishCardAsync(
                new CardParams()
                {
                    PublicKey = keypair.PublicKey,
                    PrivateKey = keypair.PrivateKey
                    
                });
            Assert.NotNull(card);

        }
    }
}