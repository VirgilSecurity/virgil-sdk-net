namespace Virgil.SDK.Tests
{
    using System.Linq;

    using System.Threading.Tasks;
    using NUnit.Framework;
    
    using Virgil.Crypto;

    [TestFixture]
    public class CardManagerTests
    {
        private const string AppCardId             = "76d9c8db57cc04707934d23db0e7175f80ae136fd0328e5a6e50e60b09b2c109";
        private const string AppApiToken           = "AT.0714bb314c8097b4a0ae5559788a55604d4b57449cbc695035de76692154f2bc";
        private const string AppPrivateKeyPassword = "111";
        private const string AppPrivateKeyBase64   = "MIGhMF0GCSqGSIb3DQEFDTBQMC8GCSqGSIb3DQEFDDAiBBDR+rirWuMEMzN" +
                                                     "JoQn+Hp0LAgIOcTAKBggqhkiG9w0CCjAdBglghkgBZQMEASoEEFXZ5AmBhM" +
                                                     "frK7ekA6r88WwEQLMOZ/WUsJBvU897m6Bh8eKTgeCK3E/f+RLGPoAH6GbR3" +
                                                     "ZXgHVdZb3rY0joGgZxoTHhNg1lWBPTKPqeZj481fFI=";

        [Test]
        public async Task CreateCard_ShouldRegisterNewCardOnVirgilSerivice()
        {
            var crypto = new VirgilCrypto();

            var manager = new CardManager(new CardsManagerParams { ApiToken = AppApiToken, Crypto = crypto });

            //var keypair = crypto.GenerateKeys();
            //var csr = manager.GenerateCSR(new CSRParams
            //{
            //    Identity = "Bob",
            //    PublicKey = keypair.PublicKey,
            //    PrivateKey = keypair.PrivateKey
            //});

            //var appPrivateKey = crypto.ImportPrivateKey(
            //    Bytes.FromString(AppPrivateKeyBase64, StringEncoding.BASE64), AppPrivateKeyPassword);

            //manager.SignCSR(csr, new SignParams
            //{
            //    SignerCardId = AppCardId,
            //    SignerType = SignerType.Application,
            //    SignerPrivateKey = appPrivateKey
            //});

            //var card = await manager.PublishCardAsync(csr);

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