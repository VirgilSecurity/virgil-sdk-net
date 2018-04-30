using Bogus;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Virgil.Crypto;
using Virgil.SDK.Exceptions;
using Virgil.SDK.Storage;

namespace Virgil.SDK.Tests.Shared
{
    [NUnit.Framework.TestFixture]
    public class PrivateKeyStorageTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void Store_Should_StorePrivateKey()
        {
            var exporterPass = faker.Random.AlphaNumeric(10);
            var storagePass = faker.Random.AlphaNumeric(10);
            var crypto = new VirgilCrypto();
            var privateKey = crypto.GenerateKeys().PrivateKey;
            var exporter = new VirgilPrivateKeyExporter(exporterPass);
            var privateKeyStorage = new PrivateKeyStorage(exporter, storagePass);

            var alias = faker.Random.AlphaNumeric(5);
            var data = new Dictionary<string, string>(){
            {
                faker.Random.AlphaNumeric(10),
                    faker.Random.AlphaNumeric(20)
            } };
            privateKeyStorage.Store(privateKey, alias, data);
            var (loadedPrivateKey, loadedData) = privateKeyStorage.Load(alias);
            //Assert.IsTrue(exporter.ExportPrivatekey(privateKey).SequenceEqual(
             //   exporter.ExportPrivatekey((PrivateKey)loadedPrivateKey)));
            Assert.AreEqual(data, loadedData);
            privateKeyStorage.Delete(alias);
        }

      
        [Test]
        public void Store_Should_RaiseException_IfAliasAlreadyExist()
        {
            var exporterPass = faker.Random.AlphaNumeric(10);
            var storagePass = faker.Random.AlphaNumeric(10);
            var crypto = new VirgilCrypto();
            var privateKey = crypto.GenerateKeys().PrivateKey;
            var exporter = new VirgilPrivateKeyExporter(exporterPass);
            var privateKeyStorage = new PrivateKeyStorage(exporter, storagePass);
            var alias = faker.Random.AlphaNumeric(5);
            var data = new Dictionary<string, string>(){
            {
                faker.Random.AlphaNumeric(10),
                faker.Random.AlphaNumeric(20)
            } };
            privateKeyStorage.Store(privateKey, alias, data);
            Assert.Throws<DuplicateKeyException>(() => privateKeyStorage.Store(privateKey, alias, data));
            privateKeyStorage.Delete(alias);
        }

        [Test]
        public void Store_Should_RaiseException_IfKeyIsAbsent()
        {
            var alias = faker.Random.AlphaNumeric(5);
            var storagePass = faker.Random.AlphaNumeric(10);
            var exporterPass = faker.Random.AlphaNumeric(10);
            var exporter = new VirgilPrivateKeyExporter(exporterPass);
            var privateKeyStorage = new PrivateKeyStorage(exporter, storagePass);
            Assert.Throws<Virgil.SDK.Exceptions.KeyNotFoundException>(
                () => privateKeyStorage.Load(alias)
                );
        }

        [Test]
        public void Delete_Should_RaiseException_IfKeyIsAbsent()
        {
            var alias = faker.Random.AlphaNumeric(5);
            var storagePass = faker.Random.AlphaNumeric(10);
            var exporterPass = faker.Random.AlphaNumeric(10);
            var exporter = new VirgilPrivateKeyExporter(exporterPass);
            var privateKeyStorage = new PrivateKeyStorage(exporter, storagePass);
            Assert.Throws<Virgil.SDK.Exceptions.KeyNotFoundException>(
                () => privateKeyStorage.Delete(alias)
            );
        }

        [Test]
        public void Delete_Should_DeleteKey()
        {
            var exporterPass = faker.Random.AlphaNumeric(10);
            var storagePass = faker.Random.AlphaNumeric(10);
            var crypto = new VirgilCrypto();
            var privateKey = crypto.GenerateKeys().PrivateKey;
            var exporter = new VirgilPrivateKeyExporter(exporterPass);
            var privateKeyStorage = new PrivateKeyStorage(exporter, storagePass);
            var alias = faker.Random.AlphaNumeric(5);
            var data = new Dictionary<string, string>(){
            {
                faker.Random.AlphaNumeric(10),
                faker.Random.AlphaNumeric(20)
            } };
            privateKeyStorage.Store(privateKey, alias, data);
            Assert.IsTrue(privateKeyStorage.Exists(alias));
            privateKeyStorage.Delete(alias);
            Assert.IsFalse(privateKeyStorage.Exists(alias));
        }

        public void Aliases_Should_HaveAllSavedAliases()
        {
            var exporterPass = faker.Random.AlphaNumeric(10);
            var storagePass = faker.Random.AlphaNumeric(10);
            var crypto = new VirgilCrypto();
            var privateKey = crypto.GenerateKeys().PrivateKey;
            var exporter = new VirgilPrivateKeyExporter(exporterPass);
            var privateKeyStorage = new PrivateKeyStorage(exporter, storagePass);
            var alias = faker.Random.AlphaNumeric(5);
            var alias2 = faker.Random.AlphaNumeric(5);

            var data = new Dictionary<string, string>(){
            {
                faker.Random.AlphaNumeric(10),
                faker.Random.AlphaNumeric(20)
            } };
            Assert.IsTrue(privateKeyStorage.Aliases().Length == 0);
            privateKeyStorage.Store(privateKey, alias, data);
            privateKeyStorage.Store(privateKey, alias2, data);

            Assert.IsTrue(privateKeyStorage.Aliases().Length == 2);
            Assert.IsTrue(privateKeyStorage.Aliases().Contains(alias));
            Assert.IsTrue(privateKeyStorage.Aliases().Contains(alias2));

            privateKeyStorage.Delete(alias);
            Assert.IsFalse(privateKeyStorage.Aliases().Contains(alias));
            Assert.IsTrue(privateKeyStorage.Aliases().Contains(alias2));
            privateKeyStorage.Delete(alias2);
            Assert.IsTrue(privateKeyStorage.Aliases().Length == 0);

        }
    }
}
