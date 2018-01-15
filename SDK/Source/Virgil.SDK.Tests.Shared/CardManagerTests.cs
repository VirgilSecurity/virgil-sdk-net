using System;
using FluentAssertions;

namespace Virgil.SDK.Tests
{
    using Bogus;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.Crypto;
    using Virgil.SDK.Web;

    [NUnit.Framework.TestFixture]
    public class CardManagerTests
    {
        private readonly Faker faker = new Faker();
        [Test]
        public async Task CreateCard_Should_RegisterNewCardOnVirgilSerivice()
        {
            var card = await IntegrationHelper.PublishCard("Alice");
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
        public async Task SearchCardByIdentityWhichHasTwoRelatedCards_Should_ReturnOneActualCards()
        {
            var cards2 = await IntegrationHelper.SearchCardsAsync("alice");
            // chain of cards for alice
            var aliceName = "alice-" + Guid.NewGuid();
            var aliceCard = await IntegrationHelper.PublishCard(aliceName);
            // override previous alice card
            var newAliceCard = await IntegrationHelper.PublishCard(aliceName, aliceCard.Id);
            var cards = await IntegrationHelper.SearchCardsAsync(aliceName);
            Assert.AreEqual(cards.Count, 1);
            var actualCard = cards.First();
            Assert.AreEqual(actualCard.Id, newAliceCard.Id);
            actualCard.PreviousCard.ShouldBeEquivalentTo(aliceCard);
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
        public void ImportCSR_Should_CreateEquivalentCSR()
        {
            var originCSR = faker.GenerateCSR();
            var exported = originCSR.Export();
            var cardManager = faker.CardManager();
            var importedCSR = cardManager.ImportCSR(exported);
            importedCSR.ShouldBeEquivalentTo(originCSR);
        }

        [Test]
        public void CSRSignWithNonUniqueSignType_Should_RaiseException()
        {
            var originCSR = faker.GenerateCSR();
            var cardCrypto = new VirgilCardCrypto();
            var crypto = new VirgilCrypto();
            Assert.Throws<VirgilException>(
                () =>
                    originCSR.Sign(cardCrypto, new SignParams
                    {
                        SignerCardId = faker.CardId(),
                        SignerType = SignerType.Self,
                        SignerPrivateKey = crypto.GenerateVirgilKeys().PrivateKey
                    })
              );
        }
    }
}