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
    using Virgil.SDK.Models;
    using Virgil.SDK.Utils;

    public class VirgilCardClientTests
    {
        [Test]
        public async Task ShouldBeAbleToGetCardById()
        {
            var hub = ServiceHubHelper.Create();

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
            var hub = ServiceHubHelper.Create();

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
            var hub = ServiceHubHelper.Create();

            var virgilKeyPair = new VirgilKeyPair();
            var email = Mailinator.GetRandomEmailName();
            var customData = new Dictionary<string,string>
            {
                ["hello"] = "world",
                ["created-guid"] = Guid.NewGuid().ToString()
            };

            CardModel virgilCard = await hub.Cards.Create(
                IdentityInfo.Email(email), 
                virgilKeyPair.PublicKey(),
                virgilKeyPair.PrivateKey(),
                customData: customData);

            virgilCard.Identity.Value.Should().BeEquivalentTo(email);
            virgilCard.PublicKey.Value.ShouldAllBeEquivalentTo(virgilKeyPair.PublicKey());
            virgilCard.CustomData.ShouldAllBeEquivalentTo(customData);
        }
        
        [Test]
        public async Task ShouldBeAbleToAttachToExistingVirgilCard()
        {
            var hub = ServiceHubHelper.Create();

            var batch = await hub.Cards.TestCreateVirgilCard();

            var attached = await hub.Cards.Create(
                IdentityInfo.Email(Mailinator.GetRandomEmailName()),
                batch.VirgilCard.PublicKey.Id,
                batch.VirgilKeyPair.PrivateKey());

            attached.PublicKey.Id.Should().Be(batch.VirgilCard.PublicKey.Id);
            attached.PublicKey.Value.ShouldAllBeEquivalentTo(batch.VirgilCard.PublicKey.Value);
        }

        [Test]
        public async Task ShouldBeAbleToSignAndUnsignVirgilCard()
        {
            var client = ServiceHubHelper.Create().Cards;

            var c1 = await client.TestCreateVirgilCard();
            var c2 = await client.TestCreateVirgilCard();

            var sign = await client.Trust(
                c1.VirgilCard.Id, 
                c1.VirgilCard.Hash,

                c2.VirgilCard.Id, 
                c2.VirgilKeyPair.PrivateKey());

            sign.SignedCardId.Should().Be(c1.VirgilCard.Id);
            sign.SignerCardId.Should().Be(c2.VirgilCard.Id);

            await client.Untrust(
                c1.VirgilCard.Id,
                c2.VirgilCard.Id,
                c2.VirgilKeyPair.PrivateKey());
        }

        [Test]
        public async Task ShouldBeAbleToSearch()
        {
            var client = ServiceHubHelper.Create().Cards;

            var c1 = await client.TestCreateVirgilCard();

            var result = await client.Search(c1.VirgilCard.Identity.Value, includeUnconfirmed:true);

            result.Count().Should().Be(1);
            result.First().Id.Should().Be(c1.VirgilCard.Id);
        }

        [Test]
        public async Task Create_UnconfirmedCustomIdentityAsParameter_SuccessfullyCreatedCard()
        {
            var serviceHub = ServiceHubHelper.Create();
            
            var keyPair = VirgilKeyPair.Generate();

            var identityInfo = new IdentityInfo
            (
                Mailinator.GetRandomEmailName(),
                IdentityType.Custom
            );

            var createdCard = await serviceHub.Cards.Create(identityInfo, keyPair.PublicKey(), keyPair.PrivateKey());
            
            createdCard.Should().NotBeNull();
            createdCard.Identity.Value.Should().Be(identityInfo.Value);
            createdCard.Identity.Type.Should().Be(identityInfo.Type);
            createdCard.IsConfirmed.Should().BeFalse();
            createdCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }
        
        [Test]
        public async Task Create_ConfirmedCustomIdentityAsParameter_SuccessfullyCreatedCard()
        {
            var serviceHub = ServiceHubHelper.Create();
            
            var email = Mailinator.GetRandomEmailName();
            
            var confirmedInfo = new IdentityInfo
            (
                email,
                IdentityType.Custom,
                ValidationTokenGenerator.Generate(email, IdentityType.Custom, EnvironmentVariables.ApplicationPrivateKey, "z13x24")
            );
            
            var keyPair = VirgilKeyPair.Generate();

            var createdCard = await serviceHub.Cards.Create(confirmedInfo, keyPair.PublicKey(), keyPair.PrivateKey());

            createdCard.Should().NotBeNull();
            createdCard.Identity.Value.Should().Be(confirmedInfo.Value);
            createdCard.Identity.Type.Should().Be(confirmedInfo.Type);
            createdCard.IsConfirmed.Should().BeTrue();
            createdCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }

        [Test]
        public async Task Create_ConfirmedHasedIdentityAsParameter_SuccessfullyCreatedCard()
        {
            var serviceHub = ServiceHubHelper.Create();

            var hashedIdentity = Obfuscator.Derive(Mailinator.GetRandomEmailName(), "724fTy6JmZxTNuM7");

            var validationToken = ValidationTokenGenerator.Generate(hashedIdentity, IdentityType.Custom,
                EnvironmentVariables.ApplicationPrivateKey, "z13x24");

            IdentityInfo identityInfo = IdentityInfo.Custom(hashedIdentity, validationToken);
            
            var keyPair = VirgilKeyPair.Generate();

            var createdCard = await serviceHub.Cards.Create(identityInfo, keyPair.PublicKey(), keyPair.PrivateKey());

            createdCard.Should().NotBeNull();
            createdCard.Identity.Value.Should().Be(identityInfo.Value);
            createdCard.Identity.Type.Should().Be(identityInfo.Type);
            createdCard.IsConfirmed.Should().BeTrue();
            createdCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }

        [Test]
        public async Task Search_HasedIdentityAsParameter_ListOfFoundCards()
        {
            var serviceHub = ServiceHubHelper.Create();
            
            var identityValue = Guid.NewGuid().ToString();
            var hashedIdentityValue = Obfuscator.Derive(identityValue, "724fTy6JmZxTNuM7");

            var validationToken = ValidationTokenGenerator.Generate(hashedIdentityValue, IdentityType.Custom,
               EnvironmentVariables.ApplicationPrivateKey, "z13x24");

            var identity = IdentityInfo.Custom(hashedIdentityValue, validationToken);

            var keyPair = VirgilKeyPair.Generate();

            await serviceHub.Cards.Create(identity, keyPair.PublicKey(), keyPair.PrivateKey());

            var foundCards = (await serviceHub.Cards.Search(hashedIdentityValue)).ToList();
            foundCards.Should().HaveCount(1);
            var theCard = foundCards.ToList().Single();

            theCard.Should().NotBeNull();
            theCard.Identity.Value.Should().Be(identity.Value);
            theCard.Identity.Type.Should().Be(identity.Type);
            theCard.IsConfirmed.Should().BeTrue();
            theCard.PublicKey.Value.Should().BeEquivalentTo(keyPair.PublicKey());
        }
    }
}