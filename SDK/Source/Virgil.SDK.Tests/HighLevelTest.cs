namespace Virgil.SDK.Tests
{
    using System.Threading.Tasks;
    using NUnit.Framework;

    public class HighLevelTest
    {
        [Test]
        public async Task CreateCard_IdentityAndTypeGiven_ShouldCardBeCreatedOnTheService()
        {
            VirgilConfig.Initialize("AT.8188dafdbdaccd2efcfb6f687988fc85a1d1e9f7c2d9221edfb0d4a6e470df73");
            
            var cards = await VirgilCard.FindAsync("bob");
        }
    }
}