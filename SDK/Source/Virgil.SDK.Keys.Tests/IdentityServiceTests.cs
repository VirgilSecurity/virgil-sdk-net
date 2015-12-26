namespace Virgil.SDK.Keys.Tests
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Infrastructurte;
    using Keys.Domain;
    using NUnit.Framework;
    using TransferObject;

    public class IdentityServiceTests
    {
        [SetUp]
        public void Init()
        {
            ServiceLocator.SetupForTests();
        }

        [Test]
        public async Task ShouldBeAbleToVerifyToken()
        {
            var client = ServiceLocator.Services.Identity;

            var mail = Mailinator.GetRandomEmailName();

            var virgilVerifyResponse = await client.Verify(mail, IdentityType.Email);
            await Task.Delay(2500);

            var code = await Mailinator.GetConfirmationCodeFromLatestEmail(mail);

            var virgilIndentityToken = await client.Confirm(code, virgilVerifyResponse.ActionId);
            (await client.IsValid(virgilIndentityToken)).Should().Be(true);
        }
    }
}