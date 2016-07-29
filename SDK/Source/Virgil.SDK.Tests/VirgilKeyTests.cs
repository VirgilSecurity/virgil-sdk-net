namespace Virgil.SDK.Keys.Tests
{
    using NUnit.Framework;

    public class VirgilKeyTests
    {
        [Test]
        public void Create_KeyName_ShouldUseDefaultKeyStorageProvider()
        {
            var key = VirgilKey.Create("alice");
        }
    }
}