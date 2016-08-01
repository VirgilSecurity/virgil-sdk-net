namespace Virgil.SDK.Keys.Tests
{
    using NUnit.Framework;

    using Virgil.SDK.Exceptions;

    public class InitializationTests
    {
        [Test]
        public void UsingVirgilServices_WithoutInitialization_ShouldThrowException()
        {
            Assert.Throws<VirgilServiceNotInitializedException>(() =>
            {
                var bobCards = VirgilCard.FindAsync("Alice").Result;
            });
        }
    }
}