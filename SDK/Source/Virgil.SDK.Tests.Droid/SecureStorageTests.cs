using Bogus;
using FluentAssertions;
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
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test85123";
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
            var keys = storage.Keys();
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
            Assert.Throws<KeyNotFoundSecureStorageException>(
                () => storage2.Load(key));
            storage.Delete(key);
        }

        //[Test]
        //public void Test(){
        //    var keyStorage = KeyStore.GetInstance(KeyStore.DefaultType);
        //    var storageIdentity = "Virgil.SecureStorage1234";
        //    var password = "123456".ToCharArray();
        //    try
        //    {
        //        using (var stream = new IsolatedStorageFileStream(storageIdentity, FileMode.Open, FileAccess.Read))
        //        {
        //            keyStorage.Load(stream, password);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var s = e.Message;
        //        // store doesn't exist, create it
        //        keyStorage.Load(null, password);
        //    }

        //    var data = faker.Random.Bytes(32);
        //    var key = "my_key_1";

        //    var keyEntry = new KeyStore.SecretKeyEntry(new KeyEntry(data));
        //    keyStorage.SetEntry(key, keyEntry, new KeyStore.PasswordProtection(password));

        //    using (var stream = new IsolatedStorageFileStream(storageIdentity, FileMode.OpenOrCreate, FileAccess.Write))
        //    {
        //        keyStorage.Store(stream, password);
        //    }

        //    var keyEntry2 = keyStorage.GetEntry(key, new KeyStore.PasswordProtection(password));

        //    var encode = ((KeyStore.SecretKeyEntry)keyEntry2).SecretKey.GetEncoded();


        //    var keyStorage2 = KeyStore.GetInstance(KeyStore.DefaultType);
           


        //        using (var stream = new IsolatedStorageFileStream(storageIdentity, FileMode.Open, FileAccess.Read))
        //        {
        //            keyStorage2.Load(stream, password);
        //        }
            
        //}
    }

}
