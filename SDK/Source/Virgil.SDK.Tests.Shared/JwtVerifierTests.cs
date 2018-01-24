using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Virgil.Crypto;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;
using Virgil.SDK.Crypto;
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
            var crypto = new VirgilCrypto();
            var signer = new VirgilAccessTokenSigner();

            var accessKeyPair = crypto.GenerateKeys();
            var fingerprint = crypto.GenerateSHA256(crypto.ExportPublicKey(accessKeyPair.PublicKey));
            var accessPublicKeyId = Bytes.ToString(fingerprint, StringEncoding.HEX);

            var accessPublicKeyBase64 = Bytes.ToString(
                crypto.ExportPublicKey(accessKeyPair.PublicKey), StringEncoding.BASE64);

            var jwtGenerator = new JwtGenerator(
                faker.AppId(),
                accessKeyPair.PrivateKey,
                accessPublicKeyId, 
                TimeSpan.FromMinutes(10), 
                signer);

            var token = jwtGenerator.GenerateToken("some_identity", new Dictionary<string, string>
            {
                {"username", "some_username"}
            });

            System.IO.File.WriteAllText(@"C:\Users\Vasilina\Documents\token_export", 
                accessPublicKeyBase64 + 
                "   " + accessPublicKeyId + "  " + token);

            var importedJwt = JwtParser.Parse(token.ToString());

            importedJwt.ShouldBeEquivalentTo(token);
            importedJwt.ToString().ShouldBeEquivalentTo(token.ToString());
            var jwtVerifier = new JwtVerifier(signer, accessKeyPair.PublicKey, accessPublicKeyId);

            jwtVerifier.VerifyToken(importedJwt).ShouldBeEquivalentTo(true);
        }
    }
}
