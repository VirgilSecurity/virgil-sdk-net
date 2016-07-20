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

            var keyPair = VirgilKeyPair.Generate();

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

            var keyPair = VirgilKeyPair.Generate();

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

            var virgilKeyPair = new VirgilKeyPair();
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
            
            var keyPair = VirgilKeyPair.Generate();

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
            
            var keyPair = VirgilKeyPair.Generate();

            var createdCard = await serviceHub.Cards.Create(confirmedInfo, keyPair.PublicKey(), keyPair.PrivateKey());

            createdCard.Should().NotBeNull();
            createdCard.Identity.Value.Should().Be(confirmedInfo.Value);
            createdCard.Identity.Type.Should().Be(confirmedInfo.Type);
            createdCard.AuthorizedBy.Should().Be(EnvironmentVariables.AppBundleId);
            createdCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }

        [Test]
        public async Task Create_ConfirmedHasedIdentityAsParameter_SuccessfullyCreatedCard()
        {
            var serviceHub = ServiceHubHelper.Create();

            var hashedIdentity = Obfuscator.PBKDF(Mailinator.GetRandomEmailName(), "724fTy6JmZxTNuM7");

            var validationToken = ValidationTokenGenerator.Generate(hashedIdentity, "some_type",
                EnvironmentVariables.AppPrivateKey, EnvironmentVariables.AppPrivateKeyPassword);

            IdentityInfo identityInfo = new IdentityInfo {
                Value = hashedIdentity,
                ValidationToken = validationToken,
                Type = "some_type"
            };
            
            var keyPair = VirgilKeyPair.Generate();

            var createdCard = await serviceHub.Cards.Create(identityInfo, keyPair.PublicKey(), keyPair.PrivateKey());

            createdCard.Should().NotBeNull();
            createdCard.Identity.Value.Should().Be(identityInfo.Value);
            createdCard.Identity.Type.Should().Be(identityInfo.Type);
            createdCard.AuthorizedBy.Should().Be(EnvironmentVariables.AppBundleId);
            createdCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }

        [Test]
        public async Task Search_HasedIdentityAsParameter_ListOfFoundCards()
        {
            var serviceHub = ServiceHubHelper.Create();
            
            var identityValue = Guid.NewGuid().ToString();
            var hashedIdentityValue = Obfuscator.PBKDF(identityValue, "724fTy6JmZxTNuM7");

            var validationToken = ValidationTokenGenerator.Generate(hashedIdentityValue, "some_type",
               EnvironmentVariables.AppPrivateKey, EnvironmentVariables.AppPrivateKeyPassword);

            var identity = new IdentityInfo
            {
                Value = hashedIdentityValue,
                Type = "some_type",
                ValidationToken = validationToken
            };

            var keyPair = VirgilKeyPair.Generate();

            await serviceHub.Cards.Create(identity, keyPair.PublicKey(), keyPair.PrivateKey());

            var foundCards = (await serviceHub.Cards.Search(hashedIdentityValue)).ToList();
            foundCards.Should().HaveCount(1);
            var theCard = foundCards.ToList().Single();

            theCard.Should().NotBeNull();
            theCard.Identity.Value.Should().Be(identity.Value);
            theCard.Identity.Type.Should().Be(identity.Type);
            theCard.AuthorizedBy.Should().Be(EnvironmentVariables.AppBundleId);
            theCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }
    }
}