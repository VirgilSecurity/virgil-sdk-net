namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Storage;

    public class VirgilKeyStorageTests
    {
        [TearDown]
        public void Teardown()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var keysPath = Path.Combine(appData, "VirgilSecurity");

            Directory.Delete(keysPath, true);
        }

        [Test]
        public void Store_GivenAliasAndKeyPairEntry_ShouldCreateDirectoryIfItDoestExists()
        {
            var keyStorage = new VirgilPrivateKeyStorage();
            keyStorage.Store("ALICE_KEY", new PrivateKeyEntry());

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var keysPath = Path.Combine(appData, "VirgilSecurity", "Keys");

            Directory.Exists(keysPath).Should().BeTrue();
        }

        [Test]
        public void Store_GivenAlreadyExistsAliasAndKeyPairEntry_ShouldThrowException()
        {
            Assert.Throws<KeyPairAlreadyExistsException>(() =>
            {
                var keyStorage = new VirgilPrivateKeyStorage();
                keyStorage.Store("ALICE_KEY", new PrivateKeyEntry());
                keyStorage.Store("ALICE_KEY", new PrivateKeyEntry());
            });
        }

        [Test]
        public void Exists_GivenExistingAlias_ShouldReturnTrue()
        {
            const string aliceKey = "Alice_Key";

            var keyStorage = new VirgilPrivateKeyStorage();
            keyStorage.Store(aliceKey, new PrivateKeyEntry());

            string name;

            using (var hasher = SHA1.Create())
            {
                var data = Encoding.UTF8.GetBytes(aliceKey.ToUpper());
                name = BitConverter.ToString(hasher.ComputeHash(data)).Replace("-", "").ToLower();
            }
            
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var keyPath = Path.Combine(appData, "VirgilSecurity", "Keys", name);
            
            File.Exists(keyPath).Should().BeTrue();
        }

        [Test]
        public void Load_GivenAlias_ShouldLoadPreviouslyStoredKeyPair()
        {
            const string aliceKey = "Alice_Key";

            var keyPair = Crypto.VirgilKeyPair.Generate();
            var keyEntry = new PrivateKeyEntry
            {
                PublicKey = keyPair.PublicKey(),
                PrivateKey = keyPair.PrivateKey(),
                MetaData = new Dictionary<string, string>
                {
                    { "card_id", "FA028901-E01A-469A-909B-37BCD005A0AB" }
                }
            };

            var keyStorage = new VirgilPrivateKeyStorage();
            keyStorage.Store(aliceKey, keyEntry);

            var loadedKeyPair = keyStorage.Load(aliceKey);
            
            loadedKeyPair.ShouldBeEquivalentTo(keyEntry);
        }
    }
}