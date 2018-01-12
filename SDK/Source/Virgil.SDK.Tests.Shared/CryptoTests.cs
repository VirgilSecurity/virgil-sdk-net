﻿//using FluentAssertions;

using FluentAssertions;
using NUnit.Framework;
using Virgil.Crypto;
//using NSubstitute.ExceptionExtensions;
using Virgil.SDK.Common;

namespace Virgil.SDK.Tests
{
    [TestFixture]
    class CryptoTests
    {
        [Test]
        public void ImportExportedPrivateKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateVirgilKeys();
            var exportedKey = crypto.ExportVirgilPrivateKey(keyPair.PrivateKey, "12345");
            var importedKey = (PrivateKey)crypto.ImportVirgilPrivateKey(exportedKey, "12345");
            importedKey.ShouldBeEquivalentTo(keyPair.PrivateKey);
        }

        [Test]
        public void ImportExportedPublicKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateVirgilKeys();
            var exportedKey = crypto.ExportPublicKey(keyPair.PublicKey);
            var importedKey = (PublicKey)crypto.ImportPublicKey(exportedKey);
            importedKey.ShouldBeEquivalentTo(keyPair.PublicKey);
        }

        [Test]
        public void ExtractPublicKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateVirgilKeys();
            var extractedPublicKey = crypto.ExtractVirgilPublicKey(keyPair.PrivateKey);
            ((PublicKey)extractedPublicKey).ShouldBeEquivalentTo(keyPair.PublicKey);
        }

        [Test]
        public void DecryptEncryptedMessage_Should_ReturnEquivalentMessage()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateVirgilKeys();
            var messageBytes = Bytes.FromString("hi");
            var encryptedData = crypto.Encrypt(messageBytes, keyPair.PublicKey);
            Assert.AreEqual(messageBytes, crypto.Decrypt(encryptedData, keyPair.PrivateKey));
        }

        [Test]
        public void DecryptEncryptedMessageWithWrongPassword_Should_RaiseException()
        {
            var crypto = new VirgilCrypto();
            var aliceKeyPair = crypto.GenerateVirgilKeys();
            var bobKeyPair = crypto.GenerateVirgilKeys();

            var messageBytes = Bytes.FromString("hi");
            var encryptedDataForAlice = crypto.Encrypt(messageBytes, aliceKeyPair.PublicKey);
            Assert.Throws<VirgilCryptoException>(() => crypto.Decrypt(encryptedDataForAlice, bobKeyPair.PrivateKey));
        }

    }
}