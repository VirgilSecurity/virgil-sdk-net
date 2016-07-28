namespace Virgil.SDK.Keys.Tests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class VirgilCardTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async void Find_IdentityName_ShouldReturnListOfCards()
        {
            var bobCards = await VirgilCard.Find("Bob");
        }

        [Test]
        public async void Find_NullOrEmptyIdentity_ShouldThrowException()
        {
            var bobCards = await VirgilCard.Find("");
        }
    }
}   