namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using FluentAssertions;
    using NUnit.Framework;
    using TransferObject;

    public class PublicKeysClientTests
    {
        [Test]
        public async Task ShouldBeAbleToGetPublicKeyByItsId()
        {
            var serviceHub = ServiceHubHelper.Create();

            var publicKeysClient = serviceHub.PublicKeys;
            var virgilCardClient = serviceHub.Cards;
            
            var card = await virgilCardClient.TestCreateVirgilCard();

            var publicKeyOur = card.VirgilCard.PublicKey;
            PublicKeyDto publicKeyTheir = await publicKeysClient.Get(publicKeyOur.Id);

            publicKeyOur.PublicKey.ShouldAllBeEquivalentTo(publicKeyTheir.PublicKey);
        }

        [Test]
        public async Task ShouldBeAbleToGetPublicKeyByItsIdExtended()
        {
            var serviceHub = ServiceHubHelper.Create();

            var publicKeysClient = serviceHub.PublicKeys;
            var virgilCardClient = serviceHub.Cards;

            var card = await virgilCardClient.TestCreateVirgilCard();

            var publicKeyOur = card.VirgilCard.PublicKey;
            IEnumerable<VirgilCardDto> publicKeyExtended = await publicKeysClient.GetExtended(publicKeyOur.Id, card.VirgilCard.Id, card.VirgilKeyPair.PrivateKey());

            publicKeyOur.PublicKey.ShouldAllBeEquivalentTo(publicKeyExtended.First().PublicKey.PublicKey);
            publicKeyExtended.Count().Should().Be(1);
            publicKeyExtended.First().Hash.ShouldBeEquivalentTo(card.VirgilCard.Hash);
        }
        
        [Test]
        public async Task ShouldBeAbleToRevokePublicKeys()
        {
            var serviceHub = ServiceHubHelper.Create();

            var publicKeysClient = serviceHub.PublicKeys;
            var cardsClient = serviceHub.Cards;

            var card = await cardsClient.TestCreateVirgilCard();

            var publicKeyOur = card.VirgilCard.PublicKey;

            var identity = card.VirgilCard.Identity;
            var request = await serviceHub.Identity.Verify(identity.Value, IdentityType.Email);

            var code = await Mailinator.GetConfirmationCodeFromLatestEmail(identity.Value, true);
            var token = await serviceHub.Identity.Confirm(request.ActionId, code);

            await publicKeysClient.Revoke(
                publicKeyOur.Id,
                new[] { token },
                card.VirgilCard.Id,
                card.VirgilKeyPair.PrivateKey());

            try
            {
                await publicKeysClient.Get(publicKeyOur.Id);
                throw new Exception("Test failed");
            }
            catch (VirgilPublicServicesException exc) when (exc.Message == "Entity not found")
            {
            }
        }
    }
}