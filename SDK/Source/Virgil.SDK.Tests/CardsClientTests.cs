namespace Virgil.SDK.Keys.Tests
{
    using Virgil.Crypto;
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using FluentAssertions;

    using Virgil.SDK.Identities;
    using Virgil.SDK.Utils;

    public class CardsClientTests
    {
        [Test]
        public async Task ShouldBeAbleToGetCardById()
        {
            var serviceHub = ServiceHubHelper.Create();

            var email = Mailinator.GetRandomEmailName();
            var identityInfo = await Utils.GetConfirmedToken(email);

            var keyPair = CryptoHelper.GenerateKeyPair();

            var createdCard = await serviceHub.Cards
                .Create(identityInfo, keyPair.PublicKey(), keyPair.PrivateKey());

            var card = await serviceHub.Cards.Get(createdCard.Id);
            card.Id.Should().Be(createdCard.Id);
        }

        [Test]
        public async Task ShouldBeAbleToRevokeCard()
        {
            var hub = ServiceHubHelper.Create();

            var randomEmail = Mailinator.GetRandomEmailName();
            var validationToken = await Utils.GetConfirmedToken(randomEmail, ctl:2);

            var keyPair = CryptoHelper.GenerateKeyPair();

            var card = await hub.Cards.Create(validationToken, keyPair.PublicKey(), keyPair.PrivateKey());

            var foundCards = await hub.Cards.Search(randomEmail, IdentityType.Email);
            foundCards.Count().Should().Be(1);

            await hub.Cards.Revoke(card.Id, validationToken, keyPair.PrivateKey());

            foundCards = await hub.Cards.Search(randomEmail, IdentityType.Email);
            foundCards.Count().Should().Be(0);
        }

        [Test]
        public async Task ShouldBeAbleToCreateNewPrivateVirgilCard()
        {
            var hub = ServiceHubHelper.Create();

            var virgilKeyPair = CryptoHelper.GenerateKeyPair();
            var email = Mailinator.GetRandomEmailName();
            var customData = new Dictionary<string,string>
            {
                ["hello"] = "world",
                ["created-guid"] = Guid.NewGuid().ToString()
            };

            var card = await hub.Cards.Create(
                new IdentityInfo { Value = email, Type = "email" }, 
                virgilKeyPair.PublicKey(),
                virgilKeyPair.PrivateKey(),
                customData: customData);

            card.Identity.Value.Should().BeEquivalentTo(email);
            card.PublicKey.Value.ShouldAllBeEquivalentTo(virgilKeyPair.PublicKey());
            card.CustomData.ShouldAllBeEquivalentTo(customData);
        }
        
        [Test]
        public async Task ShouldBeAbleToAttachToExistingVirgilCard()
        {
            var hub = ServiceHubHelper.Create();

            var batch = await hub.Cards.TestCreateVirgilCard();

            var attached = await hub.Cards.Create(
                new IdentityInfo { Value = Mailinator.GetRandomEmailName(), Type = "email" },
                batch.VirgilCard.PublicKey.Id,
                batch.VirgilKeyPair.PrivateKey());

            attached.PublicKey.Id.Should().Be(batch.VirgilCard.PublicKey.Id);
            attached.PublicKey.Value.ShouldAllBeEquivalentTo(batch.VirgilCard.PublicKey.Value);
        }
        
        [Test]
        public async Task ShouldBeAbleToSearch()
        {
            var client = ServiceHubHelper.Create().Cards;

            var c1 = await client.TestCreateVirgilCard();

            var result = await client.Search(c1.VirgilCard.Identity.Value, includeUnauthorized:true);

            result.Count().Should().Be(1);
            result.First().Id.Should().Be(c1.VirgilCard.Id);
        }

        [Test]
        public async Task Create_UnconfirmedCustomIdentityAsParameter_SuccessfullyCreatedCard()
        {
            var serviceHub = ServiceHubHelper.Create();
            
            var keyPair = CryptoHelper.GenerateKeyPair();

            var identityInfo = new IdentityInfo
            {
                Value = Mailinator.GetRandomEmailName(),
                Type = "some_type"
            };

            var createdCard = await serviceHub.Cards.Create(identityInfo, keyPair.PublicKey(), keyPair.PrivateKey());
            
            createdCard.Should().NotBeNull();
            createdCard.Identity.Value.Should().Be(identityInfo.Value);
            createdCard.Identity.Type.Should().Be(identityInfo.Type);
            createdCard.AuthorizedBy.Should().BeNullOrEmpty();
            createdCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }
        
        [Test]
        public async Task Create_ConfirmedCustomIdentityAsParameter_SuccessfullyCreatedCard()
        {
            var serviceHub = ServiceHubHelper.Create();
            
            var email = Mailinator.GetRandomEmailName();
            
            var confirmedInfo = new IdentityInfo
            {
                Value = email,
                Type = "some_type",
                ValidationToken = ValidationTokenGenerator.Generate(email, "some_type",
                    EnvironmentVariables.AppPrivateKey, EnvironmentVariables.AppPrivateKeyPassword)
            };
            
            var keyPair = CryptoHelper.GenerateKeyPair();

            var createdCard = await serviceHub.Cards.Create(confirmedInfo, keyPair.PublicKey(), keyPair.PrivateKey());

            createdCard.Should().NotBeNull();
            createdCard.Identity.Value.Should().Be(confirmedInfo.Value);
            createdCard.Identity.Type.Should().Be(confirmedInfo.Type);
            createdCard.AuthorizedBy.Should().Be(EnvironmentVariables.AppBundleId);
            createdCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }
    }
}