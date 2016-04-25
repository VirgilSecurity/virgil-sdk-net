namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Crypto;
    using Exceptions;

    using FluentAssertions;
    using Models;
    using NUnit.Framework;
    
    public class PrivateKeysClientTests
    {
        [Test]
        public async Task FullPrivateKeyLifeCycleTest()
        {
            var serviceHub = ServiceHubHelper.Create();

            var emailName = Mailinator.GetRandomEmailName();
            var emailVerifier = await serviceHub.Identity.VerifyEmail(emailName);

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName, true);
            var identityToken = await emailVerifier.Confirm(confirmationCode, 300, 2);

            var keyPair = VirgilKeyPair.Generate();
            var card = await serviceHub.Cards.CreateConfirmed(identityToken, keyPair.PublicKey(), keyPair.PrivateKey());
            
            var privateKeysClient = serviceHub.PrivateKeys;

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
            var serviceHub = ServiceHubHelper.Create();

            var emailName = Mailinator.GetRandomEmailName();
            var verifier = await serviceHub.Identity.VerifyEmail(emailName);

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName, true);
            var identityToken = await verifier.Confirm(confirmationCode, 300, 2);

            var privateKeyPassword = "PASSWORD";

            var keyPair = VirgilKeyPair.Generate();
            var card = await serviceHub.Cards.CreateConfirmed(identityToken, keyPair.PublicKey(), keyPair.PrivateKey());

            var privateKeysClient = serviceHub.PrivateKeys;

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
                Encoding.UTF8.GetBytes(privateKeyPassword));

            Encoding.UTF8.GetString(decrypt).Should().Be(expected);
        }

        [Test]
        public async Task ShouldAllowToUploadKeyForUnconfirmedCard()
        {
            var serviceHub = ServiceHubHelper.Create();

            var keyPair = VirgilKeyPair.Generate();
            var card = await serviceHub.Cards.Create(new IdentityInfo(Mailinator.GetRandomEmailName(), IdentityType.Email),
                keyPair.PublicKey(), keyPair.PrivateKey());

            await serviceHub.PrivateKeys.Stash(card.Id, keyPair.PrivateKey());
        }

        [Test]
        public async Task ShouldStoreUnconfirmedCardPrivateKey()
        {
            var serviceHub = ServiceHubHelper.Create();

            var email = Mailinator.GetRandomEmailName();

            var keyPair = VirgilKeyPair.Generate();

            var createdCard = await serviceHub.Cards
                .Create(new IdentityInfo(email, IdentityType.Email), keyPair.PublicKey(), keyPair.PrivateKey());

            await serviceHub.PrivateKeys.Stash(createdCard.Id, keyPair.PrivateKey());

            var emailVerifier = await serviceHub.Identity.VerifyEmail(email);

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(email);
            var identityToken = await emailVerifier.Confirm(confirmationCode);

            await serviceHub.PrivateKeys.Get(createdCard.Id, identityToken);
        }
    }
}