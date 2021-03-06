﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Virgil.SDK.Common;
using Virgil.SDK.Signer;
using Virgil.SDK.Verification;
using Virgil.SDK.Web.Authorization;
using Virgil.CryptoAPI;
using Virgil.Crypto;

namespace Virgil.SDK.Tests
{
    using Bogus;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.CryptoAPI;
    using Virgil.SDK.Web;
    using NSubstitute;
    using Virgil.Crypto;
    using System.Threading;

    [TestFixture]
    public class CardManagerTests
    {
        private readonly Faker faker = new Faker();


        [Test]
        public async Task CreateCard_Should_RegisterNewCardOnVirgilSerivice()
        {
            System.Console.WriteLine("Tests are running for AppId=" + AppSettings.AppId);
            var card = await IntegrationHelper.PublishCard("alice-" + Guid.NewGuid());
            Assert.AreNotEqual(card, null);
            var gotCard = await IntegrationHelper.GetCard(card.Id);
            Assert.IsTrue(card.ContentSnapshot.SequenceEqual(gotCard.ContentSnapshot));
            Assert.IsTrue(card.Signatures.First(
                x => x.Signer == ModelSigner.SelfSigner).Signature.SequenceEqual(
                gotCard.Signatures.First(x => x.Signer == ModelSigner.SelfSigner).Signature)
                );
            Assert.AreEqual(gotCard.Signatures.Count, 2);
        }


        [Test]
        public async Task CreateCardWithPreviousCardId_Should_RegisterNewCardAndFillPreviouscardId()
        {
            // chain of cards for alice
            var aliceName = "alice-" + Guid.NewGuid();
            var aliceCard = await IntegrationHelper.PublishCard(aliceName);
            // override previous alice card
            var newAliceCard = await IntegrationHelper.PublishCard(aliceName, aliceCard.Id);
            Assert.AreEqual(newAliceCard.PreviousCardId, aliceCard.Id);
        }

        [Test]
        public async Task PreviousCardId_Should_BeOutdated()
        {
            // chain of cards for alice
            var aliceName = "alice-" + Guid.NewGuid();
            var aliceCard = await IntegrationHelper.PublishCard(aliceName);
            // override previous alice card
            var newAliceCard = await IntegrationHelper.PublishCard(aliceName, aliceCard.Id);
            var outdatedCard = await IntegrationHelper.GetCard(aliceCard.Id);
            Assert.IsTrue(outdatedCard.IsOutdated);
            var esearch = await IntegrationHelper.SearchCardsAsync("lalala");
        }

        [Test]
        public async Task SearchCardByIdentityWhichHasTwoRelatedCards_Should_ReturnOneActualCard()
        {
            // chain of cards for alice
            var aliceName = "alice-" + Guid.NewGuid();
            var aliceCard = await IntegrationHelper.PublishCard(aliceName);
            // override previous alice card
            var newAliceCard = await IntegrationHelper.PublishCard(aliceName, aliceCard.Id);
            var cards = await IntegrationHelper.SearchCardsAsync(aliceName);
            Assert.AreEqual(cards.Count, 1);
            var actualCard = cards.First();
            Assert.AreEqual(actualCard.Id, newAliceCard.Id);
            Assert.AreEqual(actualCard.PreviousCard.Id, aliceCard.Id);
            Assert.IsTrue(actualCard.PreviousCard.IsOutdated);
        }

        [Test]
        public async Task SearchCardByIdentityWhichHasTwoUnrelatedCards_Should_ReturnTwoActualCards()
        {
            // list of cards for bob
            var bobName = "bob-" + Guid.NewGuid();
            // create two independent cards for bob
            await IntegrationHelper.PublishCard(bobName);
            await IntegrationHelper.PublishCard(bobName);

            var bobCards = await IntegrationHelper.SearchCardsAsync(bobName);
            Assert.AreEqual(bobCards.Count, 2);
        }

        [Test]
        public void CreateCardWithInvalidPreviousCardId_Should_RaiseException()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            Assert.ThrowsAsync<ClientException>(
                async () => await IntegrationHelper.PublishCard(aliceName, "InvalidPreviousCardId"));
        }

