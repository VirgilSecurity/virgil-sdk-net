using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests.Shared
{
    [NUnit.Framework.TestFixture]
    public class CachingJwtProviderTests
    {
        [Test]
        public async System.Threading.Tasks.Task CachingJwtProvider_Should_ReturnTheSameTokenIfValidAsync()
        {
            var provider = new CachingJwtProvider(IntegrationHelper.GetObtainToken(10));
            var jwt = await provider.GetTokenAsync(new TokenContext("some_identity", "sme_operation"));
            var jwt2 = await provider.GetTokenAsync(new TokenContext("some_identity", "sme_operation"));

            Assert.AreSame(jwt, jwt2);
        }

        [Test]
        public async System.Threading.Tasks.Task CachingJwtProvider_Should_ReturnNewTokenIfExpired()
        {
            var provider = new CachingJwtProvider(IntegrationHelper.GetObtainToken(0.01));
            var jwt = await provider.GetTokenAsync(new TokenContext("some_identity", "sme_operation"));
            var jwt2 = await provider.GetTokenAsync(new TokenContext("some_identity", "sme_operation"));

            Assert.AreNotEqual(jwt, jwt2);
        }
    }
}
