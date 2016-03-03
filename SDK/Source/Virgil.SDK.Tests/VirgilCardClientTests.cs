namespace Virgil.SDK.Keys.Tests
{
    using Virgil.Crypto;
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using FluentAssertions;
    using Newtonsoft.Json;
    using SDK.Domain;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    public class VirgilCardClientTests
    {
        [SetUp]
        public void Init()
        {
            ServiceLocator.SetupForTests();
        }

        [Test]
        public async Task ShouldBeAbleToGetCardById()
        {
            var hub = ServiceLocator.Services;

            var randomEmail = Mailinator.GetRandomEmailName();
            var validationToken = await Utils.GetConfirmedToken(randomEmail);

            var keyPair = VirgilKeyPair.Generate();

            var createdCard = await hub.Cards.Create(validationToken, keyPair.PublicKey(), keyPair.PrivateKey());

            var card = await hub.Cards.Get(createdCard.Id);
            card.Id.Should().Be(createdCard.Id);
        }

        [Test]
        public async Task ShouldBeAbleToRevokeCard()
        {
            var hub = ServiceLocator.Services;

            var randomEmail = Mailinator.GetRandomEmailName();
            var validationToken = await Utils.GetConfirmedToken(randomEmail, ctl:2);

            var keyPair = VirgilKeyPair.Generate();

            var card = await hub.Cards.Create(validationToken, keyPair.PublicKey(), keyPair.PrivateKey());

            var foundCards = await hub.Cards.Search(randomEmail);
            foundCards.Count().Should().Be(1);

            await hub.Cards.Revoke(card.Id, validationToken, keyPair.PrivateKey());

            foundCards = await hub.Cards.Search(randomEmail);
            foundCards.Count().Should().Be(0);
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
                customData: customData);

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

            var sign = await client.Trust(
                c1.VirgilCard.Id, 
                c1.VirgilCard.Hash,

                c2.VirgilCard.Id, 
                c2.VirgilKeyPair.PrivateKey());

            sign.SignedVirgilCardId.Should().Be(c1.VirgilCard.Id);
            sign.SignerVirgilCardId.Should().Be(c2.VirgilCard.Id);

            await client.Untrust(
                c1.VirgilCard.Id,
                c2.VirgilCard.Id,
                c2.VirgilKeyPair.PrivateKey());
        }

        [Test]
        public async Task ShouldBeAbleToSearch()
        {
            var client = ServiceLocator.Services.Cards;

            var c1 = await client.TestCreateVirgilCard();

            var result = await client.Search(c1.VirgilCard.Identity.Value, includeUnconfirmed:true);

            result.Count().Should().Be(1);
            result.First().Id.Should().Be(c1.VirgilCard.Id);
        }

        [Test]
        public async Task Should_Search()
        {
            var virgilHub = VirgilConfig.UseAccessToken("eyJpZCI6ImZhMTYxMGFkLTRjMGYtNGM5MS1hM2RhLTg5Yjk4NzQ2ZjE3YSIsImFwcGxpY2F0aW9uX2NhcmRfaWQiOiJjMDI0NmNhNC0wMTE0LTQ2OTQtYWIzNi1jNDdlNGMwZDAzYWIiLCJ0dGwiOi0xLCJjdGwiOi0xLCJwcm9sb25nIjowfQ==.MIGaMA0GCWCGSAFlAwQCAgUABIGIMIGFAkAKU6Wp1RsVEBiqNZeHvTbjJGRgeYYn23exVld/FIFOjSyjtCEWu+tQIBKgo1cMMUl3og/5evl1EfEjeZBN2myDAkEAl5odVSqje/XGqHwVfP0QmuChduJ7xXW2MxVgJme95AIHSNDCXwmidK9ny6IZ5LZPUO45L4Z0P5GQ4i2oDrfdkA==")
                   .WithCustomIdentityServiceUri(new Uri("https://identity-stg.virgilsecurity.com/"))
                   .WithCustomPublicServiceUri(new Uri("https://keys-stg.virgilsecurity.com/"))
                   .Build();

            var cards = await virgilHub.Cards.Search("kurilenkodenis@gmail.com");
        }
    }
}