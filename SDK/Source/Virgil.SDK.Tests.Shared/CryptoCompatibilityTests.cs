using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using Virgil.Crypto;
using Virgil.SDK.Common;

namespace Virgil.SDK.Tests.Shared
{
    public class CryptoCompatibilityTests
    {
        private readonly Dictionary<string, Dictionary<string, dynamic>> compatibilityData =
            JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(AppSettings.CryptoCompatibilityData);
        private readonly VirgilCrypto cryptoSHA256 = new VirgilCrypto() { UseSHA256Fingerprints = true };

        [Test]
        public void Decrypt_Should_BeEqualToTestData()
        {
            var testData = compatibilityData["encrypt_single_recipient"];
            var bytes = Bytes.FromString(testData["private_key"], StringEncoding.BASE64);
            var privateKey = cryptoSHA256.ImportPrivateKey(bytes);
            var publicKey = cryptoSHA256.ExtractPublicKey(privateKey);
            var data = Bytes.FromString(testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString(testData["cipher_data"], StringEncoding.BASE64);
            Assert.AreEqual(cryptoSHA256.Decrypt(cipherData, privateKey), (byte[])data);
        }

        [Test]
        public void DecryptThenVerify_Should_BeEqualToTestData()
        {
            var crypto = new VirgilCrypto() { UseSHA256Fingerprints = true };
            var testData = compatibilityData["sign_then_encrypt_single_recipient"];
            var privateKey = crypto.ImportPrivateKey(Bytes.FromString(testData["private_key"], StringEncoding.BASE64));
            var publicKey = crypto.ExtractPublicKey(privateKey);
            var data = Bytes.FromString(testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString(testData["cipher_data"], StringEncoding.BASE64);
            Assert.AreEqual(crypto.DecryptThenVerify(cipherData, privateKey, publicKey), data);
        }

        [Test]
        public void DecryptForMultipleRecipients_Should_BeEqualToTestData()
        {
            var testData = compatibilityData["encrypt_multiple_recipients"];
            var privateKeysBase64 = testData["private_keys"].ToObject<string[]>();
            var privateKeys = ((string[])privateKeysBase64).Select(x =>
               cryptoSHA256.ImportPrivateKey(Bytes.FromString((string)x, StringEncoding.BASE64)));
            var data = Bytes.FromString((string)testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString((string)testData["cipher_data"], StringEncoding.BASE64);
            foreach (var privateKey in privateKeys)
            {
                Assert.IsTrue(cryptoSHA256.Decrypt(cipherData, privateKey).SequenceEqual(data));
            }
        }

        [Test]
        public void DecryptThenVerifyForMultipleRecipients_Should_BeEqualToTestData()
        {
            var testData = compatibilityData["sign_then_encrypt_multiple_recipients"];
            var privateKeysBase64 = testData["private_keys"].ToObject<string[]>();
            
            var privateKeys = ((string[])privateKeysBase64).Select(x =>
                cryptoSHA256.ImportPrivateKey(Bytes.FromString(x, StringEncoding.BASE64)));
            var publicKeys = privateKeys.Select(x => cryptoSHA256.ExtractPublicKey(x)).ToArray();
            var signerPublicKey = publicKeys.First();
            var data = Bytes.FromString((string)testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString((string)testData["cipher_data"], StringEncoding.BASE64);
            foreach (var privateKey in privateKeys)
            {
                Assert.IsTrue(cryptoSHA256.DecryptThenVerify(cipherData, privateKey, signerPublicKey).SequenceEqual(data));
            }
        }

        [Test]
        public void DecryptThenVerifytForMultipleSigners_Should_BeEqualToTestData()
        {
            var testData = compatibilityData["sign_then_encrypt_multiple_signers"];
            var privateKey = cryptoSHA256.ImportPrivateKey(
                Bytes.FromString((string)testData["private_key"], StringEncoding.BASE64));

            var publicKeysBase64 = testData["public_keys"].ToObject<string[]>();
                var publicKeys = ((string[])publicKeysBase64).Select(x =>
                cryptoSHA256.ImportPublicKey(Bytes.FromString(x, StringEncoding.BASE64))).ToArray();
            var data = Bytes.FromString((string)testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString((string)testData["cipher_data"], StringEncoding.BASE64);
                Assert.IsTrue(cryptoSHA256.DecryptThenVerify(cipherData, privateKey, publicKeys).SequenceEqual(data));
        }

        [Test]
        public void VerifySignature_Should_BeTrueForTestData()
        {
            var testData = compatibilityData["generate_signature"];
            var privateKey = cryptoSHA256.ImportPrivateKey(Bytes.FromString(testData["private_key"], StringEncoding.BASE64));
            var publicKey = cryptoSHA256.ExtractPublicKey(privateKey);
            var data = Bytes.FromString(testData["original_data"], StringEncoding.BASE64);
            var signature = Bytes.FromString(testData["signature"], StringEncoding.BASE64);
            Assert.IsTrue(cryptoSHA256.VerifySignature(signature, data, publicKey));
        }
    }
}