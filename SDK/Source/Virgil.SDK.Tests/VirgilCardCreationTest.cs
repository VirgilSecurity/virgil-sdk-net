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

            var request = CreateCardRequest.Create(aliceIdentity, "memeber", exportedPublicKey);
            var fingerprint = crypto.CalculateFingerprint(request.Snapshot);

            var appSignature = crypto.SignText(fingerprint, appKey);
            var ownerSignature = crypto.SignText(fingerprint, aliceKeys.PrivateKey);

            request.AppendSignature(fingerprint, ownerSignature);
            request.AppendSignature(Environment.AppID, appSignature);

            await client.CreateCardAsync(request);
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

            var request = CreateCardRequest.Create(aliceIdentity, "memeber", exportedPublicKey);
            var fingerprint = crypto.CalculateFingerprint(request.Snapshot);

            var appSignature = crypto.SignText(fingerprint, appKey);
            var ownerSignature = crypto.SignText(fingerprint, aliceKeys.PrivateKey);

            request.AppendSignature(fingerprint, ownerSignature);
            request.AppendSignature(Environment.AppID, appSignature);

            var newCard = await client.CreateCardAsync(request);
            var cards = await client.SearchCardsAsync(new SearchCardsCriteria { Identities = new[] { aliceIdentity } });
            
            cards.Should().HaveCount(1);
            var foundCard = cards.Single();

            newCard.ShouldBeEquivalentTo(foundCard);
        }
    }
}