namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Policy;
    using System.Text;
    using FluentAssertions;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using Virgil.SDK;
    using Virgil.SDK.Cryptography;
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
            var keyStorage = new VirgilKeyStorage();
            keyStorage.Store("ALICE_KEY", new KeyEntry());

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var keysPath = Path.Combine(appData, "VirgilSecurity", "Keys");

            Directory.Exists(keysPath).Should().BeTrue();
        }

        [Test]
        public void Store_GivenAlreadyExistsAliasAndKeyPairEntry_ShouldThrowException()
        {
            Assert.Throws<KeyPairAlreadyExistsException>(() =>
            {
                var keyStorage = new VirgilKeyStorage();
                keyStorage.Store("ALICE_KEY", new KeyEntry());
                keyStorage.Store("ALICE_KEY", new KeyEntry());
            });
        }

        [Test]
        public void Exists_GivenExistingAlias_ShouldReturnTrue()
        {
            const string aliceKey = "Alice_Key";

            var keyStorage = new VirgilKeyStorage();
            keyStorage.Store(aliceKey, new KeyEntry());

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
            var keyEntry = new KeyEntry
            {
                PublicKey = keyPair.PublicKey(),
                PrivateKey = keyPair.PrivateKey(),
                MetaData = new Dictionary<string, string>
                {
                    { "card_id", "FA028901-E01A-469A-909B-37BCD005A0AB" }
                }
            };

            var keyStorage = new VirgilKeyStorage();
            keyStorage.Store(aliceKey, keyEntry);

            var loadedKeyPair = keyStorage.Load(aliceKey);
            
            loadedKeyPair.ShouldBeEquivalentTo(keyEntry);
        }
    }
}