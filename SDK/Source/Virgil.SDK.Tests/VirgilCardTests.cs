namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public class VirgilCardTests
    {
        [SetUp]
        public async void Setup()
        {
            var card = await VirgilCard.Get(Guid.NewGuid());
        }

        [Test]
        public async void TicketRequest_IdentityAndKey_ShouldCreateSignedTicketRequest()
        {
            var keyData = VirgilBuffer.FromBytes(File.ReadAllBytes("C:/Keys/application.virgilkey"));
            var key = VirgilKey.Import(keyData);
        }

        [Test]
        public async void Find_IdentityName_ShouldReturnListOfCards()
        {
            var bobCards = await VirgilCard.FindAsync("Bob");
        }

        [Test]
        public async void Find_NullOrEmptyIdentity_ShouldThrowException()
        {
            var bobCards = await VirgilCard.FindAsync("");
        }
    }
}   