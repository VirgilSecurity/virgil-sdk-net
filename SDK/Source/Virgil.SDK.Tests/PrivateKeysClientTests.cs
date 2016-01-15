namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Threading.Tasks;
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

            grabResponse.PrivateKey.Should().BeEquivalentTo(card.PrivateKey);

            await privateKeysClient.Destroy(card.Id, grabResponse.PrivateKey);
            
            try
            {
                await privateKeysClient.Get(card.Id, identityToken);
                throw new Exception("Test failed");
            }
            catch (VirgilPrivateServicesException exc) when (exc.Message == "Entity not found")
            {
            }
        }

        [Test]
        public async Task ShouldAllowToUploadKeyForUncofirmedCard()
        {
            var card = await PersonalCard.Create(Mailinator.GetRandomEmailName());
            await card.UploadPrivateKey();
        }
    }
}