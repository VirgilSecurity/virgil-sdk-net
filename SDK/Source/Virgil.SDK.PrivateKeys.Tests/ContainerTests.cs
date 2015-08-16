using Virgil.SDK.PrivateKeys.Exceptions;

namespace Virgil.SDK.PrivateKeys.Tests
{
    using System;
    using System.Text;

    using FluentAssertions;

    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;

    public class ContainerTests
    {
        private const string RequestSignHeader = "X-VIRGIL-REQUEST-SIGN";
        private const string RequestSignPublicKeyIdHeader = "X-VIRGIL-REQUEST-SIGN-PK-ID";

        private const string URL = "https://keys-private-stg.virgilsecurity.com";
        private const string TestUserId = "test-virgil@divermail.com";
        private const string TestPassword = "12345678";
        private const string ApplicationToken = "e872d6f718a2dd0bd8cd7d7e73a25f49";
        
        private readonly Guid TestPublicKeyId = Guid.Parse("d2aa2087-83c9-7bb7-2982-036049d73ede");

        private readonly byte[] PublicKey = Convert.FromBase64String(
            "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUlHYk1CUUdCeXFHU000OUFnRUdDU3NrQXdNQ0NBRUJEUU9CZ2dBRW44b3l1RGwwRllXSWFKczdwWFNJUDRDZApheCsrVnp3eE9YOGE1NkdrSUlIQXptdkZ2elpWUDhCS3I1RFZIZWV0SnZEUmIrcVR3eTZWUWZEdGRIL0wxVTdmCkx0TmRDQkkxM0Q2Qi9KNVQwQUZ3UGZ2K1FjUVhRbE8rTHpPSjdXRjh4ZG9pWVlXTmtwaGFCYkZseE0vbHo0TkUKQzNWcFRDM1NNcEkwZkdrcjRETT0KLS0tLS1FTkQgUFVCTElDIEtFWS0tLS0tCg==");

        private readonly byte[] PrivateKey = Convert.FromBase64String(
            "LS0tLS1CRUdJTiBFQyBQUklWQVRFIEtFWS0tLS0tCk1JSGFBZ0VCQkVBV1hYR05Hb1NKREluMXc2YytWaWxRa2pjSnJ0cmkvZVJFZzFrdS9UaTE2c255US9sRU0zb1QKS201MDMyNGhxb2JoaDhKSDNNV2p6T1J4LzVWaGFvVEFvQXNHQ1Nza0F3TUNDQUVCRGFHQmhRT0JnZ0FFbjhveQp1RGwwRllXSWFKczdwWFNJUDRDZGF4KytWend4T1g4YTU2R2tJSUhBem12RnZ6WlZQOEJLcjVEVkhlZXRKdkRSCmIrcVR3eTZWUWZEdGRIL0wxVTdmTHROZENCSTEzRDZCL0o1VDBBRndQZnYrUWNRWFFsTytMek9KN1dGOHhkb2kKWVlXTmtwaGFCYkZseE0vbHo0TkVDM1ZwVEMzU01wSTBmR2tyNERNPQotLS0tLUVORCBFQyBQUklWQVRFIEtFWS0tLS0tCg==");

        [Test]
        public async void Should_AddSignAndPublicKeyIdToHeader_When_SingRequest()
        {
            var body = new
            {
                container_type = "easy",
                password = TestPassword,
                request_sign_random_uuid = Guid.NewGuid()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequest(TestPublicKeyId, PrivateKey);

            request.Headers[RequestSignPublicKeyIdHeader].Should().Be(TestPublicKeyId.ToString());
            var sign = Convert.FromBase64String(request.Headers[RequestSignHeader]);

            using (var signer = new VirgilSigner())
            {
                signer.Verify(Encoding.UTF8.GetBytes(request.Body), sign, PublicKey).Should().BeTrue();
            }
        }

        [Test, ExpectedException(typeof(ApplicationTokenInvalidExcepton))]
        public async void Should_ThrowException_When_ApplicationTokenIsNotProvided()
        {
            var client = new KeyringClient(new Connection(null, new Uri(URL)));
            await client.Container.Initialize(ContainerType.Easy, TestPublicKeyId, PrivateKey, TestPassword);
        }


        [Test]
        public async void Should_CreateEasyAccount_By_GivenParameters()
        {
            var client = new KeyringClient(new Connection(ApplicationToken, new Uri(URL)));

            await client.Container.Initialize(ContainerType.Easy, TestPublicKeyId, PrivateKey, TestPassword);

            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            client.Connection.SetCredentials(credentials);
            await client.Connection.Authenticate();

            await client.Container.Remove(TestPublicKeyId, PrivateKey);
        }

        [Test]
        public async void Should_CreateNormalAccount_By_GivenParameters()
        {
            var client = new KeyringClient(new Connection(ApplicationToken, new Uri(URL)));
            
            await client.Container.Initialize(ContainerType.Normal, TestPublicKeyId, PrivateKey, "12345678");

            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            client.Connection.SetCredentials(credentials);
            await client.Connection.Authenticate();
            
            await client.Container.Remove(TestPublicKeyId, PrivateKey);
        }

        [Test]
        public async void Should_DeleteExistingAccount()
        {
            var client = new KeyringClient(new Connection(ApplicationToken, new Uri(URL)));
            
            await client.Container.Initialize(ContainerType.Easy, TestPublicKeyId, PrivateKey, "12345678");

            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            client.Connection.SetCredentials(credentials);

            await client.Container.Remove(TestPublicKeyId, PrivateKey);
        } 
    }
}