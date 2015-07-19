namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Http;
    using NUnit.Framework;

    using Virgil.SDK.Keys.Model;

    public class SignsTests
    {
        [Test]
        public async void Should_CreateCorrectSign()
        {
            var client = new PkiClient("APP-TOKEN");

            var keyPair = new Virgil.Crypto.VirgilKeyPair();

            var account = await client.Accounts
                .Register(UserDataType.EmailId, "test100@virgilsecurity.com", keyPair.PublicKey());

            var publicKeys = await client.PublicKeys.Search("test100@virgilsecurity.com", UserDataType.EmailId);
            var pk = publicKeys.First();

            var signer = new Virgil.Crypto.VirgilSigner();
            var sign = signer.Sign(System.Text.Encoding.UTF8.GetBytes(pk.PublicKeyId.ToString()), keyPair.PrivateKey());
        }
    }
  
}