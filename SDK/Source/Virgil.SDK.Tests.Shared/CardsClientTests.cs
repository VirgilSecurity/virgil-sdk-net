using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using NSubstitute;
using NUnit.Framework;
using Virgil.Crypto;
using Virgil.SDK.Common;
using Virgil.SDK.Crypto;
using Virgil.SDK.Signer;
using Virgil.SDK.Web;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests.Shared
{
    [TestFixture]
    public class CardsClientTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void PublishCard_Should_RaiseExceptionIfTokenSignedByWrongKey()
        {
            var identity = faker.Random.AlphaNumeric(15);
            var jwt = JwtSignedByWrongApiKey(identity);
            var client = new CardClient(IntegrationHelper.CardsServiceAddress);
            Assert.ThrowsAsync<UnauthorizedClientException>(
                () => client.PublishCardAsync(GenerateRawSignedModel(identity), jwt.ToString()));
        }

        private RawSignedModel GenerateRawSignedModel(string identity)
        {
            var crypto = new VirgilCrypto();
            var keyPair = faker.PredefinedKeyPair();
            var rawCardContent = new RawCardContent()
            {
                CreatedAt = DateTime.UtcNow,
                Identity = identity,
                PublicKey = crypto.ExportPublicKey(keyPair.PublicKey),
                Version = "5.0"
            };
            var model = new RawSignedModel() {ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent)};

            var signer = new ModelSigner(new VirgilCardCrypto());
            signer.SelfSign(model, keyPair.PrivateKey);
            return model;
        }

        [Test]
        public void GetCard_Should_RaiseExceptionIfTokenSignedByWrongKey()
        {
            var jwt = JwtSignedByWrongApiKey(faker.Random.AlphaNumeric(15));
            var client = new CardClient(IntegrationHelper.CardsServiceAddress);
            Assert.ThrowsAsync<UnauthorizedClientException>(
                () => client.GetCardAsync(faker.CardId(), jwt.ToString()));
        }

        [Test]
        public void SearchCard_Should_RaiseExceptionIfTokenSignedByWrongKey()
        {
            var jwt = JwtSignedByWrongApiKey(faker.Random.AlphaNumeric(15));
            var client = new CardClient(IntegrationHelper.CardsServiceAddress);
            Assert.ThrowsAsync<UnauthorizedClientException>(
                () => client.SearchCardsAsync(faker.Random.AlphaNumeric(20), jwt.ToString()));
        }

        [Test]
        public void PublishCard_Should_RaiseExceptionIfTokenIdentityDiffersFromModelIdentity()
        {
            var jwt = GenerateJwt(
                faker.Random.AlphaNumeric(15), 
                (PrivateKey)IntegrationHelper.ApiPrivateKey(), 
                IntegrationHelper.ApiPublicKeyId
                );
            var client = new CardClient(IntegrationHelper.CardsServiceAddress);
            Assert.ThrowsAsync<ClientException>(
                () => client.PublishCardAsync(
                    GenerateRawSignedModel(faker.Random.AlphaNumeric(15)), 
                    jwt.ToString())
                    );
        }

        private Jwt JwtSignedByWrongApiKey(string identity)
        {
            var crypto = new VirgilCrypto();
            var wrongApiKeyPair = crypto.GenerateKeys();
            var wrongApiPublicKeyId = Bytes.ToString(wrongApiKeyPair.PublicKey.Id, StringEncoding.HEX);
            return GenerateJwt(identity, wrongApiKeyPair.PrivateKey, wrongApiPublicKeyId);
        }

        private Jwt GenerateJwt(string identity, PrivateKey apiPrivateKey, string apiPublicKeyId)
        {
            var jwtGenerator = new JwtGenerator(
                IntegrationHelper.AppId,
                apiPrivateKey,
                apiPublicKeyId,
                TimeSpan.FromMinutes(10),
                Substitute.For<VirgilAccessTokenSigner>());

            var jwt = jwtGenerator.GenerateToken(identity);
            return jwt;
        }
    }
}
