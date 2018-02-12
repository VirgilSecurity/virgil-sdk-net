using System.Collections.Generic;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Virgil.SDK.Storage;
using Virgil.SDK.Storage.Exceptions;
using KeyNotFoundException = Virgil.SDK.Storage.Exceptions.KeyNotFoundException;

namespace Virgil.SDK.Tests
{

    [TestFixture]
    public class SecureStorageTests
    {
        private readonly Faker faker = new Faker();

        [SetUp]
        public void SetUp()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Tests5";
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
            storedData.ShouldBeEquivalentTo(data);
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
            storedData.ShouldBeEquivalentTo(data);
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
            Assert.Throws<DuplicateKeyException>(
                () => storage.Save(key, data));
            storage.Delete(key);
        }

        [Test]
        public void LoadByMissingKey_Should_RaiseKeyNotFoundException()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var key = faker.Person.UserName;
            
            Assert.Throws<KeyNotFoundException>(
                () => storage.Load(key));
        }

        [Test]
        public void DeleteByMissingKey_Should_RaiseKeyNotFoundException()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var key = faker.Person.UserName;
            Assert.Throws<KeyNotFoundException>(
                () => storage.Delete(key));
        }

        [Test]
        public void Keys_Should_ReturnAllSavedKeys()
        {
            var passw = faker.Random.Words();
            var storage = new SecureStorage(passw);
            var data = faker.Random.Bytes(32);
            var key = "my_key_11";
            var key2 = "my_key_22";

            //var key3 = "dd\\lllll\\aaa";
            storage.Save(key, data);
            storage.Save(key2, data);
            //storage.Save(key3, data);
            var keys = storage.Aliases();
            keys.Length.ShouldBeEquivalentTo(2);
            keys.ShouldBeEquivalentTo(new string[]{key, key2});
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
            Assert.Throws<SecureStorageException>(
                () => storage2.Load(key));
            storage.Delete(key);
        }
    }
}
