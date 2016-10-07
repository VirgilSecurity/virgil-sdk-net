namespace Virgil.SDK.Tests
{
    using System.IO;
    using System.Text;
    using Cryptography;
    using FluentAssertions;
    using NUnit.Framework;

    public class DigitalSignatureTest
    {
        [Test]
        public void SignData_PlainTextGiven_ShouldPassVerfifcation()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();
            var data = Encoding.UTF8.GetBytes("Hello Bob!");

            var signature = crypto.Sign(data, keyPair.PrivateKey);

            crypto.Verify(data, signature, keyPair.PublicKey).Should().BeTrue();
        }

        [Test]
        public void SignData_StreamGiven_ShouldPassVerification()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();
            var data = Encoding.UTF8.GetBytes("Hello Bob!");

            byte[] signature;

            using (var inputStream = new MemoryStream(data))
            {
                signature = crypto.Sign(inputStream, keyPair.PrivateKey);
            }

            using (var inputStream = new MemoryStream(data))
            {
                crypto.Verify(inputStream, signature, keyPair.PublicKey).Should().BeTrue();
            }
        }
    }
}