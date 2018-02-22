using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using NSubstitute;
using NUnit.Framework;
using Virgil.CryptoImpl;
using Virgil.SDK.Common;
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
            var client = new CardClient(AppSettings.CardsServiceAddress);
            Assert.ThrowsAsync<UnauthorizedClientException>(
                async () => await client.PublishCardAsync(GenerateRawSignedModel(identity), jwt.ToString()));
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
            var client = new CardClient(AppSettings.CardsServiceAddress);
            Assert.ThrowsAsync<UnauthorizedClientException>(
                async () => await client.GetCardAsync(faker.CardId(), jwt.ToString()));
        }

        [Test]
        public void SearchCard_Should_RaiseExceptionIfTokenSignedByWrongKeyAsync()
        {
            var jwt = JwtSignedByWrongApiKey(faker.Random.AlphaNumeric(15));
            var client = new CardClient(AppSettings.CardsServiceAddress);
            Assert.ThrowsAsync<UnauthorizedClientException>(
                async() => await client.SearchCardsAsync(faker.Random.AlphaNumeric(20), jwt.ToString()));
        }

        [Test]
        public void PublishCard_Should_RaiseExceptionIfTokenIdentityDiffersFromModelIdentity()
        {
            var jwt = GenerateJwt(
                faker.Random.AlphaNumeric(15), 
                (PrivateKey)IntegrationHelper.ApiPrivateKey(),
                AppSettings.ApiPublicKeyId
                );
            var client = new CardClient(AppSettings.CardsServiceAddress);
            Assert.ThrowsAsync<ClientException>(
                async () => await client.PublishCardAsync(
                    GenerateRawSignedModel(faker.Random.AlphaNumeric(15)), 
                    jwt.ToString())
                    );
        }

        private Jwt JwtSignedByWrongApiKey(string identity)
        {
            var crypto = new VirgilCrypto();
            var wrongApiKeyPair = crypto.GenerateKeys();
            var wrongApiPublicKeyId = faker.AppId();
            return GenerateJwt(identity, wrongApiKeyPair.PrivateKey, wrongApiPublicKeyId);
        }

        private Jwt GenerateJwt(string identity, PrivateKey apiPrivateKey, string apiPublicKeyId)
        {
            var jwtGenerator = new JwtGenerator(
                AppSettings.AppId,
                apiPrivateKey,
                apiPublicKeyId,
                TimeSpan.FromMinutes(10),
                Substitute.For<VirgilAccessTokenSigner>());

            var jwt = jwtGenerator.GenerateToken(identity);
            return jwt;
        }
    }
}
