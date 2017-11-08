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
        private string AppCardId = ConfigurationManager.AppSettings["virgil:AppID"];
        private string AppApiToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];
        private string AppPrivateKeyPassword = ConfigurationManager.AppSettings["virgil:AppKeyPassword"];
        private string AppPrivateKeyBase64 = ConfigurationManager.AppSettings["virgil:AppPrivateKeyBase64"];
        private string CardsServiceAddress = ConfigurationManager.AppSettings["virgil:CardsServicesAddressV5"];

        [Test]
        public async Task CreateCard_ShouldRegisterNewCardOnVirgilSerivice()
        {
            var crypto = new VirgilCrypto();
            var cardsManagerParams = new CardsManagerParams()
            {
                ApiToken = AppApiToken,
                Crypto = crypto,
                ApiId = AppCardId,
                ApiUrl = CardsServiceAddress
            };
            var manager = new CardManager(cardsManagerParams);

            var keypair = crypto.GenerateKeys();
            var csr = manager.GenerateCSR(new CSRParams
            {
                Identity = "Bob",
                PublicKey = keypair.PublicKey,
                PrivateKey = keypair.PrivateKey
            });

            var appPrivateKey = crypto.ImportPrivateKey(
                Bytes.FromString(AppPrivateKeyBase64, StringEncoding.BASE64), AppPrivateKeyPassword);

            manager.SignCSR(csr, new SignParams
            {
                SignerCardId = AppCardId,
                SignerType = SignerType.App,
                SignerPrivateKey = appPrivateKey
            });

            var card = await manager.PublishCardAsync(csr);
            Assert.AreNotEqual(card, null);


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
        public async Task SearchCards_Should()
        {
            var crypto = new VirgilCrypto();
            var cardsManagerParams = new CardsManagerParams()
            {
                ApiToken = AppApiToken,
                Crypto = crypto,
                ApiId = AppCardId,
                ApiUrl = CardsServiceAddress
            };
            var manager = new CardManager(cardsManagerParams);
            var aliceCards = await manager.SearchCardsAsync("Bob");
            var aliceCard = aliceCards.First();
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