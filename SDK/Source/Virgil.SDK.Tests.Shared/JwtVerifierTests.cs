using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Virgil.Crypto;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;
using Virgil.SDK.Crypto;
using Virgil.SDK.Web;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests.Shared
{
    [NUnit.Framework.TestFixture]
    public class JwtVerifierTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void JwtVerifier_Should_VerifyImportedJwt()
        {
            //STC-22
            var signer = new VirgilAccessTokenSigner();
            string apiPublicKeyId;
            string apiPublicKeyBase64;
            var crypto = new VirgilCrypto();

            var token = faker.PredefinedToken(signer, TimeSpan.FromMinutes(10), out apiPublicKeyId, out apiPublicKeyBase64).Item1;
           
            var jwtVerifier = new JwtVerifier(
                signer,
                crypto.ImportPublicKey(Bytes.FromString(apiPublicKeyBase64, StringEncoding.BASE64)),
                apiPublicKeyId);

            jwtVerifier.VerifyToken(token).ShouldBeEquivalentTo(true);
        }

        [Test]
        public void Verify_Should_VerifyTestCreatedInAnotherSDK()
        {
            var jwt = new Jwt(IntegrationHelper.ImportedJwt);
            var signer = new VirgilAccessTokenSigner();
            var crypto = new VirgilCrypto();

            var jwtVerifier = new JwtVerifier(
                signer,
                crypto.ImportPublicKey(
                    Bytes.FromString(IntegrationHelper.ImportedAccessPublicKey, StringEncoding.BASE64)),
                IntegrationHelper.ImportedAccessPublicKeyId);

            jwtVerifier.VerifyToken(jwt).ShouldBeEquivalentTo(true);
        }
    }
}
