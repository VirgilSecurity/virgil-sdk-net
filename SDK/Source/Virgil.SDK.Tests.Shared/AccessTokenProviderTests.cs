using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using NUnit.Framework;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests.Shared
{
    [TestFixture]
    public class AccessTokenProviderTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public async Task GetTokenAsync_Should_ReturnNewTokenEachTime_IfUserHasNotImplementedCashInObtainTokenFuncAsync()
        {
            Func<TokenContext, Task<string>> obtainToken = async (TokenContext tokenContext) =>
            {
                var jwtFromServer = await IntegrationHelper.EmulateServerResponseToBuildTokenRequest(tokenContext);
                return jwtFromServer;
            };
            var callBackProvider = new CallbackJwtProvider(obtainToken);
            var context = new TokenContext(faker.Random.AlphaNumeric(20), "some_operation");
            var token1 = await callBackProvider.GetTokenAsync(context);
            var token2 = await callBackProvider.GetTokenAsync(context);
            Assert.AreNotEqual(token1, token2);
        }

        [Test]
        public void GetTokenAsync_Should_RaiseException_IfObtainTokenFuncReturnsInvalidString()
        {
            Func<TokenContext, Task<string>> obtainToken = async (TokenContext tokenContext) =>
            {
                var jwtFromServer = await Task<string>.Factory.StartNew(() =>
                {
                    return faker.Random.AlphaNumeric(30);
                }); ;
                return jwtFromServer;
            };
            var callBackProvider = new CallbackJwtProvider(obtainToken);
            var context = new TokenContext(faker.Random.AlphaNumeric(20), "some_operation");
            Assert.ThrowsAsync<ArgumentException>(
                async () => await callBackProvider.GetTokenAsync(context));
        }

        [Test]
        public async Task GetTokenAsync_Should_ReturnTheSameToken_InConstAccessTokenProviderAsync()
        {
            var jwtFromServer = await IntegrationHelper.EmulateServerResponseToBuildTokenRequest(
                new TokenContext(faker.Random.AlphaNumeric(20), "some_operation")
                );
            var jwt = new Jwt(jwtFromServer);
            var constAccessTokenProvider = new ConstAccessTokenProvider(jwt);
            var token1 = await constAccessTokenProvider.GetTokenAsync(
                new TokenContext(
                    faker.Random.AlphaNumeric(10),
                    faker.Random.AlphaNumeric(10),
                    true)
            );

            var token2 = await constAccessTokenProvider.GetTokenAsync(
                new TokenContext(
                    faker.Random.AlphaNumeric(10),
                    faker.Random.AlphaNumeric(10),
                    true)
            );

            Assert.AreEqual(token1, token2);
        }

        [Test]
        public async Task ImportedJwt_Should_BeTheSameAsOrigin()
        {
            Func<TokenContext, Task<string>> obtainToken = async (TokenContext tokenContext) =>
            {
                var jwtFromServer = await IntegrationHelper.EmulateServerResponseToBuildTokenRequest(tokenContext);
                return jwtFromServer;
            };
            var callBackProvider = new CallbackJwtProvider(obtainToken);
            var context = new TokenContext(faker.Random.AlphaNumeric(20), "some_operation");
            var token = await callBackProvider.GetTokenAsync(context);

            var importedJwt = new Jwt(token.ToString());

            Assert.AreEqual(importedJwt, token);
            Assert.AreEqual(importedJwt.ToString(), token.ToString());
            //Assert.AreEqual(importedJwt.BodyContent, ((Jwt) token).BodyContent);
            Assert.AreEqual(importedJwt.BodyContent.Identity, ((Jwt)token).BodyContent.Identity);
            Assert.AreEqual(importedJwt.BodyContent.AdditionalData, ((Jwt)token).BodyContent.AdditionalData);
            Assert.AreEqual(importedJwt.BodyContent.AppId, ((Jwt)token).BodyContent.AppId);
            Assert.AreEqual(importedJwt.BodyContent.ExpiresAt, ((Jwt)token).BodyContent.ExpiresAt);
            Assert.AreEqual(importedJwt.BodyContent.IssuedAt, ((Jwt)token).BodyContent.IssuedAt);
            Assert.AreEqual(importedJwt.BodyContent.Subject, ((Jwt)token).BodyContent.Subject);
            Assert.AreEqual(importedJwt.BodyContent.Issuer, ((Jwt)token).BodyContent.Issuer);


            Assert.AreEqual(importedJwt.HeaderContent.Algorithm, ((Jwt)token).HeaderContent.Algorithm);
            Assert.AreEqual(importedJwt.HeaderContent.ApiKeyId, ((Jwt)token).HeaderContent.ApiKeyId);
            Assert.AreEqual(importedJwt.HeaderContent.ContentType, ((Jwt)token).HeaderContent.ContentType);
            Assert.AreEqual(importedJwt.HeaderContent.Type, ((Jwt)token).HeaderContent.Type);

            Assert.IsTrue(importedJwt.SignatureData.SequenceEqual(((Jwt)token).SignatureData));


        }


    }
}
