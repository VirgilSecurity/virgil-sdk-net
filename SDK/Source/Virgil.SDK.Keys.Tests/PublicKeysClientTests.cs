namespace Virgil.SDK.Keys.Tests
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Infrastructure;
    using NUnit.Framework;

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

            publicKeyOur.PublicKey.ShouldAllBeEquivalentTo(publicKeyExtended.PublicKey);
            publicKeyExtended.VirgilCards.Count.Should().Be(1);
            publicKeyExtended.VirgilCards[0].Hash.ShouldBeEquivalentTo(card.VirgilCard.Hash);
        }
    }
}