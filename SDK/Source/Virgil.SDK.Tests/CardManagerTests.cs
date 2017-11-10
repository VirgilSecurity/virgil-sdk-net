using System;
using System.Configuration;

namespace Virgil.SDK.Tests
{
    using System.Linq;

    using System.Threading.Tasks;
    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.Common;
    using Newtonsoft.Json;
    using FluentAssertions;
    using Virgil.SDK.Web;
    using Bogus;

    [TestFixture]
    public class CardManagerTests
    {
        private readonly Faker faker = new Faker();
        [Test]
        public async Task CreateCard_ShouldRegisterNewCardOnVirgilSerivice()
        {
            var card = await IntegrationHelper.PublishCard("Alice");
            Assert.AreNotEqual(card, null);
            var gotCard = await IntegrationHelper.GetCard(card.Id);
            Assert.AreNotEqual(card, gotCard);
        }

        [Test]
        public async Task CreateCardWithPreviousCardId_ShouldRegisterNewCardAndFillPreviouscardId()
        {
            // chain of cards for alice
            var aliceName = "alice-" + Guid.NewGuid();
            var aliceCard = await IntegrationHelper.PublishCard(aliceName);
            // override previous alice card
            var newAliceCard = await IntegrationHelper.PublishCard(aliceName, aliceCard.Id);
            newAliceCard.PreviousCardId.ShouldBeEquivalentTo(aliceCard.Id);
            var cards = await IntegrationHelper.SearchCardsAsync(aliceName);
            cards.Count.ShouldBeEquivalentTo(2);

            // list of cards for bob
            var bobName = "bob-" + Guid.NewGuid();
            // create two independent cards for bob
            await IntegrationHelper.PublishCard(bobName);
            await IntegrationHelper.PublishCard(bobName);

            var bobCards = await IntegrationHelper.SearchCardsAsync(bobName);
            bobCards.Count.ShouldBeEquivalentTo(2);
        }

        [Test]
        public void CreateCardWithInvalidPreviousCardId_ShouldRaiseException()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            Assert.ThrowsAsync<ClientException>(
                () => IntegrationHelper.PublishCard(aliceName, "InvalidPreviousCardId"));
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
            aliceCards.Count.ShouldBeEquivalentTo(1);
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
            var crypto = new VirgilCrypto();
            Assert.Throws<VirgilException>(
                () =>
                    originCSR.Sign(crypto, new SignParams
                    {
                        SignerCardId = faker.CardId(),
                        SignerType = SignerType.Self,
                        SignerPrivateKey = crypto.GenerateKeys().PrivateKey
                    })
              );
        }
    }
}