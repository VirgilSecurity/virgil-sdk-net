namespace Virgil.SDK.Keys.Tests
{
    using NUnit.Framework;

    public class VirgilKeyTests
    {
        [Test]
        public void Create_KeyName_ShouldUseDefaultKeyStorageProvider()
        {
            VirgilConfig.Initialize("<ACCESS_TOKEN>");

            var key = VirgilKey.Create("alice");
        }
    }
}