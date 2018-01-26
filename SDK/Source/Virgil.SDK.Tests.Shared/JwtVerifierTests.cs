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
using Virgil.SDK.Web;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests.Shared
{
    [NUnit.Framework.TestFixture]
    public class JwtVerifierTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void JwtParse_Should()
        {
            /*
            var rawCard = Configuration.Serializer.Deserialize<string>(
                "eyJpZGVudGl0eSI6IlRFU1QiLCJwdWJsaWNfa2V5IjoiTUNvd0JRWURLMlZ3QXlFQVpUdHZkVmE2YnhLUENWcDZVWnBwMFhJNDdhN3lNTlNNb2FYZ0R5VHQvak09IiwidmVyc2lvbiI6IjUuMCIsImNyZWF0ZWRfYXQiOjE1MTc5MDQ2NzN9");
            var rawcard2 = Configuration.Serializer.Deserialize<RawCardContent>(rawCard);
            var str = "eyJhbGciOiJWRURTNTEyIiwidHlwIjoiSldUIiwiY3R5IjoidmlyZ2lsLWp3dDt2PTEiLCJraWQiOiIzMWNiMDMzYWQ3ZDk0NGYzYzRkZGQ2ZGE1OGExMThmYzE4MzY2ZDExOTc4MjQxM2NmMTYxYmM4MjRlODI1MGU0In0.eyJpc3MiOiJ2aXJnaWwtN2RhZjE0NDE5ZWU4NDJiYThhMWFhYmE3M2QxMDVkNDQiLCJzdWIiOiJpZGVudGl0eS1Tb21lVGVzdElkZW50aXR5IiwiZXhwIjoxNTE2OTY3Mjk2LCJpYXQiOjE1MTY5NjM2OTd9.MFEwDQYJYIZIAWUDBAICBQAEQDMTLQkMNGpC8i1fqkmTfBJBCv_lYNJyA7CfPjjzeCqAgF_dU1fF6wrzvlgoDJsPYVzf64vgycvoZgC0Jouw9w8";
            var publicKeyBase64 = "MCowBQYDK2VwAyEAIFpLWJWM1u8IvUbZzdvNjdu6syEJq5BgmstGl8vwrqI=";
            var jwt = JwtParser.Parse(str);
            var crypto = new VirgilCrypto();
            var pubKey = crypto.ImportPublicKey(Bytes.FromString(publicKeyBase64, StringEncoding.BASE64));
            var signer = new VirgilAccessTokenSigner();
            var jwtVerifier = new JwtVerifier(signer, pubKey, "31cb033ad7d944f3c4ddd6da58a118fc18366d119782413cf161bc824e8250e4");
            var e = jwtVerifier.VerifyToken(jwt);*/
        }

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
