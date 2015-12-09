
using FluentAssertions;

namespace Virgil.SDK.Keys.Tests
{
    using Virgil.SDK.Keys.Clients;
    using Virgil.SDK.Keys.TransferObject;
    using Virgil.Crypto;
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;
    
    using Http;

    public static class Constants
    {
        public const string ApplicationToken = "e872d6f718a2dd0bd8cd7d7e73a25f49";
    }

    public class VirgilCardClientTests
    {
        public static readonly PublicKeysConnection ApiEndpoint = new PublicKeysConnection(Constants.ApplicationToken, new Uri(@"https://keys-stg.virgilsecurity.com"));

        [Test]
        public async Task ShouldBeAbleToCreateNewVirgilCard()
        {
            var client = new VirgilCardClient(ApiEndpoint);

            var virgilKeyPair = new VirgilKeyPair();
            var email = GetRandomEmail();
            var customData = new Dictionary<string,string>
            {
                ["hello"] = "world",
                ["created-guid"] = Guid.NewGuid().ToString()
            };

            VirgilCardDto virgilCard = await client.Create(
                virgilKeyPair.PublicKey(),
                VirgilIdentityType.Email,
                email,
                customData, 
                virgilKeyPair.PrivateKey());

            virgilCard.Identity.Value.Should().BeEquivalentTo(email);
            virgilCard.PublicKey.PublicKey.ShouldAllBeEquivalentTo(virgilKeyPair.PublicKey());
            virgilCard.CustomData.ShouldAllBeEquivalentTo(customData);
        }

        [Test]
        public async Task ShouldBeAbleToAttachToExistingVirgilCard()
        {
            var client = new VirgilCardClient(ApiEndpoint);

            var batch = await CreateVirgilCard(client);

            var attached = await client.AttachTo(
                batch.VirgilCard.PublicKey.Id,
                VirgilIdentityType.Email,
                GetRandomEmail(),
                null,
                batch.VirgilCard.Id,
                batch.VirgilKeyPair.PrivateKey());

            attached.PublicKey.Id.Should().Be(batch.VirgilCard.PublicKey.Id);
            attached.PublicKey.PublicKey.ShouldAllBeEquivalentTo(batch.VirgilCard.PublicKey.PublicKey);
        }

        [Test]
        public async Task ShouldBeAbleToSignAndUnsignVirgilCard()
        {
            var client = new VirgilCardClient(ApiEndpoint);

            var c1 = await CreateVirgilCard(client);
            var c2 = await CreateVirgilCard(client);

            var sign = await client.Sign(
                c1.VirgilCard.Id, 
                c1.VirgilCard.Hash,

                c2.VirgilCard.Id, 
                c2.VirgilKeyPair.PrivateKey());

            sign.SignedVirgilCardId.Should().Be(c1.VirgilCard.Id);
            sign.SignerVirgilCardId.Should().Be(c2.VirgilCard.Id);

            await client.Unsign(
                c1.VirgilCard.Id,
                c2.VirgilCard.Id,
                c2.VirgilKeyPair.PrivateKey());
        }

        [Test]
        public async Task ShouldBeAbleToSearch()
        {
            var client = new VirgilCardClient(ApiEndpoint);

            var c1 = await CreateVirgilCard(client);

            var result = await client.Search(
                c1.VirgilCard.Identity.Value,
                null,
                null,
                true,
                c1.VirgilCard.Id,
                c1.VirgilKeyPair.PrivateKey());

            result.Count.Should().Be(1);

            result.First().Id.Should().Be(c1.VirgilCard.Id);
        }

        private static async Task<Batch> CreateVirgilCard(VirgilCardClient client)
        {
            var virgilKeyPair = new VirgilKeyPair();

            VirgilCardDto virgilCard = await client.Create(
                virgilKeyPair.PublicKey(),
                VirgilIdentityType.Email,
                GetRandomEmail(),
                new Dictionary<string,string>()
                {
                    ["hello"] = "world"
                }, 
                virgilKeyPair.PrivateKey());

            return new Batch
            {
                VirgilCard = virgilCard,
                VirgilKeyPair = virgilKeyPair
            };
        }

        public class Batch
        {
            public VirgilCardDto VirgilCard;
            public VirgilKeyPair VirgilKeyPair;
        }

        private static string GetRandomEmail()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToLowerInvariant() + "@mailinator.com";
        }
    }
}