        [Test]
        public async Task CreateCardWithNonuniquePreviousCardId_Should_RaiseExceptionAsync()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            var prevCard = await IntegrationHelper.PublishCard(aliceName);
            // first card with previous_card
            await IntegrationHelper.PublishCard(aliceName, prevCard.Id);
            // second card with the same previous_card
            Assert.ThrowsAsync<ClientException>(
                async () => await IntegrationHelper.PublishCard(aliceName, prevCard.Id));
        }
        [Test]
        public async Task CreateCardWithWrongIdentityInPreviousCard_Should_RaiseExceptionAsync()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            var prevCard = await IntegrationHelper.PublishCard(aliceName);
            // identity and identity of previous card shouldn't be different
            Assert.ThrowsAsync<ClientException>(
               async () => await IntegrationHelper.PublishCard($"new-{aliceName}", prevCard.Id));
        }

        [Test]
        public void GetCardWithWrongId_Should_RaiseException()
        {
            Assert.ThrowsAsync<ClientException>(
                async () => await IntegrationHelper.GetCard("InvalidCardId"));
        }

        [Test]
        public async Task SearchCardWithWrongIdentity_Should_ReturnEmptyList()
        {
            var cards = await IntegrationHelper.SearchCardsAsync("someidentity1");
            Assert.AreEqual(cards.Count, 0);
        }

        [Test]
        public async Task SearchCards_Should_ReturnTheSameCard()
        {
            var aliceName = "alice-" + Guid.NewGuid();
            var card = await IntegrationHelper.PublishCard(aliceName);
            var aliceCards = await IntegrationHelper.SearchCardsAsync(aliceName);
            Assert.AreEqual(aliceCards.Count, 1);
            Assert.AreEqual(aliceCards.First().Id, card.Id);
        }

        
        [Test]
        public void ImportPureCardFromString_Should_CreateEquivalentCard()
        {
            //STC-3
            var rawSignedModel = faker.PredefinedRawSignedModel();
            var cardManager = faker.CardManager();
            var str = rawSignedModel.ExportAsString();
            var card = cardManager.ImportCardFromString(str);
            var exportedCardStr = cardManager.ExportCardAsString(card);
            Assert.AreEqual(exportedCardStr, str);
        }

        [Test]
        public void ImportPureCardFromJson_Should_CreateEquivalentCard()
        {
            //STC-3
            var rawSignedModel = faker.PredefinedRawSignedModel();
            var cardManager = faker.CardManager();
            var json = rawSignedModel.ExportAsJson();
            var card = cardManager.ImportCardFromJson(json);
            var exportedCardJson = cardManager.ExportCardAsJson(card);
            Assert.AreEqual(exportedCardJson, json);
        }



        [Test]
        public void ImportFullCardFromString_Should_CreateEquivalentCard()
        {
            //STC-4
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, true);
            var cardManager = faker.CardManager();
            var str = rawSignedModel.ExportAsString();
            var card = cardManager.ImportCardFromString(str);
            var exportedCardStr = cardManager.ExportCardAsString(card);
            Assert.AreEqual(exportedCardStr, str);
        }

        [Test]
        public void ImportFullCardFromJson_Should_CreateEquivalentCard()
        {
            //STC-4
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, true);
            var cardManager = faker.CardManager();
            var json = rawSignedModel.ExportAsJson();
            var card = cardManager.ImportCardFromJson(json);
            var exportedCardJson = cardManager.ExportCardAsJson(card);

            Assert.AreEqual(exportedCardJson, json);
        }
        [Test]
        public async Task CardManager_Should_RaiseException_IfExpiredToken()
        {
            // STC-26
            var aliceName = "alice-" + Guid.NewGuid();
            //var aliceCard = await IntegrationHelper.PublishCard(aliceName);
            var crypto = new VirgilCrypto();
            var keypair = crypto.GenerateKeys();

            var jwtFromServer = await IntegrationHelper.EmulateServerResponseToBuildTokenRequest(
               new TokenContext(faker.Random.AlphaNumeric(20), "some_operation"), 0.3
               );
            var jwt = new Jwt(jwtFromServer);
            var constAccessTokenProvider = new ConstAccessTokenProvider(jwt);

            var cardManager = IntegrationHelper.GetManagerWithConstAccessTokenProvider(constAccessTokenProvider);
            var aliceCard = await cardManager.PublishCardAsync(
                new CardParams()
                {
                    Identity = aliceName,
                    PublicKey = keypair.PublicKey,
                    PrivateKey = keypair.PrivateKey,
                    PreviousCardId = null,
                    ExtraFields = new Dictionary<string, string>
                    {
                        { "some meta key", "some meta val" }
                    }
                });

            // var aaa = await IntegrationHelper.GetCardAsync(aliceCard.Id);
            Thread.Sleep(30000);
            Assert.ThrowsAsync<UnauthorizedClientException>(
                async () => await cardManager.GetCardAsync(aliceCard.Id));
        }

        [Test]
        public async Task CardManager_Should_SendSecondRequestToCliet_IfTokenExpiredAndRetryOnUnauthorizedAsync()
        {
            // STC-26
            var expiredJwtGenerator = new JwtGenerator(
                AppSettings.AppId,
                IntegrationHelper.ApiPrivateKey(),
                AppSettings.ApiPublicKeyId,
                TimeSpan.FromSeconds(1),
                Substitute.For<VirgilAccessTokenSigner>());
            var jwtGenerator = new JwtGenerator(
                AppSettings.AppId,
                IntegrationHelper.ApiPrivateKey(),
                AppSettings.ApiPublicKeyId,
                TimeSpan.FromMinutes(5),
                new VirgilAccessTokenSigner()
            );
            var identity = faker.Random.AlphaNumeric(20);
            var expiredToken = expiredJwtGenerator.GenerateToken(identity);
            var accessTokenProvider = Substitute.For<IAccessTokenProvider>();
            // suppose we have got expired token at the first attempt
            // and we have got valid token at the second attempt
            accessTokenProvider.GetTokenAsync(Arg.Any<TokenContext>()
                ).Returns(
                args => 
                ((TokenContext)args[0]).ForceReload ? 
                jwtGenerator.GenerateToken(identity) : 
                expiredToken
                );
            var validator = new VirgilCardVerifier(new VirgilCardCrypto()) { VerifySelfSignature = true, VerifyVirgilSignature = true };
            validator.ChangeServiceCreds(AppSettings.ServicePublicKeyDerBase64);
            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = new VirgilCardCrypto(),
                AccessTokenProvider = accessTokenProvider,
                ApiUrl = AppSettings.CardsServiceAddress,
                RetryOnUnauthorized = true,
                Verifier = validator
            });

           var keypair = new VirgilCrypto().GenerateKeys();

            var card = await manager.PublishCardAsync(
                new CardParams()
                {
                    Identity = identity,
                    PublicKey = keypair.PublicKey,
                    PrivateKey = keypair.PrivateKey
                    
                });
            Assert.NotNull(card);
            var searchCard = await manager.SearchCardsAsync(identity);
            Assert.AreEqual(searchCard.Count, 1);

            var getCard = await manager.GetCardAsync(card.Id);
            Assert.NotNull(getCard);
        }


        [Test]
        public  void CardManager_Should_RaiseExceptionIfGetsInvalidCard()
        {
            // STC-13
            var verifier = Substitute.For<ICardVerifier>();
            verifier.VerifyCard(Arg.Any<Card>()).Returns(false);

            var signCallBack = Substitute.For<Func<RawSignedModel, Task<RawSignedModel>>>();
            signCallBack.Invoke(Arg.Any<RawSignedModel>()).Returns(args => (RawSignedModel)args[0]);
            var jwtGenerator = new JwtGenerator(
                faker.AppId(),
                IntegrationHelper.ApiPrivateKey(),
                AppSettings.ApiPublicKeyId,
                TimeSpan.FromMinutes(10),
                new VirgilAccessTokenSigner()
            );
            var accessTokenProvider = Substitute.For<IAccessTokenProvider>();
            var identity = faker.Random.AlphaNumeric(20);
            var token = jwtGenerator.GenerateToken(identity);
            accessTokenProvider.GetTokenAsync(Arg.Any<TokenContext>()).Returns(
              token  
            );
            var model = faker.PredefinedRawSignedModel(null, true, true, true);

            var client = Substitute.For<ICardClient>();
            Func<Task<Tuple<RawSignedModel, bool>>> getStub = async () =>
            {
                return new Tuple<RawSignedModel, bool>(model, false);
            };
            Func<Task<RawSignedModel>> publishStub = async () =>
            {
                return model;
            };
            Func<Task<IEnumerable<RawSignedModel>>> searchStub = async () =>
            {
                return new List<RawSignedModel>() {model};
            };
            client.GetCardAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(getStub.Invoke());
            client.PublishCardAsync(Arg.Any<RawSignedModel>(), Arg.Any<string>()).Returns(publishStub.Invoke());
            client.SearchCardsAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(searchStub.Invoke());

            var cardId = faker.CardId();
            var searchCardIdentity = faker.Random.AlphaNumeric(20);
            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = new VirgilCardCrypto(),
                AccessTokenProvider = accessTokenProvider,
                SignCallBack = signCallBack,
                Verifier = verifier,
                ApiUrl = AppSettings.CardsServiceAddress
            }
            ){Client = client};
            Assert.Throws<CardVerificationException>(() => manager.ImportCardFromJson(model.ExportAsJson()));
            Assert.Throws<CardVerificationException>(() => manager.ImportCardFromString(model.ExportAsString()));
            Assert.ThrowsAsync<CardVerificationException>(async () => await manager.GetCardAsync(cardId));
            Assert.ThrowsAsync<CardVerificationException>(async () => await manager.PublishCardAsync(model));
            Assert.ThrowsAsync<CardVerificationException>(async () => await manager.SearchCardsAsync(searchCardIdentity));
            Assert.Throws<CardVerificationException>(() => manager.ImportCard(model));
        }

        [Test]
        public void CardManager_Should_RaiseExceptionIfGetsCardWithDifferentId()
        {
            // STC-34
            var verifier = Substitute.For<ICardVerifier>();
            verifier.VerifyCard(Arg.Any<Card>()).Returns(true);
            var crypto = new VirgilCrypto();
            var keyPair = faker.PredefinedKeyPair();
            var rawCardContent = new RawCardContent()
            {
                CreatedAt = DateTime.UtcNow,
                Identity = "test",
                PublicKey = crypto.ExportPublicKey(keyPair.PublicKey),
                Version = "5.0"
            };
            var model = new RawSignedModel() { ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent) };

            var signer = new ModelSigner(new VirgilCardCrypto());
                signer.SelfSign(
                    model, keyPair.PrivateKey, new Dictionary<string, string>(){{ "info", "some_additional_info" }}
                    );
            var signCallBack = Substitute.For<Func<RawSignedModel, Task<RawSignedModel>>>();
            signCallBack.Invoke(Arg.Any<RawSignedModel>()).Returns(args => (RawSignedModel)args[0]);
            var jwtGenerator = new JwtGenerator(
                AppSettings.AppId,
                IntegrationHelper.ApiPrivateKey(),
                AppSettings.ApiPublicKeyId,
                TimeSpan.FromMinutes(10),
                new VirgilAccessTokenSigner()
            );
            var accessTokenProvider = Substitute.For<IAccessTokenProvider>();
            var identity = faker.Random.AlphaNumeric(20);
            var token = jwtGenerator.GenerateToken(identity);
            accessTokenProvider.GetTokenAsync(Arg.Any<TokenContext>()).Returns(
              token
            );
            var cardId = faker.CardId();
            var client = Substitute.For<ICardClient>();
            Func<Task<Tuple<RawSignedModel, bool>>> stub = async () =>
            {
                return new Tuple<RawSignedModel, bool>(model, false);
            };
            client.GetCardAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(stub.Invoke());

            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = new VirgilCardCrypto(),
                AccessTokenProvider = accessTokenProvider,
                SignCallBack = signCallBack,
                Verifier = verifier,
                ApiUrl = AppSettings.CardsServiceAddress
            }
            ){Client = client};
            Assert.ThrowsAsync<CardVerificationException>(async () => await manager.GetCardAsync(cardId));

        }

    }
}