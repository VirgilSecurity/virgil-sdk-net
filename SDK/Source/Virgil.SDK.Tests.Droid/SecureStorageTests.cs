using System;
using Bogus;
using NUnit.Framework;
using Virgil.SDK;
using Virgil.SDK.Storage.Exceptions;

namespace AndroidTestApp
{
    [TestFixture]
    public class SecureStorageTests
    {
        private readonly Faker faker = new Faker();

        [SetUp]
        public void SetUp()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test";
        }

        [Test]
        public void Save_Should_SaveDataUnderKey()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var data = faker.Random.Bytes(32);
            var key = faker.Person.UserName;

            storage.Save(key, data);
            var storedData = storage.Load(key);
            Assert.AreEqual(storedData, data);
            storage.Delete(key);
        }

        [Test]
        public void Save_Should_SaveDataBetweenSessions()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var data = faker.Random.Bytes(32);
            var key = faker.Person.UserName;

            storage.Save(key, data);
            var storage2 = new SecureStorage(passw);

            var storedData = storage2.Load(key);
            Assert.AreEqual(storedData, data);
            storage.Delete(key);
        }

        [Test]
        public void SaveWithDuplicateKey_Should_RaiseDuplicateKeyException()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var data = faker.Random.Bytes(32);
            var key = faker.Person.UserName;

            storage.Save(key, data);
            Assert.Throws<DuplicateKeySecureStorageException>(
                () => storage.Save(key, data));
            storage.Delete(key);
        }

        [Test]
        public void LoadByMissingKey_Should_RaiseKeyNotFoundException()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var key = faker.Person.UserName;
            
            Assert.Throws<KeyNotFoundSecureStorageException>(
                () => storage.Load(key));
        }

        [Test]
        public void DeleteByMissingKey_Should_RaiseKeyNotFoundException()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var key = faker.Person.UserName;
            Assert.Throws<KeyNotFoundSecureStorageException>(
                () => storage.Delete(key));
        }

        [Test]
        public void Keys_Should_ReturnAllSavedKeys()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var data = faker.Random.Bytes(32);
            var key = "my_key_1";
            var key2 = "my_key_2";

            storage.Save(key, data);
            storage.Save(key2, data);
            var keys = storage.Aliases();
            Assert.AreNotEqual(Array.IndexOf(keys, key), -1);
            Assert.AreNotEqual(Array.IndexOf(keys, key2), -1);

            storage.Delete(key);
            storage.Delete(key2);
        }

        [Test]
        public void LoadByWrongPass_Should_RaiseKeyNotFoundException()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var key = faker.Person.UserName;
            var data = faker.Random.Bytes(32);
            storage.Save(key, data);
            //change pass
            var passw2 = faker.Random.Words();
            var storage2 = new SecureStorage(passw2);
            Assert.Throws<KeyNotFoundSecureStorageException>(
                () => storage2.Load(key));
            storage.Delete(key);
        }

    }

}
