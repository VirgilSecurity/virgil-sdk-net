using NUnit.Framework;
using System.Linq;
using Virgil.Crypto;
using Virgil.SDK.Common;
<<<<<<< HEAD
using Virgil.SDK.Storage;
using FluentAssertions;
=======
>>>>>>> 98fc81adead21a4ce8ef9df3bcdd9a464b1bcc8a

namespace Virgil.SDK.Tests
{
    [TestFixture]
    class CryptoTests
    {
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

            Assert.IsTrue(importedKey.Id.SequenceEqual(keyPair.PrivateKey.Id));
            Assert.IsTrue(importedKey.RawKey.SequenceEqual(keyPair.PrivateKey.RawKey));

        }

        [Test]
        public void ImportExportedPublicKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var exportedKey = crypto.ExportPublicKey(keyPair.PublicKey);
            var importedKey = (PublicKey)crypto.ImportPublicKey(exportedKey);
            Assert.IsTrue(importedKey.Id.SequenceEqual(keyPair.PublicKey.Id));
            Assert.IsTrue(importedKey.RawKey.SequenceEqual(keyPair.PublicKey.RawKey));

        }

        [Test]
        public void ExtractPublicKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var extractedPublicKey = crypto.ExtractPublicKey(keyPair.PrivateKey);
<<<<<<< HEAD
            Assert.AreEqual
            ((PublicKey)extractedPublicKey, keyPair.PublicKey);
=======
            Assert.IsTrue(((PublicKey)extractedPublicKey).RawKey.SequenceEqual(keyPair.PublicKey.RawKey));
            Assert.IsTrue(((PublicKey)extractedPublicKey).Id.SequenceEqual(keyPair.PublicKey.Id));

>>>>>>> 98fc81adead21a4ce8ef9df3bcdd9a464b1bcc8a
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
        public void KeyPairId_Should_Be8BytesFromSha512FromPublicKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var keyId = crypto.GenerateHash(keyPair.PublicKey.RawKey, HashAlgorithm.SHA512).Take(8).ToArray();
            Assert.AreEqual(keyPair.PrivateKey.Id, keyId);
            Assert.AreEqual(keyPair.PublicKey.Id, keyId);
        }

        [Test]
        public void ExportPrivateKey_Should_ReturnDerFormat()
        {
            var privateKey2Passw = "qwerty";
            var crypto = new VirgilCrypto();
            var privateKey1 = crypto.ImportPrivateKey(
                Bytes.FromString(IntegrationHelper.PrivateKeySTC31_1, StringEncoding.BASE64));
            var privateKey2 = crypto.ImportPrivateKey(
                Bytes.FromString(IntegrationHelper.PrivateKeySTC31_2, StringEncoding.BASE64), privateKey2Passw);
            var exportedPrivateKey1Bytes = crypto.ExportPrivateKey(privateKey1);
            var privateKeyToDer = VirgilKeyPair.PrivateKeyToDER(((PrivateKey) privateKey1).RawKey);

            var exportedPrivateKey2Bytes = crypto.ExportPrivateKey(privateKey2);
            var privateKeyToDer2 = VirgilKeyPair.PrivateKeyToDER(((PrivateKey)privateKey2).RawKey);

            Assert.IsTrue(privateKeyToDer.SequenceEqual(exportedPrivateKey1Bytes));
            Assert.IsTrue(privateKeyToDer2.SequenceEqual(exportedPrivateKey2Bytes));
        }

        [Test]
        public void ExportPublicKey_Should_ReturnDerFormat()
        {
            var crypto = new VirgilCrypto();
            var publicKey = crypto.ImportPublicKey(
                Bytes.FromString(IntegrationHelper.PublicKeySTC32, StringEncoding.BASE64));
            var exportedPublicKey1Bytes = crypto.ExportPublicKey(publicKey);
            var privateKeyToDer = VirgilKeyPair.PublicKeyToDER(((PublicKey)publicKey).RawKey);
            Assert.IsTrue(privateKeyToDer.SequenceEqual(exportedPublicKey1Bytes));
        }

    }


}
