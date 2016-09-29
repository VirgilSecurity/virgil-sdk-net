namespace Virgil.SDK.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;

    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;

    using NUnit.Framework;
    using Virgil.SDK.Exceptions;

    public class VirgilCardCreationTest
    {
        [Test]
        public async Task CreateNewVirgilCard_DuplicateCardCreation_ShouldThrowException()
        {
            var crypto = new VirgilCrypto();
            var client = Environment.GetVirgilClient();
            
            var appKey = crypto.ImportPrivateKey(Environment.AppKey, Environment.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();

            var builder = new RequestSigner<CardCreateRequest>(crypto);
            builder.Initialize(new CardCreateRequest
            {
                Identity = aliceIdentity,
                IdentityType = "member",
                PublicKey = exportedPublicKey
            });

            builder.SelfSign(aliceKeys.PrivateKey);
            builder.Sign(Environment.AppID, appKey);

            var request = builder.Build();

            var virgilCard = await client.CreateCardAsync(request);
            Assert.ThrowsAsync<VirgilClientException>(async () => await client.CreateCardAsync(request));
        }

        [Test]
        public async Task CreateNewVirgilCard_IdentityAndPublicKeyGiven_ShouldBeFoundByIdentity()
        {
            var crypto = new VirgilCrypto();
            var client = Environment.GetVirgilClient();

            var appKey = crypto.ImportPrivateKey(Environment.AppKey, Environment.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();

            var builder = new RequestSigner<CardCreateRequest>(crypto);
            builder.Initialize(new CardCreateRequest
            {
                Identity = aliceIdentity,
                IdentityType = "member",
                PublicKey = exportedPublicKey
            });

            builder.SelfSign(aliceKeys.PrivateKey);
            builder.Sign(Environment.AppID, appKey);

            var request = builder.Build();

            var newCard = await client.CreateCardAsync(request);
            var cards = await client.SearchCardsAsync(new SearchCardsCriteria { Identities = new[] { aliceIdentity } });
            
            cards.Should().HaveCount(1);
            var foundCard = cards.Single();

            newCard.ShouldBeEquivalentTo(foundCard);
        }
    }
}