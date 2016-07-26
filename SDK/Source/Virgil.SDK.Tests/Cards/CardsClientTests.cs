namespace Virgil.SDK.Keys.Tests.Cards
{
    using Cryptography;
    using NSubstitute;
    using NUnit.Framework;

    public class CardsClientTests
    {
        [Test]
        public void Test1()
        {
            var serviceHub = ServiceHub.Create("<ACCESS_TOKEN>");

            VirgilCrypto.SetDefaultCryptoProvider(Substitute.For<ICryptoProvider>());
            VirgilCrypto.SetDefaultKeyStorage(Substitute.For<IKeyStorage>());

            
            
            var crypto = new VirgilCrypto();
            
            var key = crypto.CreateKey("alice");

            var recipients = serviceHub.Cards.SearchAsync("denis").Result;
            var cipherData = crypto.EncryptText("Hello world!!!", recipients);

            var readyToTransfer = cipherData.ToBase64String();
        }
    }
}