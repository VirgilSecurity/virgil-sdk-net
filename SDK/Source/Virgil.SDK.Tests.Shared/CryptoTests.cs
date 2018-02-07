//using FluentAssertions;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Virgil.Crypto;
//using NSubstitute.ExceptionExtensions;
using Virgil.SDK.Common;
using Virgil.SDK.Web;

namespace Virgil.SDK.Tests
{
    [TestFixture]
    class CryptoTests
    {
        private readonly Dictionary<string, object> compatibilityData = 
            Configuration.Serializer.Deserialize < Dictionary<string, object> >(
                File.ReadAllText(IntegrationHelper.CryptoCompatibilityDataPath));
        
        [Test]
        public void GenerateHash_Should_GenerateNonEmptyArray()
        {
            var crypto = new VirgilCrypto();
            var hash = crypto.GenerateHash(Bytes.FromString("hi"));
            Assert.AreNotEqual(hash, null);
        }

        [Test]
        public void ImportExportedPrivateKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var exportedKey = crypto.ExportPrivateKey(keyPair.PrivateKey, "12345");
            var importedKey = (PrivateKey)crypto.ImportPrivateKey(exportedKey, "12345");
            importedKey.ShouldBeEquivalentTo(keyPair.PrivateKey);
        }

        [Test]
        public void ImportExportedPublicKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var exportedKey = crypto.ExportPublicKey(keyPair.PublicKey);
            var importedKey = (PublicKey)crypto.ImportPublicKey(exportedKey);
            importedKey.ShouldBeEquivalentTo(keyPair.PublicKey);
        }

        [Test]
        public void ExtractPublicKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var extractedPublicKey = crypto.ExtractPublicKey(keyPair.PrivateKey);
            ((PublicKey)extractedPublicKey).ShouldBeEquivalentTo(keyPair.PublicKey);
        }

        [Test]
        public void DecryptEncryptedMessage_Should_ReturnEquivalentMessage()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var messageBytes = Bytes.FromString("hi");
            var encryptedData = crypto.Encrypt(messageBytes, keyPair.PublicKey);
            Assert.AreEqual(messageBytes, crypto.Decrypt(encryptedData, keyPair.PrivateKey));
        }

        [Test]
        public void DecryptEncryptedMessageWithWrongPassword_Should_RaiseException()
        {
            var crypto = new VirgilCrypto();
            var aliceKeyPair = crypto.GenerateKeys();
            var bobKeyPair = crypto.GenerateKeys();

            var messageBytes = Bytes.FromString("hi");
            var encryptedDataForAlice = crypto.Encrypt(messageBytes, aliceKeyPair.PublicKey);
            Assert.Throws<VirgilCryptoException>(() => crypto.Decrypt(encryptedDataForAlice, bobKeyPair.PrivateKey));
        }

        [Test]
        public void GenerateSignature_Should_ReturnValidSignature()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var snapshot = Bytes.FromString("some card snapshot");
            var signatureSnapshot = Bytes.FromString("some signature snapshot");
            var extendedSnapshot = signatureSnapshot != null
                ? Bytes.Combine(snapshot, signatureSnapshot)
                : snapshot;
            var signature = crypto.GenerateSignature(extendedSnapshot, keyPair.PrivateKey);
            Assert.IsTrue(crypto.VerifySignature(signature, extendedSnapshot, keyPair.PublicKey));
        }

        [Test]
        public void Decrypt_Should_BeEqualToTestData()
        {
            var crypto = new VirgilCrypto();
            var testData = (Dictionary<string, string>)compatibilityData["encrypt_single_recipient"];
            var privateKey = crypto.ImportPrivateKey(Bytes.FromString(testData["private_key"], StringEncoding.BASE64));
            var publicKey = crypto.ExtractPublicKey(privateKey);
            var data = Bytes.FromString(testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString(testData["cipher_data"], StringEncoding.BASE64);
            Assert.IsTrue(crypto.Decrypt(cipherData, privateKey).SequenceEqual(data));
        }

        [Test]
        public void DecryptThenVerify_Should_BeEqualToTestData()
        {
            var crypto = new VirgilCrypto();
            var testData = (Dictionary<string, string>)compatibilityData["sign_then_encrypt_single_recipient"];
            var privateKey = crypto.ImportPrivateKey(Bytes.FromString(testData["private_key"], StringEncoding.BASE64));
            var publicKey = crypto.ExtractPublicKey(privateKey);
            var data = Bytes.FromString(testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString(testData["cipher_data"], StringEncoding.BASE64);
            Assert.IsTrue(crypto.DecryptThenVerify(cipherData, privateKey, publicKey).SequenceEqual(data));
        }

        [Test]
        public void DecryptForMultipleRecipients_Should_BeEqualToTestData()
        {
            var jsonStr = File.ReadAllText("");
            var crypto = new VirgilCrypto();

            var allTestdata = Configuration.Serializer.Deserialize<Dictionary<object, object>>(jsonStr);
            var testData = (Dictionary<string, object>)allTestdata["encrypt_multiple_recipients"];
            var privateKeys = ((string[]) testData["private_keys"]).Select(x =>
                crypto.ImportPrivateKey(Bytes.FromString(x, StringEncoding.BASE64)));
            var data = Bytes.FromString((string)testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString((string)testData["cipher_data"], StringEncoding.BASE64);
            foreach (var privateKey in privateKeys)
            {
            Assert.IsTrue(crypto.Decrypt(cipherData, privateKey).SequenceEqual(data));

            }
        }

        [Test]
        public void DecryptThenVerifyForMultipleRecipients_Should_BeEqualToTestData()
        {
            var jsonStr = File.ReadAllText("");
            var crypto = new VirgilCrypto();

            var allTestdata = Configuration.Serializer.Deserialize<Dictionary<object, object>>(jsonStr);
            var testData = (Dictionary<string, object>)allTestdata["sign_then_encrypt_multiple_recipients"];
            var privateKeys = ((string[])testData["private_keys"]).Select(x =>
                crypto.ImportPrivateKey(Bytes.FromString(x, StringEncoding.BASE64)));
            var publicKeys = privateKeys.Select(x => crypto.ExtractPublicKey(x)).ToArray();
            var signerPublicKey = publicKeys.First();
            var data = Bytes.FromString((string)testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString((string)testData["cipher_data"], StringEncoding.BASE64);
            foreach (var privateKey in privateKeys)
            {
                Assert.IsTrue(crypto.DecryptThenVerify(cipherData, privateKey, signerPublicKey).SequenceEqual(data));
            }
        }

        [Test]
        public void DecryptThenVerifytForMultipleSigners_Should_BeEqualToTestData()
        {
            var jsonStr = File.ReadAllText("");
            var crypto = new VirgilCrypto();

            var allTestdata = Configuration.Serializer.Deserialize<Dictionary<object, object>>(jsonStr);
            var testData = (Dictionary<string, object>)allTestdata["sign_then_encrypt_multiple_signers"];
            var privateKey = crypto.ImportPrivateKey(Bytes.FromString((string)testData["private_key"], StringEncoding.BASE64));

            var publicKeys = ((string[])testData["public_keys"]).Select(x =>
                crypto.ImportPublicKey(Bytes.FromString(x, StringEncoding.BASE64)));
            var data = Bytes.FromString((string)testData["original_data"], StringEncoding.BASE64);
            var cipherData = Bytes.FromString((string)testData["cipher_data"], StringEncoding.BASE64);
            foreach (var publicKey in publicKeys)
            {
                Assert.IsTrue(crypto.DecryptThenVerify(cipherData, privateKey, publicKey).SequenceEqual(data));
            }
        }

        [Test]
        public void VerifySignature_Should_BeTrueForTestData()
        {
            var crypto = new VirgilCrypto();
            var testData = (Dictionary<string, string>)compatibilityData["generate_signature"];
            var privateKey = crypto.ImportPrivateKey(Bytes.FromString(testData["private_key"], StringEncoding.BASE64));
            var publicKey = crypto.ExtractPublicKey(privateKey);
            var data = Bytes.FromString(testData["original_data"], StringEncoding.BASE64);
            var signature = Bytes.FromString(testData["signature"], StringEncoding.BASE64);
            Assert.IsTrue(crypto.VerifySignature(signature, data, publicKey));
        }

    }


}
