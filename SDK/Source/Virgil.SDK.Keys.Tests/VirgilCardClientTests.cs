
using FluentAssertions;

namespace Virgil.SDK.Keys.Tests
{
    using Virgil.SDK.Keys.Clients;
    using Virgil.SDK.Keys.TransferObject;
    using Virgil.Crypto;
    
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;
    
    using Http;
    
    public class VirgilCardClientTests
    {
        public static readonly Connection ApiEndpoint = new Connection(new Uri(@"https://keys-stg.virgilsecurity.com"));

        [Test]
        public async Task ShouldBeAbleToCreateKey()
        {
            var client = new VirgilCardClient(ApiEndpoint);

            var virgilKeyPair = new VirgilKeyPair();

            var virgilCardId = Guid.NewGuid();

            var email = "somemeail@mailinator.com";

            VirgilCardDto virgilCard = await client.Create(
                virgilKeyPair.PublicKey(),
                VirgilIdentityType.Email,
                email,
                null,
                virgilCardId,
                virgilKeyPair.PrivateKey());

            virgilCard.PublicKey.PublicKey.ShouldAllBeEquivalentTo(virgilKeyPair.PublicKey());
            virgilCard.Id.Should().Be(virgilCardId);

            virgilCard.Identity.Value.Should().BeEquivalentTo(email);
        }
    }
}