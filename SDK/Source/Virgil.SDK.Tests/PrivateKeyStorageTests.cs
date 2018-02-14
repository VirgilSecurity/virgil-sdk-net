using Bogus;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Virgil.Crypto;
using Virgil.SDK.Crypto;
using Virgil.SDK.Storage;
using Virgil.SDK.Storage.Exceptions;

namespace Virgil.SDK.Tests.Shared
{
    [NUnit.Framework.TestFixture]
    public class PrivateKeyStorageTestsNet
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void Load_Should_Return_EqualOldPrivateKey_If_StorageHasSHA256Exporter()
        {
            var exporterPass = faker.Random.AlphaNumeric(10);
            var storagePass = faker.Random.AlphaNumeric(10);
            var crypto = new VirgilCrypto();
            var cryptoSHA256 = new VirgilCrypto() { UseSHA256Fingerprints = true };

            var exporter = new VirgilPrivateKeyExporter(cryptoSHA256, exporterPass);
            var privateKeyStorage = new PrivateKeyStorage(exporter, storagePass);

            var storageReader =
                new KeyStorageReaderV4(IntegrationHelper.OldKeyStoragePath, true);
            var (oldKeyData, oldKeydata) = storageReader.Load(IntegrationHelper.OldKeyAliase);
            var key = cryptoSHA256.ImportPrivateKey(oldKeyData);
            var oldKeyAliase = faker.Random.AlphaNumeric(10);
            privateKeyStorage.Store(key, oldKeyAliase);
            var loadedOldKey = privateKeyStorage.Load(oldKeyAliase).Item1;
            
            Assert.IsTrue(((PrivateKey)key).Id.SequenceEqual(((PrivateKey)loadedOldKey).Id));
            Assert.IsTrue(((PrivateKey)key).RawKey.SequenceEqual(((PrivateKey)loadedOldKey).RawKey));
            privateKeyStorage.Delete(oldKeyAliase);
        }
    }
}
