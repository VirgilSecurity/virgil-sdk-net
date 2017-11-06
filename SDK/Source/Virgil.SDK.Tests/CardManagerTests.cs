namespace Virgil.SDK.Tests
{
    using System.Linq;

    using System.Threading.Tasks;
    using NUnit.Framework;
    
    using Virgil.Crypto;
    using Virgil.SDK.Common;
    using Newtonsoft.Json;
    using Virgil.SDK.Web;

    [TestFixture]
    public class CardManagerTests
    {
  
        [Test]
        public async Task CreateCard_ShouldRegisterNewCardOnVirgilSerivice()
        {
            var crypto = new VirgilCrypto();

            var manager = new CardManager(new CardsManagerParams { ApiToken = AppApiToken, Crypto = crypto });

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
                SignerType = SignerType.Application,
                SignerPrivateKey = appPrivateKey
            });


            var json = JsonConvert.SerializeObject(csr.RawCard.ContentSnapshot);

            var json2 = JsonConvert.SerializeObject(csr.RawCard);

            var cardInfo = JsonConvert.DeserializeObject<RawCard>(json2);
            var card = await manager.PublishCardAsync(csr);
            
            var aliceCards = await manager.SearchCardsAsync("Alice");
            var aliceCard = aliceCards.First();

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
    }
}