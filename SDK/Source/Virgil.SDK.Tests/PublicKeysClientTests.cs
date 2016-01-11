namespace Virgil.SDK.Keys.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using Virgil.SDK.Infrastructure;

    public class PublicKeysClientTests
    {
        [SetUp]
        public void Init()
        {
            ServiceLocator.SetupForTests();
        }

        [Test]
        public async Task ShouldBeAbleToGetPublicKeyByItsId()
        {
            var publicKeysClient = ServiceLocator.Services.PublicKeys;
            var virgilCardClient = ServiceLocator.Services.Cards;
            
            var card = await virgilCardClient.TestCreateVirgilCard();

            var publicKeyOur = card.VirgilCard.PublicKey;
            var publicKeyTheir = await publicKeysClient.Get(publicKeyOur.Id);

            publicKeyOur.PublicKey.ShouldAllBeEquivalentTo(publicKeyTheir.PublicKey);
        }

        [Test]
        public async Task ShouldBeAbleToGetPublicKeyByItsIdExtended()
        {
            var publicKeysClient = ServiceLocator.Services.PublicKeys;
            var virgilCardClient = ServiceLocator.Services.Cards;

            var card = await virgilCardClient.TestCreateVirgilCard();

            var publicKeyOur = card.VirgilCard.PublicKey;
            var publicKeyExtended = await publicKeysClient.GetExtended(publicKeyOur.Id, card.VirgilCard.Id, card.VirgilKeyPair.PrivateKey());

            publicKeyOur.PublicKey.ShouldAllBeEquivalentTo(publicKeyExtended.First().PublicKey.PublicKey);
            publicKeyExtended.Count().Should().Be(1);
            publicKeyExtended.First().Hash.ShouldBeEquivalentTo(card.VirgilCard.Hash);
        }
    }
}