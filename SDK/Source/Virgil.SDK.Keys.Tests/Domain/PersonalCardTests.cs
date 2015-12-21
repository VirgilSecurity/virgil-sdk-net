namespace Virgil.SDK.Keys.Tests.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using TransferObject;
    using Virgil.SDK.Keys.Domain;

    public class PersonalCardTests
    {
        [Test]
        public async Task ShouldCreateConfirmedVirgilCard()
        {
            var emailName = Mailinator.GetRandomEmailName();

            var request = await card12312.Identity.Verify();

            await Task.Delay(2000);

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(emailName);

            var identityProof = await request.Confirm(confirmationCode);
            var card = await PersonalCard.Create(identityProof);

            var encrypt = card.Encrypt("Hello");
            var decrypt = card.Decrypt(encrypt);

            decrypt.Should().Be("Hello");
        }

        [Test]
        public async Task ShouldCreateUnConfirmedVirgilCard()
        {
            var emailName = Mailinator.GetRandomEmailName();
            
            var card = await PersonalCard.Create(emailName, new Dictionary<string, string>
            {
                ["hello"] = "world"
            });

            var encrypt = card.Encrypt("Hello");
            var decrypt = card.Decrypt("123123" + encrypt);

            decrypt.Should().Be("Hello");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldThrowArgumentExceptionWhenHeaderIsMalformed()
        {
            var emailName = Mailinator.GetRandomEmailName();

            var card = await PersonalCard.Create(emailName, new Dictionary<string, string>
            {
                ["hello"] = "world"
            });

            var encrypt = card.Encrypt("Hello");
            var decrypt = card.Decrypt(Convert.ToBase64String(Encoding.UTF8.GetBytes("123123" + encrypt)));
        }

        [Test]
        public async Task ShouldExportImportPersonalCard()
        {
            var emailName = Mailinator.GetRandomEmailName();

            PersonalCard createdCard = await PersonalCard.Create(emailName, new Dictionary<string, string>
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

            PersonalCard createdCard = await PersonalCard.Create(emailName, new Dictionary<string, string>
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