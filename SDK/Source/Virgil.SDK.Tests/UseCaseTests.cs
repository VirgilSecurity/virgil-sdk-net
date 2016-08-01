namespace Virgil.SDK.Keys.Tests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class UseCaseTests
    {
        [Test]
        public void InitializeHighLevel_UsingAccessToken()
        {
            VirgilConfig.Initialize("<ACCESS_TOKEN>");
        }

        [Test]
        public async void PublishVirgilCard_SignedWithApplicationKey()
        {
            // -------- ALICE'S SIDE

            var aliceKey = VirgilKey.Create("ALICE_KEY");
            var request = aliceKey.BuildPublishRequest("alice", "name");

            // -------- APPLICATION'S SIDE

            var appKey = VirgilKey.FromFile("application.virgilkey", "pwd");
            appKey.ApproveRequest(request);

            // -------- ALICE'S/APPLICATION'S SIDE 

            var card = await VirgilCard.PublishAsync(request);
        }

        [Test]
        public async void EncryptMessageForBob_UsingBobCard()
        {
            // -------- ALICE'S SIDE

            const string message = "Hey Bob, are you fu**ing kidding me?";
            var ciphertext = await VirgilCard.FindAsync("Bob").ThenEncrypt(message);

            // -------- BOB'S SIDE

            var bobKey = VirgilKey.Load("BOB_KEY");
            var receivedMessage = bobKey.Decrypt(ciphertext);

            // -------- ASSERTIONS

            receivedMessage.ShouldBeEquivalentTo(message);
        }

        [Test]
        public async void SignAndEncryptMessageForBob_UsingBobCardAndAliceKey()
        {
            // -------- ALICE'S SIDE

            const string message = "Hey Bob, are you fu**ing kidding me?";

            var aliceKey = VirgilKey.Load("ALICE_KEY");
            var ciphertext = await VirgilCard.FindAsync("Bob").ThenSignAndEncrypt(message, aliceKey);

            // -------- BOB'S SIDE

            var bobKey = VirgilKey.Load("BOB_KEY");
            var receivedMessage = bobKey.DecryptAndVerify(ciphertext);

            // -------- ASSERTIONS

            receivedMessage.ShouldBeEquivalentTo(message);
        }
    }
}