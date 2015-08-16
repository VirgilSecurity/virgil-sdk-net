namespace Virgil.SDK.PrivateKeys.Tests
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    using FluentAssertions;
    
    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.PrivateKeys.Exceptions;
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;

    public class PrivateKeysTests
    {
        private const string URL = "https://keys-private-stg.virgilsecurity.com/v2/";
        private const string TestUserId = "test-virgil@divermail.com";
        private const string TestPassword = "12345678";
        public const string ApplicationToken = "";

        private readonly Guid TestAccountId = Guid.Parse("2775e79c-ffba-877a-d183-e4fad453e266");
        private readonly Guid TestPublicKeyId = Guid.Parse("d2aa2087-83c9-7bb7-2982-036049d73ede");

        private readonly byte[] PrivateKey = Convert.FromBase64String(
            "LS0tLS1CRUdJTiBFQyBQUklWQVRFIEtFWS0tLS0tCk1JSGFBZ0VCQkVBV1hYR05Hb1NKREluMXc2YytWaWxRa2pjSnJ0cmkvZVJFZzFrdS9UaTE2c255US9sRU0zb1QKS201MDMyNGhxb2JoaDhKSDNNV2p6T1J4LzVWaGFvVEFvQXNHQ1Nza0F3TUNDQUVCRGFHQmhRT0JnZ0FFbjhveQp1RGwwRllXSWFKczdwWFNJUDRDZGF4KytWend4T1g4YTU2R2tJSUhBem12RnZ6WlZQOEJLcjVEVkhlZXRKdkRSCmIrcVR3eTZWUWZEdGRIL0wxVTdmTHROZENCSTEzRDZCL0o1VDBBRndQZnYrUWNRWFFsTytMek9KN1dGOHhkb2kKWVlXTmtwaGFCYkZseE0vbHo0TkVDM1ZwVEMzU01wSTBmR2tyNERNPQotLS0tLUVORCBFQyBQUklWQVRFIEtFWS0tLS0tCg==");

        public async Task Setup()
        {
            var connection = new Connection(ApplicationToken, new Uri(URL));
            var client = new KeyringClient(connection);

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(TestPublicKeyId.ToString()), PrivateKey);

            try
            {
                await client.Container.Initialize(ContainerType.Easy, TestPublicKeyId, sign, TestPassword);
                return;
            }
            catch (PrivateKeysServiceException ex)
            {
                // check if account already exists.
                if (ex.ErrorCode != 40003)
                    throw;
            }

            client.Connection.SetCredentials(new Credentials(TestUserId, TestPassword));

            // remove previously created account.
            await client.Container.Remove(TestPublicKeyId, sign);

            // try to create account again.
            await client.Container.Initialize(ContainerType.Easy, TestPublicKeyId, sign, TestPassword);
        }

        public async Task TearDown()
        {
            var connection = new Connection(ApplicationToken, new Uri(URL));
            var client = new KeyringClient(connection);

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(TestPublicKeyId.ToString()), PrivateKey);

            client.Connection.SetCredentials(new Credentials(TestUserId, TestPassword));

            // remove previously created account.
            await client.Container.Remove(TestPublicKeyId, sign);
        }
        
        [Test]
        public async void Should_AddPrivateKeyToExistingAccount()
        {
            await this.Setup();

            var client = new KeyringClient(new Connection(ApplicationToken, new Credentials(TestUserId, TestPassword), new Uri(URL)));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(TestPublicKeyId.ToString()), PrivateKey);
            
            await client.PrivateKeys.Add(TestPublicKeyId, sign, PrivateKey);
            var privateKey = await client.PrivateKeys.Get(TestPublicKeyId);

            privateKey.Should().NotBeNull();
            privateKey.PublicKeyId.Should().Be(TestPublicKeyId);

            await this.TearDown();
        }

        [Test, ExpectedException(typeof(PrivateKeyOperationException))]
        public async void Should_RemovePrivateKeyFromExistingAccount()
        {
            await this.Setup();

            var client = new KeyringClient(new Connection(ApplicationToken, new Credentials(TestUserId, TestPassword), new Uri(URL)));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(TestPublicKeyId.ToString()), PrivateKey);
            
            await client.PrivateKeys.Add(TestPublicKeyId, sign, PrivateKey);

            var privateKey = await client.PrivateKeys.Get(TestPublicKeyId);

            privateKey.Should().NotBeNull();
            privateKey.PublicKeyId.Should().Be(TestPublicKeyId);

            await client.PrivateKeys.Remove(TestPublicKeyId, sign);

            privateKey = await client.PrivateKeys.Get(TestPublicKeyId);

            privateKey.Should().BeNull();

            await this.TearDown();
        }

        [Test]
        public async void Should_ReturnAllAccountsPrivateKeys()
        {
            await this.Setup();

            var client = new KeyringClient(new Connection(ApplicationToken, new Credentials(TestUserId, TestPassword), new Uri(URL)));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(TestPublicKeyId.ToString()), PrivateKey);

            await client.PrivateKeys.Add(TestPublicKeyId, sign, PrivateKey);
            
            //var privateKeys = await client.PrivateKeys.GetAll(TestAccountId);
            //privateKeys.Count().Should().Be(1);

            await this.TearDown();
        }

        [Test]
        public async void Should_ReturnPrivateKeyByPublicKey()
        {
            await this.Setup();

            var client = new KeyringClient(new Connection(ApplicationToken, new Credentials(TestUserId, TestPassword), new Uri(URL)));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(TestPublicKeyId.ToString()), PrivateKey);

            await client.PrivateKeys.Add(TestPublicKeyId, sign, PrivateKey);

            var privateKey = await client.PrivateKeys.Get(TestPublicKeyId);
            privateKey.PublicKeyId.Should().Be(TestPublicKeyId);

            await this.TearDown();
        }
    }
}