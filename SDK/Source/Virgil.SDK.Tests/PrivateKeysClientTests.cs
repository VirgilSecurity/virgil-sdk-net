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

    using Virgil.SDK.TransferObject;

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

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName, true);
            var identityToken = await request.Confirm(confirmationCode, new ConfirmOptions(300, 2));

            var keyPair = VirgilKeyPair.Generate();
            var card = await ServiceLocator.Services.Cards.Create(identityToken, keyPair.PublicKey(), keyPair.PrivateKey());
            
            var privateKeysClient = ServiceLocator.Services.PrivateKeys;

            await privateKeysClient.Stash(card.Id, keyPair.PrivateKey());
            var grabResponse = await privateKeysClient.Get(card.Id, identityToken);

            grabResponse.PrivateKey.Should().BeEquivalentTo(keyPair.PrivateKey());

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
            

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName, true);
            var identityToken = await request.Confirm(confirmationCode, new ConfirmOptions(300, 2));

            var privateKeyPassword = "PASSWORD";

            var keyPair = VirgilKeyPair.Generate();
            var card = await ServiceLocator.Services.Cards.Create(identityToken, keyPair.PublicKey(), keyPair.PrivateKey());

            var privateKeysClient = ServiceLocator.Services.PrivateKeys;

            await privateKeysClient.Stash(card.Id, keyPair.PrivateKey(), privateKeyPassword);
            var grabResponse = await privateKeysClient.Get(card.Id, identityToken);

            grabResponse.PrivateKey.Should().BeEquivalentTo(keyPair.PrivateKey());

            var virgilCipher1 = new VirgilCipher();
            var virgilCipher2 = new VirgilCipher();
            
            virgilCipher1.AddKeyRecipient(Encoding.UTF8.GetBytes(card.Id.ToString()), keyPair.PublicKey());

            var expected = "TEST";
            var encrypt = virgilCipher1.Encrypt(Encoding.UTF8.GetBytes(expected), true);
            
            var decrypt = virgilCipher2.DecryptWithKey(
                encrypt,
                Encoding.UTF8.GetBytes(card.Id.ToString()), 
                grabResponse.PrivateKey,
                privateKeyPassword.GetBytes());

            Encoding.UTF8.GetString(decrypt).Should().Be(expected);
        }

        [Test]
        public async Task ShouldAllowToUploadKeyForUnconfirmedCard()
        {
            var keyPair = VirgilKeyPair.Generate();
            var card = await ServiceLocator.Services.Cards.Create(Mailinator.GetRandomEmailName(), IdentityType.Email,
                keyPair.PublicKey(), keyPair.PrivateKey());

            await ServiceLocator.Services.PrivateKeys.Stash(card.Id, keyPair.PrivateKey());
        }

        [Test]
        public async Task ShouldStoreUnconfirmedCardPrivateKey()
        {
            var email = Mailinator.GetRandomEmailName();

            var keyPair = VirgilKeyPair.Generate();

            var createdCard = await ServiceLocator.Services.Cards
                .Create(email, IdentityType.Email, keyPair.PublicKey(), keyPair.PrivateKey());

            await ServiceLocator.Services.PrivateKeys.Stash(createdCard.Id, keyPair.PrivateKey());

            var responceVerification = await ServiceLocator.Services.Identity.Verify(email, IdentityType.Email);

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(email);
            var identityToken = await ServiceLocator.Services.Identity.Confirm(responceVerification.ActionId, confirmationCode);

            await ServiceLocator.Services.PrivateKeys.Get(createdCard.Id, identityToken);
        }
    }
}