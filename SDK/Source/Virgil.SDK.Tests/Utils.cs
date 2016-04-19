namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Crypto;
    using SDK.Domain;
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
            email = email ?? Mailinator.GetRandomEmailName();
            var request = await Identity.Verify(email);

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(email, true);
            return await request.Confirm(confirmationCode, new ConfirmOptions(ttl, ctl));
        }
    }
}