namespace Virgil.SDK.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Common;
    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;

    public class VirgilCardSearchTest
    {
        [Test]
        public async Task SearchForVirgilCards_ValidationWithServiceKey_ShouldPassValidation()
        {
            var crypto = new VirgilCrypto();
            var client = IntergrationHelper.GetVirgilClient();
            client.SetCardValidator(new CardValidator(crypto));

            // CREATING A VIRGIL CARD

            var appKey = crypto.ImportPrivateKey(IntergrationHelper.AppKey, IntergrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var request = new CreateCardRequest(aliceIdentity, "member", exportedPublicKey);

            var requestSigner = new RequestSigner(crypto);

            requestSigner.SelfSign(request, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(request, IntergrationHelper.AppID, appKey);
            
            var aliceCard = await client.CreateCardAsync(request);

            // VALIDATING A VIRGIL CARD
            
            var cards = await client.SearchCardsAsync(SearchCriteria.ByIdentity(aliceIdentity));

            aliceCard.ShouldBeEquivalentTo(cards.Single());

            await IntergrationHelper.RevokeCard(aliceCard.Id);
        }

        [Test]
        public async Task SearchForTheVirgilCards_MultipleIdentitiesGiven_ShouldReturnVirgilCards()
        {
            // Initialization

            var crypto = new VirgilCrypto();
            var client = IntergrationHelper.GetVirgilClient();
            var requestSigner = new RequestSigner(crypto);
            
            var appKey = crypto.ImportPrivateKey(IntergrationHelper.AppKey, IntergrationHelper.AppKeyPassword);

            // Prepare Requests

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var aliceKeys = crypto.GenerateKeys();
            var alicePublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var bobIdentity = "bob-" + Guid.NewGuid();
            var bobKeys = crypto.GenerateKeys();
            var bobPublicKey = crypto.ExportPublicKey(bobKeys.PublicKey);

            var aliceRequest = new CreateCardRequest(aliceIdentity, "member", alicePublicKey);
            var bobRequest = new CreateCardRequest(bobIdentity, "member", bobPublicKey);

            requestSigner.SelfSign(aliceRequest, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(aliceRequest, IntergrationHelper.AppID, appKey);

            requestSigner.SelfSign(bobRequest, bobKeys.PrivateKey);
            requestSigner.AuthoritySign(bobRequest, IntergrationHelper.AppID, appKey);

            // Publish Virgil Cards 

            var aliceCard = await client.CreateCardAsync(aliceRequest);
            var bobCard = await client.CreateCardAsync(bobRequest);
            
            // Search for the Virgil Cards

            var foundCards = await client.SearchCardsAsync(new SearchCriteria
            {
                Identities = new[] { bobIdentity, aliceIdentity }
            });

            // Assertions
            
            foundCards.Should().HaveCount(2);

            foundCards.Single(it => it.Id == aliceCard.Id).ShouldBeEquivalentTo(aliceCard);
            foundCards.Single(it => it.Id == bobCard.Id).ShouldBeEquivalentTo(bobCard);

            await IntergrationHelper.RevokeCard(aliceCard.Id);
            await IntergrationHelper.RevokeCard(bobCard.Id);
        }

        [Test]
        public async Task GetSignleVirgilCard_ByGivenId_ShouldReturnVirgilCard()
        {
            var crypto = new VirgilCrypto();
            var client = IntergrationHelper.GetVirgilClient();
            var requestSigner = new RequestSigner(crypto);

            var appKey = crypto.ImportPrivateKey(IntergrationHelper.AppKey, IntergrationHelper.AppKeyPassword);

            // Prepare Requests

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var aliceKeys = crypto.GenerateKeys();
            var alicePublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceRequest = new CreateCardRequest(aliceIdentity, "member", alicePublicKey);

            requestSigner.SelfSign(aliceRequest, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(aliceRequest, IntergrationHelper.AppID, appKey);

            var aliceCard = await client.CreateCardAsync(aliceRequest);
            var foundAliceCard = await client.GetCardAsync(aliceCard.Id);

            aliceCard.ShouldBeEquivalentTo(foundAliceCard);

            await IntergrationHelper.RevokeCard(aliceCard.Id);
        }
    }
}