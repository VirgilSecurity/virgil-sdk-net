namespace Virgil.SDK.Keys.Tests
{
    using NSubstitute;
    using NUnit.Framework;
    
    using Virgil.SDK.Cryptography;

    public class VirgilKeyTests
    {
        [Test]
        public void Create_KeyName_ShouldUseDefaultKeyStorageProvider()
        {
            var key = VirgilKey.Create("alice");
        }
    }
}