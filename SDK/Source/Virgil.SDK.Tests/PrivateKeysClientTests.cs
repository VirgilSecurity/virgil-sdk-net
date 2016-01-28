namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Exceptions;
    using FluentAssertions;
    using NUnit.Framework;
    using SDK.Domain;
    using Virgil.SDK.Infrastructure;

    public class PrivateKeysClientTests
    {
        [SetUp]
        public void Init()
        {
            ServiceLocator.SetupForTests();
        }

        [Test]
        public async Task FullPrivateKeyLifeCycleTest()
        {
            var emailName = Mailinator.GetRandomEmailName();
            var request = await Identity.Verify(emailName);
            await Task.Delay(1000);
            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName);
            var identityToken = await request.Confirm(confirmationCode, new ConfirmOptions(300, 2));
            var card = await PersonalCard.Create(identityToken);
            var privateKeysClient = ServiceLocator.Services.PrivateKeys;

            await privateKeysClient.Stash(card.Id, card.PrivateKey);
            var grabResponse = await privateKeysClient.Get(card.Id, identityToken);

            grabResponse.PrivateKey.Should().BeEquivalentTo(card.PrivateKey.Data);

            await privateKeysClient.Destroy(card.Id, grabResponse.PrivateKey);
            
            try
            {
                await privateKeysClient.Get(card.Id, identityToken);
                throw new Exception("Test failed");
            }
            catch (VirgilPrivateServicesException)
            {
            }
        }

        [Test]
        public async Task ShouldBeAbleToProtectKeyWithPassword()
        {
            var emailName = Mailinator.GetRandomEmailName();
            var request = await Identity.Verify(emailName);
            await Task.Delay(1000);
            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName);
            var identityToken = await request.Confirm(confirmationCode, new ConfirmOptions(300, 2));

            var privateKeyPassword = "PASSWORD";

            var card = await PersonalCard.Create(identityToken, privateKeyPassword);
            var privateKeysClient = ServiceLocator.Services.PrivateKeys;

            await privateKeysClient.Stash(card.Id, card.PrivateKey, privateKeyPassword);
            var grabResponse = await privateKeysClient.Get(card.Id, identityToken);

            grabResponse.PrivateKey.Should().BeEquivalentTo(card.PrivateKey.Data);
            

            var virgilCipher1 = new VirgilCipher();
            var virgilCipher2 = new VirgilCipher();


            virgilCipher1.AddKeyRecipient(card.GetRecepientId(), card.PublicKey);

            var expected = "TEST";

            var encrypt = virgilCipher1.Encrypt(Encoding.UTF8.GetBytes(expected), true);
            
            var decrypt = virgilCipher2.DecryptWithKey(
                encrypt, 
                card.GetRecepientId(), 
                grabResponse.PrivateKey,
                privateKeyPassword.GetBytes());

            Encoding.UTF8.GetString(decrypt).Should().Be(expected);
        }

        [Test]
        public async Task ShouldAllowToUploadKeyForUnconfirmedCard()
        {
            var card = await PersonalCard.Create(Mailinator.GetRandomEmailName());
            await card.UploadPrivateKey();
        }
    }
}