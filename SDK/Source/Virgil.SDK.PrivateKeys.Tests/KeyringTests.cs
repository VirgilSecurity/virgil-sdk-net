namespace Virgil.SDK.PrivateKeys.Tests
{
    using System;
    using System.Text;
    using System.Linq;

    using FluentAssertions;
    
    using NUnit.Framework;

    using Virgil.Crypto;

    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;

    public class KeyringTests
    {
        private readonly Guid TestAccountId = Guid.Parse("2775e79c-ffba-877a-d183-e4fad453e266");
        private readonly Guid PublicKeyId = Guid.Parse("d2aa2087-83c9-7bb7-2982-036049d73ede");

        private readonly byte[] PrivateKey = Convert.FromBase64String(
            "LS0tLS1CRUdJTiBFQyBQUklWQVRFIEtFWS0tLS0tCk1JSGFBZ0VCQkVBV1hYR05Hb1NKREluMXc2YytWaWxRa2pjSnJ0cmkvZVJFZzFrdS9UaTE2c255US9sRU0zb1QKS201MDMyNGhxb2JoaDhKSDNNV2p6T1J4LzVWaGFvVEFvQXNHQ1Nza0F3TUNDQUVCRGFHQmhRT0JnZ0FFbjhveQp1RGwwRllXSWFKczdwWFNJUDRDZGF4KytWend4T1g4YTU2R2tJSUhBem12RnZ6WlZQOEJLcjVEVkhlZXRKdkRSCmIrcVR3eTZWUWZEdGRIL0wxVTdmTHROZENCSTEzRDZCL0o1VDBBRndQZnYrUWNRWFFsTytMek9KN1dGOHhkb2kKWVlXTmtwaGFCYkZseE0vbHo0TkVDM1ZwVEMzU01wSTBmR2tyNERNPQotLS0tLUVORCBFQyBQUklWQVRFIEtFWS0tLS0tCg==");

        [Test]
        public async void Should_CreateEasyAccount_By_GivenParameters()
        {
            var client = new KeyringClient(new Connection(new Uri("https://keys-private-stg.virgilsecurity.com/v2/")));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(PublicKeyId.ToString()), PrivateKey);

            await client.Accounts.Create(TestAccountId, PrivateKeysAccountType.Easy, PublicKeyId, sign, "12345678");

            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            client.Connection.SetCredentials(credentials);
            
            var createdAccount = await client.Accounts.Get(TestAccountId);

            createdAccount.Should().NotBeNull();
            createdAccount.AccountId.Should().Be(TestAccountId);
            createdAccount.Type.Should().Be(PrivateKeysAccountType.Easy);
            createdAccount.PrivateKeys.Should().BeEmpty();

            await client.Accounts.Remove(TestAccountId, PublicKeyId, sign);
        }

        [Test]
        public async void Should_CreateNormalAccount_By_GivenParameters()
        {
            var client = new KeyringClient(new Connection(new Uri("https://keys-private-stg.virgilsecurity.com/v2/")));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(PublicKeyId.ToString()), PrivateKey);

            await client.Accounts.Create(TestAccountId, PrivateKeysAccountType.Normal, PublicKeyId, sign, "12345678");

            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            client.Connection.SetCredentials(credentials);

            var createdAccount = await client.Accounts.Get(TestAccountId);

            createdAccount.Should().NotBeNull();
            createdAccount.AccountId.Should().Be(TestAccountId);
            createdAccount.Type.Should().Be(PrivateKeysAccountType.Normal);
            createdAccount.PrivateKeys.Should().BeEmpty();

            await client.Accounts.Remove(TestAccountId, PublicKeyId, sign);
        }

        [Test]
        public async void Should_DeleteExistingAccount()
        {
            var client = new KeyringClient(new Connection(new Uri("https://keys-private-stg.virgilsecurity.com/v2/")));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(PublicKeyId.ToString()), PrivateKey);

            await client.Accounts.Create(TestAccountId, PrivateKeysAccountType.Easy, PublicKeyId, sign, "12345678");

            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            client.Connection.SetCredentials(credentials);

            await client.Accounts.Remove(TestAccountId, PublicKeyId, sign);
        }

        [Test]
        public async void Should_AddPrivateKeyToExistingAccount()
        {
            var client = new KeyringClient(new Connection(new Uri("https://keys-private-stg.virgilsecurity.com/v2/")));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(PublicKeyId.ToString()), PrivateKey);

            await client.Accounts.Create(TestAccountId, PrivateKeysAccountType.Easy, PublicKeyId, sign, "12345678");

            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            client.Connection.SetCredentials(credentials);

            await client.PrivateKeys.Add(TestAccountId, PublicKeyId, sign, PrivateKey);

            var privateKey = await client.PrivateKeys.Get(PublicKeyId);

            privateKey.Should().NotBeNull();
            privateKey.PublicKeyId.Should().Be(PublicKeyId);

            await client.Accounts.Remove(TestAccountId, PublicKeyId, sign);
        }

        [Test]
        public async void Should_RemovePrivateKeyFromExistingAccount()
        {
            var client = new KeyringClient(new Connection(new Uri("https://keys-private-stg.virgilsecurity.com/v2/")));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(PublicKeyId.ToString()), PrivateKey);

            await client.Accounts.Create(TestAccountId, PrivateKeysAccountType.Easy, PublicKeyId, sign, "12345678");

            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            client.Connection.SetCredentials(credentials);

            await client.PrivateKeys.Add(TestAccountId, PublicKeyId, sign, PrivateKey);

            var privateKey = await client.PrivateKeys.Get(PublicKeyId);

            privateKey.Should().NotBeNull();
            privateKey.PublicKeyId.Should().Be(PublicKeyId);

            await client.PrivateKeys.Remove(PublicKeyId, sign);

            privateKey = await client.PrivateKeys.Get(PublicKeyId);

            privateKey.Should().BeNull();
        }

        [Test]
        public async void Should_ReturnAllAccountsPrivateKeys()
        {
            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            var client = new KeyringClient(new Connection(credentials, new Uri("https://keys-private-stg.virgilsecurity.com/v2/")));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(PublicKeyId.ToString()), PrivateKey);

            var privateKeys = await client.PrivateKeys.GetAll(TestAccountId);
            privateKeys.Count().Should().Be(1);
        }

        [Test]
        public async void Should_ReturnPrivateKeyByPublicKey()
        {
            var credentials = new Credentials("test-virgil@divermail.com", "12345678");
            var client = new KeyringClient(new Connection(credentials, new Uri("https://keys-private-stg.virgilsecurity.com/v2/")));

            var privateKey = await client.PrivateKeys.Get(PublicKeyId);
            privateKey.PublicKeyId.Should().Be(PublicKeyId);
        }
    }
}