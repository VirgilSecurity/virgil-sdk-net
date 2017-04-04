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
            var clientParams = new VirgilClientParams(ConfigurationManager.AppSettings["virgil:AppAccessToken"]);
            clientParams.SetCardsServiceAddress(ConfigurationManager.AppSettings["virgil:CardsServicesAddress"]);
            clientParams.SetIdentityServiceAddress(ConfigurationManager.AppSettings["virgil:IdentityServiceAddress"]);
            clientParams.SetRAServiceAddress(ConfigurationManager.AppSettings["virgil:RAServicesAddress"]);
            clientParams.SetReadCardsServiceAddress(ConfigurationManager.AppSettings["virgil:CardsReadServicesAddress"]);

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
                ClientParams = clientParams,
                UseBuiltInVerifiers = false,
                CardVerifiers = new[] { cardVerifier }
            });

            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("AliceCard", aliceKey);

            await virgil.Cards.PublishAsync(aliceCard);
            aliceCard.Id.Should().NotBeEmpty();
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
