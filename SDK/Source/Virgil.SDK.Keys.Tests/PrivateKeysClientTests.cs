namespace Virgil.SDK.Keys.Tests
{
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Virgil.SDK.Infrastructure;

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