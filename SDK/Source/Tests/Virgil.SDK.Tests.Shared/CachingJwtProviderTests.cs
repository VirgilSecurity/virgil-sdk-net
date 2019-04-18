using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests.Shared
{
    [TestFixture]
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
            var provider = new CachingJwtProvider(IntegrationHelper.GetObtainToken(0.001));
            var jwt = await provider.GetTokenAsync(new TokenContext("some_identity", "sme_operation"));
            var jwt2 = await provider.GetTokenAsync(new TokenContext("some_identity", "sme_operation"));

            Assert.AreNotEqual(jwt, jwt2);
        }

        [Test]
        public async System.Threading.Tasks.Task CachingJwtProvider_Should_BeThreadSafe()
        {
            var client1 = new TokenClient(1);
            var client2 = new TokenClient(2);

            var jwt = client1.AccessToken;
            var jwt2 = client2.AccessToken;

            Assert.AreEqual(jwt, jwt2);
        }


        private class TokenClient
        {
            static CachingJwtProvider provider = new CachingJwtProvider(IntegrationHelper.GetObtainToken(10));
            private Thread myThread;
            public IAccessToken AccessToken;

            public TokenClient(int i)
            {
                myThread = new Thread(GetToken);
                myThread.Name = "Client " + i.ToString();
                myThread.Start();
            }

            public async void GetToken()
            {
                AccessToken = await provider.GetTokenAsync(new TokenContext("some_identity", "sme_operation"));
                Thread.Sleep(1000);
            }
        }
    }
}
