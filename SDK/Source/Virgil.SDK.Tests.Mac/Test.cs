using System;
using Virgil.SDK.Storage.Exceptions;
using Bogus;
using Virgil.SDK.Storage;
using Xunit;

namespace Virgil.SDK.Tests.Mac
{
    public class SecureStorageTests
    {
        private readonly Faker faker = new Faker();

        [Theory]
        public void Save_Should_SaveDataUnderKey()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test";

            var storage = new SecureStorage();
            var data = faker.Random.Bytes(32);
            var key = faker.Person.UserName;

            storage.Save(key, data);
            var storedData = storage.Load(key);
            Assert.Equal(storedData, data);
            storage.Delete(key);
        }

        [Theory]
        public void Save_Should_SaveDataBetweenSessions()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test";

            var storage = new SecureStorage();
            var data = faker.Random.Bytes(32);
            var key = faker.Person.UserName;

            storage.Save(key, data);
            var storage2 = new SecureStorage();

            var storedData = storage2.Load(key);
            Assert.Equal(storedData, data);
            storage.Delete(key);
        }

        [Theory]
        public void SaveWithDuplicateKey_Should_RaiseDuplicateKeyException()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test";

            var storage = new SecureStorage();
            var data = faker.Random.Bytes(32);
            var key = faker.Person.UserName;

            storage.Save(key, data);
            Assert.Throws<DuplicateKeySecureStorageException>(
                () => storage.Save(key, data));
            storage.Delete(key);
        }

        [Theory]
        public void LoadByMissingKey_Should_RaiseKeyNotFoundException()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test";

            var storage = new SecureStorage();
            var key = faker.Person.UserName;

            Assert.Throws<KeyNotFoundSecureStorageException>(
                () => storage.Load(key));
        }

        [Theory]
        public void DeleteByMissingKey_Should_RaiseKeyNotFoundException()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test";

            var storage = new SecureStorage();
            var key = faker.Person.UserName;
            Assert.Throws<KeyNotFoundSecureStorageException>(
                () => storage.Delete(key));
        }

        [Theory]
        public void Keys_Should_ReturnAllSavedKeys()
        {
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test";

            var storage = new SecureStorage();
            var data = faker.Random.Bytes(32);
            var key = "my_key_1";
            var key2 = "my_key_2";

            storage.Save(key, data);
            storage.Save(key2, data);
            var keys = storage.Aliases();
            Assert.NotEqual(Array.IndexOf(keys, key), -1);
            Assert.NotEqual(Array.IndexOf(keys, key2), -1);

            storage.Delete(key);
            storage.Delete(key2);
        }

    }
}
