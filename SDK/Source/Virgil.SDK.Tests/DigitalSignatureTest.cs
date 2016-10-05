namespace Virgil.SDK.Tests
{
    using System.Text;
    using Cryptography;
    using FluentAssertions;
    using NUnit.Framework;

    public class DigitalSignatureTest
    {
        [Test]
        public void SignData_PrivateKeyGiven_ShouldBeVerified()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();
            var data = Encoding.UTF8.GetBytes("Hello Bob!");

            var signature = crypto.Sign(data, keyPair.PrivateKey);

            crypto.Verify(data, signature, keyPair.PublicKey).Should().BeTrue();
        }
    }
}