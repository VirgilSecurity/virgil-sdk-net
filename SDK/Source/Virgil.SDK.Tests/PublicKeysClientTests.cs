namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using FluentAssertions;
    using Models;
    using NUnit.Framework;

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
            PublicKeyModel publicKeyTheir = await publicKeysClient.Get(publicKeyOur.Id);

            publicKeyOur.Value.ShouldAllBeEquivalentTo(publicKeyTheir.Value);
        }

        [Test]
        public async Task ShouldBeAbleToGetPublicKeyByItsIdExtended()
        {
            var serviceHub = ServiceHubHelper.Create();

            var publicKeysClient = serviceHub.PublicKeys;
            var virgilCardClient = serviceHub.Cards;

            var card = await virgilCardClient.TestCreateVirgilCard();

            var publicKeyOur = card.VirgilCard.PublicKey;
            IEnumerable<CardModel> publicKeyExtended = await virgilCardClient.GetRelatedCards(publicKeyOur.Id, card.VirgilCard.Id, card.VirgilKeyPair.PrivateKey());

            publicKeyOur.Value.ShouldAllBeEquivalentTo(publicKeyExtended.First().PublicKey.Value);
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
            var emailVerifier = await serviceHub.Identity.VerifyEmail(identity.Value);

            var code = await Mailinator.GetConfirmationCodeFromLatestEmail(identity.Value, true);
            var token = await emailVerifier.Confirm(code);

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