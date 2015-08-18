namespace Virgil.SDK.PrivateKeys.Tests
{
    using System;
    using System.Text;

    using FluentAssertions;

    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;
    using Virgil.SDK.PrivateKeys.Exceptions;

    public class ContainerTests
    {
        private const string RequestSignHeader = "X-VIRGIL-REQUEST-SIGN";
        private const string RequestSignPublicKeyIdHeader = "X-VIRGIL-REQUEST-SIGN-PK-ID";

        private const string URL = "https://keys-private-stg.virgilsecurity.com";
        private const string TestUserId = "heki@inboxstore.me";
        private const string TestPassword = "12345678";
        private const string ApplicationToken = "e872d6f718a2dd0bd8cd7d7e73a25f49";
        
        private readonly Guid TestPublicKeyId = Guid.Parse("fbac633b-346b-1c93-060b-6d29b06e8a64");

        private readonly byte[] PublicKey = Convert.FromBase64String(
            "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUlHYk1CUUdCeXFHU000OUFnRUdDU3NrQXdNQ0NBRUJEUU9CZ2dBRUV5TW9nVUNnWVZhMnIzY294NXp3T1BKVApWRTkwc0RvNDNCVEpzbGIwVGVBcUIxbm5SMzFGdS92TnZYNHJUdHdjaHV0UW1ta3NhRGwwMHRSUmlETUtpMkhrCmVXVHFPMTc2N2R3M2ltQkRtSVFDbkVsNHRxVnhUbDRmQVZicjJubXB4Vm1EbHVrdjV6UlNtQVFRWkJFRm8yTHgKU3lkTitwZlY5dDJxWVUwSEgwUT0KLS0tLS1FTkQgUFVCTElDIEtFWS0tLS0tCg==");

        private readonly byte[] PrivateKey = Convert.FromBase64String(
            "LS0tLS1CRUdJTiBFQyBQUklWQVRFIEtFWS0tLS0tCk1JSGFBZ0VCQkVCUlljZzV4enJtS1oycUV2a0tXamRaMnlZSDliVW1wYnlMN2FPR0p4dnlhUWlQR01uOXk3Mk8KWEdlMVkza3RYeUVIN0FKam0yUEkzZU1wRVNUeHNtUFhvQXNHQ1Nza0F3TUNDQUVCRGFHQmhRT0JnZ0FFRXlNbwpnVUNnWVZhMnIzY294NXp3T1BKVFZFOTBzRG80M0JUSnNsYjBUZUFxQjFublIzMUZ1L3ZOdlg0clR0d2NodXRRCm1ta3NhRGwwMHRSUmlETUtpMkhrZVdUcU8xNzY3ZHczaW1CRG1JUUNuRWw0dHFWeFRsNGZBVmJyMm5tcHhWbUQKbHVrdjV6UlNtQVFRWkJFRm8yTHhTeWROK3BmVjl0MnFZVTBISDBRPQotLS0tLUVORCBFQyBQUklWQVRFIEtFWS0tLS0tCg==");

        [Test]
        public async void Should_AddSignAndPublicKeyIdToHeader_When_SingRequest()
        {
            var body = new
            {
                container_type = "easy",
                password = TestPassword,
                request_sign_random_uuid = Guid.NewGuid().ToString()
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

            var credentials = new Credentials(TestUserId, TestPassword);
            client.Connection.SetCredentials(credentials);
            await client.Connection.Authenticate();

            await client.Container.Remove(TestPublicKeyId, PrivateKey);
        }

        [Test]
        public async void Should_CreateNormalAccount_By_GivenParameters()
        {
            var client = new KeyringClient(new Connection(ApplicationToken, new Uri(URL)));
            
            await client.Container.Initialize(ContainerType.Normal, TestPublicKeyId, PrivateKey, TestPassword);

            var credentials = new Credentials(TestUserId, TestPassword);
            client.Connection.SetCredentials(credentials);
            await client.Connection.Authenticate();
            
            await client.Container.Remove(TestPublicKeyId, PrivateKey);
        }

        [Test]
        public async void Should_DeleteExistingAccount()
        {
            var client = new KeyringClient(new Connection(ApplicationToken, new Uri(URL)));
            
            await client.Container.Initialize(ContainerType.Easy, TestPublicKeyId, PrivateKey, TestPassword);

            var credentials = new Credentials(TestUserId, TestPassword);
            client.Connection.SetCredentials(credentials);

            await client.Container.Remove(TestPublicKeyId, PrivateKey);
        } 
    }
}