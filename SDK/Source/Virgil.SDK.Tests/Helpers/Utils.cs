namespace Virgil.SDK.Keys.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.Crypto;

    using Virgil.SDK.Clients;
    using Virgil.SDK.TransferObject;

    public static class Utils
    {
        public static async Task<Batch> TestCreateVirgilCard(this ICardsClient client)
        {
            var virgilKeyPair = new VirgilKeyPair();

            var virgilCard = await client.Create(
                Mailinator.GetRandomEmailName(),
                IdentityType.Email,
                virgilKeyPair.PublicKey(),
                virgilKeyPair.PrivateKey(),
                customData: new Dictionary<string, string>()
                {
                    ["hello"] = "world"
                });

            return new Batch
            {
                VirgilCard = virgilCard,
                VirgilKeyPair = virgilKeyPair
            };
        }

        public class Batch
        {
            public VirgilCardDto VirgilCard;
            public VirgilKeyPair VirgilKeyPair;
        }

        public static async Task<IdentityTokenDto> GetConfirmedToken(string email = null, int ttl = 300, int ctl = 1)
        {
            var serviceHub = ServiceHubHelper.Create();

            email = email ?? Mailinator.GetRandomEmailName();
            var request = await serviceHub.Identity.Verify(email, IdentityType.Email);

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(email, true);
            return await serviceHub.Identity.Confirm(request.ActionId, confirmationCode, ttl, ctl);
        }
    }
}