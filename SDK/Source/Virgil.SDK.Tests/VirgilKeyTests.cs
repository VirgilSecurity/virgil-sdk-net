namespace Virgil.SDK.Keys.Tests
{
    using NSubstitute;
    using NUnit.Framework;

    using Virgil.SDK.Configuration;
    using Virgil.SDK.Cryptography;

    public class VirgilKeyTests
    {
        [Test]
        public void Create_KeyName_ShouldUseDefaultKeyStorageProvider()
        {
            var storageProvider = Substitute.For<IStorageProvider>();
            Manager.SetDefaultKeyStorageProvider(storageProvider);
            
            var key = VirgilKey.Create("alice");
        }
    }
}