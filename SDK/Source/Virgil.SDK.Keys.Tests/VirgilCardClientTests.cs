
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
    using Keys.Domain;

    public static class Constants
    {
        public const string ApplicationToken = "e872d6f718a2dd0bd8cd7d7e73a25f49";

        public static readonly PublicServicesConnection ApiEndpoint = 
            new PublicServicesConnection(
                ApplicationToken,
                new Uri(@"https://keys-stg.virgilsecurity.com"));
    }

    public class PublicKeysClientTests
    {
        [Test]
        public async Task ShouldBeAbleToGetPublicKeyByItsId()
        {
            var publicKeysClient = new PublicKeysClient(Constants.ApiEndpoint);
            var virgilCardClient = new VirgilCardClient(Constants.ApiEndpoint);

            var card = await virgilCardClient.TestCreateVirgilCard();

            var publicKeyOur = card.VirgilCard.PublicKey;
            var publicKeyTheir = await publicKeysClient.Get(publicKeyOur.Id);

            publicKeyOur.PublicKey.ShouldAllBeEquivalentTo(publicKeyTheir.PublicKey);
        }

        [Test]
        public async Task ShouldBeAbleToGetPublicKeyByItsIdExtended()
        {
            var publicKeysClient = new PublicKeysClient(Constants.ApiEndpoint);
            var virgilCardClient = new VirgilCardClient(Constants.ApiEndpoint);

            var card = await virgilCardClient.TestCreateVirgilCard();

            var publicKeyOur = card.VirgilCard.PublicKey;
            var publicKeyExtended = await publicKeysClient.GetExtended(publicKeyOur.Id, card.VirgilCard.Id, card.VirgilKeyPair.PrivateKey());

            publicKeyOur.PublicKey.ShouldAllBeEquivalentTo(publicKeyExtended.PublicKey);
            publicKeyExtended.VirgilCards.Count.Should().Be(1);
            publicKeyExtended.VirgilCards[0].Hash.ShouldBeEquivalentTo(card.VirgilCard.Hash);
        }


    }

    public class VirgilCardClientTests
    {
        [Test]
        public async Task ShouldBeAbleToCreateNewVirgilCard()
        {
            var client = new VirgilCardClient(Constants.ApiEndpoint);

            var virgilKeyPair = new VirgilKeyPair();
            var email = Mailinator.GetRandomEmailName();
            var customData = new Dictionary<string,string>
            {
                ["hello"] = "world",
                ["created-guid"] = Guid.NewGuid().ToString()
            };

            VirgilCardDto virgilCard = await client.Create(
                virgilKeyPair.PublicKey(),
                IdentityType.Email,
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
            var client = new VirgilCardClient(Constants.ApiEndpoint);

            var batch = await client.TestCreateVirgilCard();

            var attached = await client.CreateAttached(
                batch.VirgilCard.PublicKey.Id,
                IdentityType.Email,
                Mailinator.GetRandomEmailName(),
                null,
                batch.VirgilKeyPair.PrivateKey());

            attached.PublicKey.Id.Should().Be(batch.VirgilCard.PublicKey.Id);
            attached.PublicKey.PublicKey.ShouldAllBeEquivalentTo(batch.VirgilCard.PublicKey.PublicKey);
        }

        [Test]
        public async Task ShouldBeAbleToSignAndUnsignVirgilCard()
        {
            var client = new VirgilCardClient(Constants.ApiEndpoint);

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
            var client = new VirgilCardClient(Constants.ApiEndpoint);

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