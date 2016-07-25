namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using SDK.Utils;
    using Virgil.Crypto;
    using Virgil.SDK.Clients;
    using Virgil.SDK.Identities;

    public static class Utils
    {
        public static async Task<Batch> TestCreateVirgilCard(this ICardsServiceClient client, bool isAuthorized = false)
        {
            var virgilKeyPair = new VirgilKeyPair();

            var identityInfo = new IdentityInfo {
                Value = Guid.NewGuid() + "virgil_demo_user",
                Type = "username"
            };

            if (isAuthorized)
            {
                identityInfo.ValidationToken = ValidationTokenGenerator.Generate(identityInfo.Value, identityInfo.Type,
                    EnvironmentVariables.AppPrivateKey, EnvironmentVariables.AppPrivateKeyPassword);
            }

            var virgilCard = await client.Create(
                identityInfo,
                virgilKeyPair.PublicKey(),
                virgilKeyPair.PrivateKey(),
                customData: new Dictionary<string, string>
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
            public CardModel VirgilCard;
            public VirgilKeyPair VirgilKeyPair;
        }

        public static async Task<IdentityInfo> GetConfirmedToken(string email = null, int ttl = 300, int ctl = 1)
        {
            var serviceHub = ServiceHubHelper.Create();

            email = email ?? Mailinator.GetRandomEmailName();
            var identityBuilder = await serviceHub.Identity.VerifyEmail(email);

            var confirmationCode = await Mailinator.GetConfirmationCodeFromLatestEmail(email, true);

            var identity = await identityBuilder.Confirm(confirmationCode, ttl, ctl);
            return identity;
        }
    }
}