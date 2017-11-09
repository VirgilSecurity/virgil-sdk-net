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
            var gotCard = await IntegrationHelper.GetCard(card.Id.ToString());
            Assert.AreNotEqual(card, gotCard);

            // var plainbytes = Bytes.FromString("Hello There :)");
            // var cipherbytes = crypto.Encrypt(plainbytes, aliceCard.PublicKey);

            // generate a new public/private key pair
            //var keyPair = crypto.GenerateKeys();

            //// create card info with public key and identity name
            //var cardInfo = new CardRequestParams
            //{
            //    Identity = "Alice",
            //    PublicKey = keyPair.PublicKey
            //};

            //// create request for registering the card.  
            //var csr = requestManager.CreateCardRequest(cardInfo, keyPair.PrivateKey);

            //// import private key from base64 encoded string.
            //var appPrivateKey = crypto.ImportPrivateKey(
            //    Bytes.FromString(AppPrivateKeyBase64, StringEncoding.BASE64), AppPrivateKeyPassword);

            //// sign request using application private key.
            //requestManager.SignRequest(csr, new SignParams { CardId = AppCardId, PrivateKey = appPrivateKey });

            //// register new card
            //var card = await manager.CreateCardAsync(csr);
        }

        [Test]
        [Ignore("cash of revoked card")]
        public async Task CreateCardWithPreviousCardId_ShouldRegisterNewCardAndInvokePrevious()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            var card = await IntegrationHelper.PublishCard(aliceName);
            var newAliceName = "alice-" + Guid.NewGuid();
            await IntegrationHelper.PublishCard(newAliceName, card.Id);
            //System.Threading.Thread.Sleep(600000);
            var revokedCard = await IntegrationHelper.GetCard(card.Id);
            revokedCard.ShouldBeEquivalentTo(null);
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
    }
}