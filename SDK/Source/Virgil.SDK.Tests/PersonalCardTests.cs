namespace Virgil.SDK.Keys.Tests.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Clients;
    using Crypto;
    using FluentAssertions;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using TransferObject;
    using Virgil.SDK.Domain;
    using Virgil.SDK.Infrastructure;

    public class PersonalCardTests
    {
        [SetUp]
        public void Init()
        {
            ServiceLocator.SetupForTests();
        }

        [Test]
        public async Task ShouldBeAbleToGetAllCardsForApplicationIds()
        {
            var virgilCardsClient = ServiceLocator.Services.Cards;
            var results = new Dictionary<string, object>
            {
                [VirgilApplicationIds.PrivateService] =
                    (await virgilCardsClient.GetApplicationCard(VirgilApplicationIds.PrivateService)).First(),
                [VirgilApplicationIds.PublicService] =
                    (await virgilCardsClient.GetApplicationCard(VirgilApplicationIds.PublicService)).First(),
                [VirgilApplicationIds.IdentityService] =
                    (await virgilCardsClient.GetApplicationCard(VirgilApplicationIds.IdentityService)).First()
            };

            var json = JsonConvert.SerializeObject(results);
            Console.WriteLine(json);
        }

        [Test]
        public async Task ShouldCreateConfirmedVirgilCard()
        {
            var emailName = Mailinator.GetRandomEmailName();

            var request = await Identity.Verify(emailName);

            await Task.Delay(2000);
            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName);

            var identityToken = await request.Confirm(confirmationCode, new ConfirmOptions(300, 2));
            var card = await PersonalCard.Create(identityToken);

            var encrypt = card.Encrypt("Hello");
            var decrypt = card.Decrypt(encrypt);

            decrypt.Should().Be("Hello");
        }

        [Test]
        public async Task ShouldCreateConfirmedVirgilCardAndUploadPrivateKey()
        {
            var emailName = Mailinator.GetRandomEmailName();
            var request = await Identity.Verify(emailName);
            await Task.Delay(1000);
            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName);
            var identityToken = await request.Confirm(confirmationCode, new ConfirmOptions(3600, 15));
            var card = await PersonalCard.Create(identityToken);

            await card.UploadPrivateKey();
            var grabResponse = await ServiceLocator.Services.PrivateKeys.Get(card.Id, identityToken);
            await ServiceLocator.Services.PrivateKeys.Destroy(card.Id, grabResponse.PrivateKey);
        }

        [Test]
        public async Task ShouldCreateConfirmedVirgilCardAndUploadPrivateKeyWithPassword()
        {
            string password = "123123123";

            var emailName = Mailinator.GetRandomEmailName();
            var request = await Identity.Verify(emailName);
            await Task.Delay(1000);
            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName);
            var identityToken = await request.Confirm(confirmationCode, new ConfirmOptions(3600, 15));
            var card = await PersonalCard.Create(identityToken, password);

            await card.UploadPrivateKey(password);
            var grabResponse = await ServiceLocator.Services.PrivateKeys.Get(card.Id, identityToken);
            await ServiceLocator.Services.PrivateKeys.Destroy(card.Id, grabResponse.PrivateKey, password);
        }


        [Test]
        public async Task ShouldCreateVirgilCardAndBeAbleToDownloadIt()
        {
            var identityToken = await Utils.GetConfirmedToken();
            var card = await PersonalCard.Create(identityToken);
            await card.UploadPrivateKey();

            var loader = await PersonalCard.BeginLoadAll(card.Identity.Value, card.Identity.Type);
            var request = await loader.Verify();
            
            await Task.Delay(2000);

            var code = await Mailinator.GetConfirmationCodeFromLatestEmail(request.Identity);

            var cards = await loader.Finish(request, code);

            var personalCards = cards.ToArray();

            personalCards.Length.Should().Be(1);
            personalCards[0].ShouldBeEquivalentTo(card);
        }

        [Test]
        public async Task ShouldCreateVirgilCardAndBeAbleToDownloadLatest()
        {
            var identityToken = await Utils.GetConfirmedToken();
            var card1 = await PersonalCard.Create(identityToken);
            await card1.UploadPrivateKey();

            var card2 = await PersonalCard.LoadLatest(await Utils.GetConfirmedToken(identityToken.Value));
            
            card2.ShouldBeEquivalentTo(card1);
        }

        [Test]
        public async Task ShouldCreateUnConfirmedVirgilCard()
        {
            var emailName = Mailinator.GetRandomEmailName();
            
            var card = await PersonalCard.Create(emailName, customData: new Dictionary<string, string>
            {
                ["hello"] = "world"
            });

            var encrypt = card.Encrypt("Hello");
            var decrypt = card.Decrypt(encrypt);

            decrypt.Should().Be("Hello");
        }

        [Test]
        public async Task ShouldThrowArgumentExceptionWhenHeaderIsMalformed()
        {
            var emailName = Mailinator.GetRandomEmailName();

            var card = await PersonalCard.Create(emailName, customData: new Dictionary<string, string>
            {
                ["hello"] = "world"
            });

            var encrypt = card.Encrypt("Hello");
            Assert.Throws<ArgumentException>(
                () => card.Decrypt(Convert.ToBase64String(Encoding.UTF8.GetBytes("123123" + encrypt))));
            
        }

        [Test]
        public async Task ShouldExportImportPersonalCard()
        {
            var emailName = Mailinator.GetRandomEmailName();

            PersonalCard createdCard = await PersonalCard.Create(emailName, customData: new Dictionary<string, string>
            {
                ["hello"] = "world"
            });

            var export = createdCard.Export();

            PersonalCard restoredCard = PersonalCard.Import(export);

            createdCard.ShouldBeEquivalentTo(restoredCard);
        }

        [Test]
        public async Task ShouldExportImportPersonalCardWithPassword()
        {
            var emailName = Mailinator.GetRandomEmailName();

            PersonalCard createdCard = await PersonalCard.Create(emailName, customData: new Dictionary<string, string>
            {
                ["hello"] = "world"
            });

            var export = createdCard.Export("Password");

            PersonalCard restoredCard = PersonalCard.Import(export, "Password");

            createdCard.ShouldBeEquivalentTo(restoredCard);
        }
        
        [Test]
        public async Task ShouldExportImportPersonalCardWithPassword2()
        {
            var cards = await Cards.PrepareSearch("valera").WithUnconfirmed(true).Execute();
            cards.Encrypt("Tolik");
        }

    }
}