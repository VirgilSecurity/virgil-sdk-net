namespace Virgil.SDK.Keys.Tests
{
    using System.Threading.Tasks;
    using Infrastructure;
    using NUnit.Framework;

    public class PrivateKeysClientTests
    {
        [SetUp]
        public void Init()
        {
            ServiceLocator.SetupForTests();
        }

        [Test]
        public async Task ShouldBeAbleToPutPrivateKeyByItsId()
        {
            
        }
    }
}