namespace Virgil.SDK.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FluentAssertions;
    using NUnit.Framework;
    using Client.Requests;
    using Cryptography;
    using Client;
    using Exceptions;
    using System.Collections.Generic;
    using Client.Models;
    using System.Configuration;

    public class VirgilCardCreationTest
    {
        [Test]
        public async Task CreateNewVirgilCard_DuplicateCardCreation_ShouldThrowException()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetCardsClient();
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();

            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
            var request = new CreateUserCardRequest
            {
                Identity = "alice-" + Guid.NewGuid(),
                PublicKeyData = exportedPublicKey
            };

            request.SelfSign(crypto, aliceKeys.PrivateKey);

            var exportedRequest = request.Export();

            // transfer alice's request to the server

            var importedRequest = new CreateUserCardRequest();
            importedRequest.Import(exportedRequest);

            importedRequest.ApplicationSign(crypto, IntegrationHelper.AppID, appKey);

            // publish alice's card
            var cardModel = await client.CreateUserCardAsync(importedRequest);

            Assert.ThrowsAsync<VirgilClientException>(async () => await client.CreateUserCardAsync(request));

            await IntegrationHelper.RevokeCard(cardModel.Id);
        }

        [Test]
        public async Task CreateNewVirgilCard_IdentityAndPublicKeyGiven_ShouldBeFoundByIdentity()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetCardsClient();
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var aliceIdentity = "alice-" + Guid.NewGuid();

            // publish alice's card
            var aliceCard = await IntegrationHelper.PublishCard(client, crypto, aliceIdentity, aliceKeys);

            var cards = await client.SearchCardsAsync(new SearchCriteria { Identities = new[] { aliceIdentity } });

            cards.Should().HaveCount(1);
            var foundCard = cards.Single();

            aliceCard.ShouldBeEquivalentTo(foundCard);

            await IntegrationHelper.RevokeCard(aliceCard.Id);
        }

        [Test]
        public async Task CreateNewVirgilCard_SignatureValidation_ShouldPassValidation()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetCardsClient();

            // CREATING A VIRGIL CARD
            var aliceKeys = crypto.GenerateKeys();
            var aliceIdentity = "alice-" + Guid.NewGuid();

            // publish alice's card
            var aliceCard = await IntegrationHelper.PublishCard(client, crypto, aliceIdentity, aliceKeys);

            // VALIDATING A VIRGIL CARD
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);
            var appPublicKey = crypto.ExtractPublicKey(appKey);
            var exportedAppPublicKey = crypto.ExportPublicKey(appPublicKey);

            var validator = new CardValidator(crypto);
            validator.AddVerifier(IntegrationHelper.AppID, exportedAppPublicKey);

            validator.Validate(aliceCard).Should().BeTrue();

            await IntegrationHelper.RevokeCard(aliceCard.Id);

        }

        [Test]
        [Ignore("global")]
        public async Task CreateNewGlobalCard()
        {
            var virgil = new VirgilApi(IntegrationHelper.VirgilApiContext());
            // generate a Virgil Key
            var aliceKey = virgil.Keys.Generate();

            // create a Global Virgil Card 
            var aliceCard = virgil.Cards.CreateGlobal(
                identity: "marfachaiko@gmail.com",
                identityType: IdentityType.Email,
                ownerKey: aliceKey
            );

            var attemptId = Guid.NewGuid().ToString();
            var option = new IdentityTokenOptions();

            option.ExtraFields = new Dictionary<string, string> {
                    { "attempt_id", attemptId.ToString() }
                };

            // initiate identity verification process
            var attempt = await aliceCard.CheckIdentityAsync(option);
            var code = "";
            // confirm an identity and grab the validation token
            var token = await attempt.ConfirmAsync(new EmailConfirmation(code));

            // publish the Virgil Card
            await virgil.Cards.PublishGlobalAsync(aliceCard, token);
        }

        [Test]
        [Ignore("global")]
        public async Task CreateGlobalCardLowLevel()
        {
            var crypto = new VirgilCrypto();
            var identityClient = IntegrationHelper.GetIdentityClient();
            var cardsClient = IntegrationHelper.GetCardsClient();

            var verifyResult = await identityClient.VerifyEmailAsync("marfachaiko@gmail.com");

            var code = "";
            var tokenResult = await identityClient.ConfirmEmailAsync(verifyResult.ActionId, code);

            var bobKeyPair = crypto.GenerateKeys();
            var bobCardRequest = new CreateGlobalCardRequest
            {
                Identity = tokenResult.Identity,
                IdentityType = GlobalCardIdentityType.Email,
                PublicKeyData = crypto.ExportPublicKey(bobKeyPair.PublicKey),
                CustomFields = new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                },
                ValidationToken = tokenResult.ValidationToken
            };

            bobCardRequest.SelfSign(crypto, bobKeyPair.PrivateKey);

            var bobCard = await cardsClient.CreateGlobalCardAsync(bobCardRequest);


            verifyResult = await identityClient.VerifyEmailAsync("marfachaiko@gmail.com");

            code = "";
            tokenResult = await identityClient.ConfirmEmailAsync(verifyResult.ActionId, code);

            var removeBobCardRequest = new RevokeGlobalCardRequest()
            {
                CardId = bobCard.Id,
                Reason = RevocationReason.Unspecified,
                ValidationToken = tokenResult.ValidationToken
            };

            removeBobCardRequest.SelfSign(crypto, bobCard.Id, bobKeyPair.PrivateKey);

            await cardsClient.RevokeGlobalCardAsync(removeBobCardRequest);

        }
        
        [Test]
        public async Task AuthClient()
        {
            var authClient = IntegrationHelper.GetAuthClient();
            var crypto = new VirgilCrypto();

            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);
            var authPublicKey = crypto.ExtractPublicKey(appKey);
           
            // obtain and re-encrypt a challenge message
            var messageResponse = await authClient.GetChallengeMessageAsync(IntegrationHelper.AppID);

            var message = crypto.Decrypt(messageResponse.EncryptedMessage, appKey);
            var reEncryptedMessage = crypto.Encrypt(message, authPublicKey);

            // grab an access code using encrypted challenge message
            var accessCodeResponse = await authClient.AsknowledgeAsync(messageResponse.AuthenticationGrantId, reEncryptedMessage);

            // obtain an access token using access code
            var accessTokenResponse = await authClient.ObtainAccessTokenAsync(accessCodeResponse.AccessCode);

            // refresh the access token using refresh token
            var refreshTokenResponse = await authClient.RefreshAccessTokenAsync(accessTokenResponse.RefreshToken);

            // verify the access token
            var verifyAccessTokenResponse = await authClient.VerifyAccessTokenAsync(accessTokenResponse.AccessToken);

            var a = verifyAccessTokenResponse.ResourceOwnerVirgilCardId;
        }
    }
}