namespace Virgil.SDK.Keys.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using HtmlAgilityPack;
    using NUnit.Framework;
    using SDK.Domain;
    using Virgil.SDK.TransferObject;

    public class IdentityServiceTests
    {
        [SetUp]
        public void Init()
        {
            ServiceLocator.SetupForTests();
        }

        [Test]
        public async Task ShouldBeAbleToSendVerificationRequestWithExtraFields()
        {
            var client = ServiceLocator.Services.Identity;

            var mail = Mailinator.GetRandomEmailName();

            await client.Verify(mail, IdentityType.Email, new Dictionary<string, string>
            {
                { "extra_field_1", "bugaga" },
                { "extra_field_2", "bugagagaga" }
            });

            await Task.Delay(2500);
            var email = await Mailinator.GetLatestEmail(mail);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(email.parts.First().body);

            var field1 = htmlDoc.GetElementbyId("extra_field_1");
            var field2 = htmlDoc.GetElementbyId("extra_field_2");

            field1.Should().NotBeNull();
            field2.Should().NotBeNull();

            field1.GetAttributeValue("value", null).Should().Be("bugaga");
            field2.GetAttributeValue("value", null).Should().Be("bugagagaga");
        }

        [Test]
        public async Task ShouldSendVerificationRequestWithConfirmationCodeInHiddenInput()
        {
            var client = ServiceLocator.Services.Identity;

            var mail = Mailinator.GetRandomEmailName();

            var virificationToken = await client.Verify(mail, IdentityType.Email);

            await Task.Delay(2500);
            var email = await Mailinator.GetLatestEmail(mail);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(email.parts.First().body);

            var code = htmlDoc.GetElementbyId("confirmation_code").GetAttributeValue("value", null);
            var identityToken = await client.Confirm(virificationToken.ActionId, code);
        }

        [Test]
        public async Task ShouldBeAbleToVerifyToken()
        {
            var client = ServiceLocator.Services.Identity;

            var mail = Mailinator.GetRandomEmailName();

            var virgilVerifyResponse = await client.Verify(mail, IdentityType.Email);
            await Task.Delay(2500);

            var code = await Mailinator.GetConfirmationCodeFromLatestEmail(mail);

            var virgilIndentityToken = await client.Confirm(virgilVerifyResponse.ActionId, code);
            (await client.IsValid(virgilIndentityToken)).Should().Be(true);
        }
    }
}