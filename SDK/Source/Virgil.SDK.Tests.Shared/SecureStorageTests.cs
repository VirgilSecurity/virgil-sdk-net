using Bogus;
using NUnit.Framework;
using Virgil.SDK.Storage;
using Virgil.SDK.Storage.Exceptions;

namespace Virgil.SDK.Tests
{

    [TestFixture]
    public class SecureStorageTests
    {
        private readonly Faker faker = new Faker();

        [SetUp]
        public void SetUp()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test19";
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

            //var key3 = "dd\\lllll\\aaa";
            storage.Save(key, data);
            storage.Save(key2, data);
            //storage.Save(key3, data);
            var keys = storage.Aliases();
            Assert.AreEqual(keys.Length, 2);
            Assert.AreEqual(keys, new string[]{key, key2});
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
            storage = new SecureStorage(passw2);
            Assert.Throws<SecureStorageException>(
                () => storage.Load(key));
            storage.Delete(key);
        }
    }
}
