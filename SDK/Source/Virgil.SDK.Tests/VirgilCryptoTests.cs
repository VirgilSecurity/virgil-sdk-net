namespace Virgil.SDK.Tests
{
    using System.IO;
    using NUnit.Framework;
    using Virgil.SDK.Cryptography;

    public class VirgilCryptoTests
    {
        [Test]
        public void Encrypt_InputAndOutputStreams_ShouldDecryptWithPrivateKeys()
        {
            var crypto = new VirgilCrypto();

            var aliceKeys = crypto.GenerateKeys();

            using (var inputFile = new FileStream(@"C:\Users\Denis\Desktop\test.pdf", FileMode.Open))
            using (var outputFile = new FileStream(@"C:\Users\Denis\Desktop\test.pdf.enc", FileMode.Create))
            {
                crypto.Encrypt(inputFile, outputFile, aliceKeys.PublicKey);
            }

            using (var inputFile = new FileStream(@"C:\Users\Denis\Desktop\test.pdf.enc", FileMode.Open))
            using (var outputFile = new FileStream(@"C:\Users\Denis\Desktop\test-dec.pdf", FileMode.Create))
            {
                crypto.Decrypt(inputFile, outputFile, aliceKeys.PrivateKey);
            }


            ;
        }
    }
}