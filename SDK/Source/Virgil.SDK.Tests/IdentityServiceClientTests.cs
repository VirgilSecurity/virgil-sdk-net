namespace Virgil.SDK.Keys.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;

    using HtmlAgilityPack;
    using NUnit.Framework;

    using Virgil.SDK.Identities;

    public class IdentityServiceClientTests
    {
        [Test]
        public async Task ShouldBeAbleToSendVerificationRequestWithExtraFields()
        {
            var serviceHub = ServiceHubHelper.Create();

            var mail = Mailinator.GetRandomEmailName();

            var identityBuilder = serviceHub.Identity.BuildEmail(mail);
            await identityBuilder.Verify(new Dictionary<string, string>
            {
                {"extra_field_1", "bugaga"},
                {"extra_field_2", "bugagagaga"}
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

            var identityBuilder = serviceHub.Identity.BuildEmail(mail);

            await identityBuilder.Verify();
            await Task.Delay(2500);
            var email = await Mailinator.GetLatestEmail(mail);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(email.parts.First().body);

            var code = htmlDoc.GetElementbyId("confirmation_code").GetAttributeValue("value", null);
            await identityBuilder.Confirm(code);
        }

        [Test]
        public async Task ShouldBeAbleToVerifyToken()
        {
            var serviceHub = ServiceHubHelper.Create();

            var mail = Mailinator.GetRandomEmailName();

            var identityBuilder = serviceHub.Identity.BuildEmail(mail);

            await identityBuilder.Verify();

            var code = await Mailinator.GetConfirmationCodeFromLatestEmail(mail, true);

            await identityBuilder.Confirm(code);

            (await serviceHub.Identity.IsValid(identityBuilder.GetIdentity().Value, VerifiableIdentityType.Email, identityBuilder.GetIdentity().ValidationToken)).Should().Be(true);
        }
    }
}