namespace Virgil.SDK.Keys.Tests.UseCases
{
    using FluentAssertions;
    using NUnit.Framework;

    public class PublishVirgilCardTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async void PublishVirgilCard_WithoutApplicationApproval_InvisibleInRegularSearch()
        {
            // ---------- ALICE'S SIDE

            var aliceKey = VirgilKey.Create("ALICE_KEY");   
            var request = aliceKey.BuildPublishRequest("alice", "name");

            // publish new Virgil Card to the service.

            await VirgilCard.PublishAsync(request);

            // search for Alice's Virgil Card on the service using reqular search.

            var aliceCards =  await VirgilCard.FindAsync("alice", "name");

            // ---------- ASSERTIONS

            aliceCards.Should().BeEmpty();
        }

        [Test]
        public async void PublishVirgilCard_WithApplicationApproval_VisibleInRegularSearch()
        {
            // ---------- ALICE'S SIDE

            var aliceKey = VirgilKey.Create("ALICE_KEY");
            var request = aliceKey.BuildPublishRequest("alice", "name");

            // ---------- APPLICATION'S SIDE

            var appKey = VirgilKey.FromFile("application.virgilkey", "<PASSWORD>");
            appKey.ApproveRequest(request);

            // publish new Virgil Card to the service.

            await VirgilCard.PublishAsync(request);

            // search for Alice's Virgil Card on the service using reqular search.

            var aliceCards = await VirgilCard.FindAsync("alice", "name");

            // ---------- ASSERTIONS

            aliceCards.Should().HaveCount(1);
        }
    }
}