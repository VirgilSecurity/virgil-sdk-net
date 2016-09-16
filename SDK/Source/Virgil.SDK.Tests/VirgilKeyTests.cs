namespace Virgil.SDK.Tests
{
    using System.Threading.Tasks;
    using NUnit.Framework;
    
    using Virgil.SDK;

    public class VirgilKeyTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [TearDown]
        public void Teardown()
        {
        }

        [Test]
        public void CreateKey_GivenName_ShouldGenerateAndStoreTheKey()
        {
            var key = VirgilKey.Load("Alice");
        }
    }
}