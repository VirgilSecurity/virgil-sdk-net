namespace Virgil.SDK.PrivateKeys.Tests
{
    using System;
    using System.Text;
    
    using NUnit.Framework;

    using Virgil.Crypto;

    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;

    public class AccountsTests
    {
        private readonly Guid TestAccountId = Guid.Parse("2775e79c-ffba-877a-d183-e4fad453e266");
        private readonly Guid PublicKeyId = Guid.Parse("d2aa2087-83c9-7bb7-2982-036049d73ede");

        private readonly byte[] PrivateKey = Convert.FromBase64String(
            "LS0tLS1CRUdJTiBFQyBQUklWQVRFIEtFWS0tLS0tCk1JSGFBZ0VCQkVBV1hYR05Hb1NKREluMXc2YytWaWxRa2pjSnJ0cmkvZVJFZzFrdS9UaTE2c255US9sRU0zb1QKS201MDMyNGhxb2JoaDhKSDNNV2p6T1J4LzVWaGFvVEFvQXNHQ1Nza0F3TUNDQUVCRGFHQmhRT0JnZ0FFbjhveQp1RGwwRllXSWFKczdwWFNJUDRDZGF4KytWend4T1g4YTU2R2tJSUhBem12RnZ6WlZQOEJLcjVEVkhlZXRKdkRSCmIrcVR3eTZWUWZEdGRIL0wxVTdmTHROZENCSTEzRDZCL0o1VDBBRndQZnYrUWNRWFFsTytMek9KN1dGOHhkb2kKWVlXTmtwaGFCYkZseE0vbHo0TkVDM1ZwVEMzU01wSTBmR2tyNERNPQotLS0tLUVORCBFQyBQUklWQVRFIEtFWS0tLS0tCg==");

        [Test]
        public async void Should_ReturnStatusCode200_When_AccountCreatedSuccessfully()
        {
            var client = new KeyringClient(new Connection(new Uri("https://keys-private-stg.virgilsecurity.com/v2/")));

            var signer = new VirgilSigner();
            var sign = signer.Sign(Encoding.UTF8.GetBytes(PublicKeyId.ToString()), PrivateKey);

            await client.Accounts.Create(TestAccountId, PrivateKeysAccountType.Easy, PublicKeyId, sign, "12345678");
        }
    }
}