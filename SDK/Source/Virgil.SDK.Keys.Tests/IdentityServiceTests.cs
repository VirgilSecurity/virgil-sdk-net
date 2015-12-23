namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Clients;
    using Clients.Authority;
    using FluentAssertions;
    using Http;
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
            var connection1 = new IdentityConnection(new Uri(@"https://identity-stg.virgilsecurity.com"));
            var connection = new VerifiedConnection(connection1, new KnownKeyProvider(null), new VirgilServiceResponseVerifier());

            var client = new IdentityService(connection);

            var mail = Mailinator.GetRandomEmailName();

            var virgilVerifyResponse = await client.Verify(mail, IdentityType.Email);
            await Task.Delay(2500);

            var code = await Mailinator.GetConfirmationCodeFromLatestEmail(mail);

            var virgilIndentityToken = await client.Confirm(code, virgilVerifyResponse.ActionId);
            (await client.IsValid(virgilIndentityToken)).Should().Be(true);
        }
    }
}