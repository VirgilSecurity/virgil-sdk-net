namespace Virgil.SDK.Tests
{
    using System.Configuration;
    using NUnit.Framework;
    using FluentAssertions;
    using System.Threading.Tasks;
    using Client;

    class CardsManagerTests
    {
        [Test]
        public async Task PublishCardWithAppCredentials_PredefinedCard_ShouldPublishCard()
        {
            var cardsClientParams = new CardsClientParams(ConfigurationManager.AppSettings["virgil:AppAccessToken"]);
            cardsClientParams.SetCardsServiceAddress(ConfigurationManager.AppSettings["virgil:CardsServicesAddress"]);
            cardsClientParams.SetRAServiceAddress(ConfigurationManager.AppSettings["virgil:RAServicesAddress"]);
            cardsClientParams.SetReadCardsServiceAddress(ConfigurationManager.AppSettings["virgil:CardsReadServicesAddress"]);

            var identityClientParams = new IdentityClientParams();
            identityClientParams.SetIdentityServiceAddress(ConfigurationManager.AppSettings["virgil:IdentityServiceAddress"]);

            // To use staging Verifier instead of default verifier
            var cardVerifier = new CardVerifierInfo
            {
                CardId = ConfigurationManager.AppSettings["virgil:ServiceCardId"],
                PublicKeyData = VirgilBuffer.From(ConfigurationManager.AppSettings["virgil:ServicePublicKeyDerBase64"], 
                StringEncoding.Base64)
            };
            var virgil = new VirgilApi(new VirgilApiContext
            {
                Credentials = new AppCredentials
                {
                    AppId = ConfigurationManager.AppSettings["virgil:AppID"],
                    AppKey = VirgilBuffer.FromFile(ConfigurationManager.AppSettings["virgil:AppKeyPath"]),
                    AppKeyPassword = ConfigurationManager.AppSettings["virgil:AppKeyPassword"]
                },
                CardsClientParams = cardsClientParams,
                UseBuiltInVerifiers = false,
                CardVerifiers = new[] { cardVerifier }
            });

            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("AliceCard", aliceKey);

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
