namespace Virgil.SDK.Tests
{
    using System.Configuration;
    using NUnit.Framework;
    using FluentAssertions;
    using System.Threading.Tasks;
    using Client;
    using System;
    using Client.Requests;

    class CardsManagerTests
    {
        [Test]
        public async Task PublishCardWithAppCredentials_PredefinedCard_ShouldPublishCard()
        {
            var virgil = new VirgilApi(IntegrationHelper.VirgilApiContext());

            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("alice-" + Guid.NewGuid(), aliceKey);

            await virgil.Cards.PublishAsync(aliceCard);
            aliceCard.Id.Should().NotBeEmpty();

            Assert.IsTrue(aliceCard.IsPairFor(aliceKey));
            await virgil.Cards.RevokeAsync(aliceCard);
        }

        [Test]
        public void PublishCardWithEmptyAppCredentials_PredefinedCard_ShouldOccurAppCredentialsException()
        {
            var AppAccessToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];
            var virgil = new VirgilApi(AppAccessToken);
            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("AliceCard", aliceKey);
            Assert.ThrowsAsync<Exceptions.AppCredentialsException>(() => virgil.Cards.PublishAsync(aliceCard));
        }

        [Test]
        public void RevokeWithEmptyAppCredentials_PredefinedCard_ShouldOccurAppCredentialsException()
        {
            var AppAccessToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];
            var virgil = new VirgilApi(AppAccessToken);
            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("AliceCard", aliceKey);
            Assert.ThrowsAsync<Exceptions.AppCredentialsException>(() => virgil.Cards.RevokeAsync(aliceCard));
        }
    }
}
