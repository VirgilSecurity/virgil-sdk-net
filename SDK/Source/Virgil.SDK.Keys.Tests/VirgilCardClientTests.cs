
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
    using Infrastructurte;
    using Keys.Domain;
    
    public class VirgilCardClientTests
    {
        [SetUp]
        public void Init()
        {
            ServiceLocator.SetupForTests();
        }

        [Test]
        public async Task ShouldBeAbleToCreateNewVirgilCard()
        {
            var client = ServiceLocator.Services.Cards;

            var virgilKeyPair = new VirgilKeyPair();
            var email = Mailinator.GetRandomEmailName();
            var customData = new Dictionary<string,string>
            {
                ["hello"] = "world",
                ["created-guid"] = Guid.NewGuid().ToString()
            };

            VirgilCardDto virgilCard = await client.Create(
                email,
                IdentityType.Email,
                virgilKeyPair.PublicKey(),
                virgilKeyPair.PrivateKey(),
                customData);

            virgilCard.Identity.Value.Should().BeEquivalentTo(email);
            virgilCard.PublicKey.PublicKey.ShouldAllBeEquivalentTo(virgilKeyPair.PublicKey());
            virgilCard.CustomData.ShouldAllBeEquivalentTo(customData);
        }
        
        [Test]
        public async Task ShouldBeAbleToAttachToExistingVirgilCard()
        {
            var client = ServiceLocator.Services.Cards;

            var batch = await client.TestCreateVirgilCard();

            var attached = await client.Create(
                Mailinator.GetRandomEmailName(),
                IdentityType.Email,
                batch.VirgilCard.PublicKey.Id,
                batch.VirgilKeyPair.PrivateKey());

            attached.PublicKey.Id.Should().Be(batch.VirgilCard.PublicKey.Id);
            attached.PublicKey.PublicKey.ShouldAllBeEquivalentTo(batch.VirgilCard.PublicKey.PublicKey);
        }

        [Test]
        public async Task ShouldBeAbleToSignAndUnsignVirgilCard()
        {
            var client = ServiceLocator.Services.Cards;

            var c1 = await client.TestCreateVirgilCard();
            var c2 = await client.TestCreateVirgilCard();

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
            var client = ServiceLocator.Services.Cards;

            var c1 = await client.TestCreateVirgilCard();

            var result = await client.Search(
                value: c1.VirgilCard.Identity.Value,
                type: IdentityType.Email, 
                relations: null, 
                includeUnconfirmed: true);

            result.Count.Should().Be(1);

            result.First().Id.Should().Be(c1.VirgilCard.Id);
        }
    }
}