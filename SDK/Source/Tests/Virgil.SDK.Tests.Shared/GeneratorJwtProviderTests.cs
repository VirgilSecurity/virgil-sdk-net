using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Virgil.Crypto;
using Virgil.SDK.Common;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests.Shared
{
    [TestFixture]
    public class GeneratorJwtProviderTests
    {
        public GeneratorJwtProviderTests()
        {
        }

        [Test]
        public async Task generatorJwtProvider_Should_GenerateValidJwt()
        {
            var defaultIdentity = "my_default_identity";
            var identity = "identity";
            var signer = new VirgilAccessTokenSigner();
            var builder = new JwtGenerator(
                AppSettings.AppId,
                IntegrationHelper.ApiPrivateKey(),
                AppSettings.ApiPublicKeyId,
                TimeSpan.FromMinutes(5),
                signer);
            var generatorJwtProvider = new GeneratorJwtProvider(builder, defaultIdentity);
            var jwt = (Jwt)(await generatorJwtProvider.GetTokenAsync(new TokenContext(identity, "get")));
            Assert.Equals(identity, jwt.Identity());
            Assert.Equals(AppSettings.AppId, jwt.BodyContent.AppId);
            Assert.Null(jwt.BodyContent.AdditionalData);

            var crypto = new VirgilCrypto();
            var jwtVerifier = new JwtVerifier(
                signer,
                crypto.ImportPublicKey(
                    Bytes.FromString(AppSettings.ImportedAccessPublicKey, StringEncoding.BASE64)),
                AppSettings.ImportedAccessPublicKeyId);
            Assert.IsTrue(jwtVerifier.VerifyToken(jwt));
        }

        [Test]
        public async Task generatorJwtProvider_Should_GenerateValidJwtWithAdditionalData()
        {
            var defaultIdentity = "my_default_identity";
            var identity = "identity";
            var additionalData = new Dictionary<object, object>() { {"field_1", "data"}};
            var signer = new VirgilAccessTokenSigner();
            var builder = new JwtGenerator(
                AppSettings.AppId,
                IntegrationHelper.ApiPrivateKey(),
                AppSettings.ApiPublicKeyId,
                TimeSpan.FromMinutes(5),
                signer);
            var generatorJwtProvider = new GeneratorJwtProvider(builder, defaultIdentity, additionalData);
            var jwt = (Jwt)(await generatorJwtProvider.GetTokenAsync(new TokenContext(identity, "get")));
            Assert.Equals(identity, jwt.Identity());
            Assert.Equals(AppSettings.AppId, jwt.BodyContent.AppId);
            Assert.Equals(additionalData, jwt.BodyContent.AdditionalData);
            var crypto = new VirgilCrypto();
            var jwtVerifier = new JwtVerifier(
                signer,
                crypto.ImportPublicKey(
                    Bytes.FromString(AppSettings.ImportedAccessPublicKey, StringEncoding.BASE64)),
                AppSettings.ImportedAccessPublicKeyId);
            Assert.IsTrue(jwtVerifier.VerifyToken(jwt));
        }

        [Test]
        public async Task generatorJwtProvider_Should_GenerateJwtWithDefaultIdentity()
        {
            var defaultIdentity = "my_default_identity";
            var additionalData = new Dictionary<object, object>() { { "field_1", "data" } };
            var signer = new VirgilAccessTokenSigner();
            var builder = new JwtGenerator(
                AppSettings.AppId,
                IntegrationHelper.ApiPrivateKey(),
                AppSettings.ApiPublicKeyId,
                TimeSpan.FromMinutes(5),
                signer);
            var generatorJwtProvider = new GeneratorJwtProvider(builder, defaultIdentity, additionalData);
            var jwt = (Jwt)(await generatorJwtProvider.GetTokenAsync(new TokenContext(null, "get")));
            Assert.Equals(defaultIdentity, jwt.Identity());
            Assert.Equals(AppSettings.AppId, jwt.BodyContent.AppId);
            Assert.Equals(additionalData, jwt.BodyContent.AdditionalData);
            var crypto = new VirgilCrypto();
            var jwtVerifier = new JwtVerifier(
                signer,
                crypto.ImportPublicKey(
                    Bytes.FromString(AppSettings.ImportedAccessPublicKey, StringEncoding.BASE64)),
                AppSettings.ImportedAccessPublicKeyId);
            Assert.IsTrue(jwtVerifier.VerifyToken(jwt));
        }
    }
}
