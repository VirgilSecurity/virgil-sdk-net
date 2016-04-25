namespace Virgil.SDK.Keys.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Clients;
    using FluentAssertions;

    using HtmlAgilityPack;
    using Models;
    using NUnit.Framework;
    
    public class IdentityServiceClientTests
    {
        [Test]
        public async Task ShouldBeAbleToSendVerificationRequestWithExtraFields()
        {
            var serviceHub = ServiceHubHelper.Create();

            var mail = Mailinator.GetRandomEmailName();

            await serviceHub.Identity.VerifyEmail(mail, new Dictionary<string, string>
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
            var serviceHub = ServiceHubHelper.Create();

            var mail = Mailinator.GetRandomEmailName();

            var emailVerifier = await serviceHub.Identity.VerifyEmail(mail);

            await Task.Delay(2500);
            var email = await Mailinator.GetLatestEmail(mail);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(email.parts.First().body);

            var code = htmlDoc.GetElementbyId("confirmation_code").GetAttributeValue("value", null);
            await emailVerifier.Confirm(code);
        }

        [Test]
        public async Task ShouldBeAbleToVerifyToken()
        {
            var serviceHub = ServiceHubHelper.Create();

            var mail = Mailinator.GetRandomEmailName();

            var emailVerifier = await serviceHub.Identity.VerifyEmail(mail);

            var code = await Mailinator.GetConfirmationCodeFromLatestEmail(mail, true);

            var confirmedInfo = await emailVerifier.Confirm(code);
            (await serviceHub.Identity.IsValid(confirmedInfo.Value, VerifiableIdentityType.Email, confirmedInfo.ValidationToken)).Should().Be(true);
        }
    }
}