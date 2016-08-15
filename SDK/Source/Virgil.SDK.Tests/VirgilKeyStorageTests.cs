namespace Virgil.SDK.Tests
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Policy;
    using System.Text;
    using FluentAssertions;
    using NUnit.Framework;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Exceptions;

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
            keyStorage.Store("ALICE_KEY", new KeyPairEntry());

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
                keyStorage.Store("ALICE_KEY", new KeyPairEntry());
                keyStorage.Store("ALICE_KEY", new KeyPairEntry());
            });
        }

        [Test]
        public void Exists_GivenExistingAlias_ShouldReturnTrue()
        {
            const string aliceKey = "Alice_Key";

            var keyStorage = new VirgilKeyStorage();
            keyStorage.Store(aliceKey, new KeyPairEntry());

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
    }
}