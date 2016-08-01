namespace Virgil.SDK.Keys.Tests
{
    using NUnit.Framework;
    
    public class VirgilKeyTests
    {
        [Test]
        public async void Create_KeyName_ShouldUseDefaultKeyStorageProvider()
        {
            VirgilConfig.Initialize("<ACCESS_TOKEN>");
            
            var aliceKey = VirgilKey.Create("alice_key");
            var publishRequest = aliceKey.BuildPublishRequest("alice", "name");

            // ---------------- APPLICATION SIDE
            
            var appKey = VirgilKey.FromFile("myapplication.virgilkey", "pwd");
            appKey.ApproveRequest(publishRequest);

            // ---------------- CLIENT SIDE

            await VirgilCard.Publish(publishRequest);
        }
    }
}