namespace Virgil.SDK.Keys.Tests.Cards
{
    using Identities;
    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.Cards;

    public class CardsClientTests
    {
        [Test]
        public async void Test1()
        {
            var config = ServiceHubConfig.UseAccessToken("");
            var serviceHub = ServiceHub.Create("<ACCESS_TOKEN>");
            var keyPair = VirgilKeyPair.Generate();
            
            var ticket = new VirgilCardTicket("denis", "user_name", keyPair.PublicKey());
            var exportedTicket = ticket.Export();

            var ownerSign = CryptoHelper.Sign(exportedTicket, keyPair.PrivateKey());
            var appSign = CryptoHelper.Sign(exportedTicket, keyPair.PrivateKey());
            
            var newCard = await serviceHub.Cards.PublishAsPrivateAsync(ticket);

            var globalCards = await serviceHub.Cards
                .SearchGlobalAsync("demo@virgilsecurity.com", IdentityType.Email);

            var privateCards = await serviceHub.Cards
                .SearchPrivateAsync("denis", "user_name", true);
            
            
            //var newVirgilKey = await serviceHub.Cards.
        }
    }
}