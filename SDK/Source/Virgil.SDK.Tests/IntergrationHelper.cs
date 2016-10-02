namespace Virgil.SDK.Tests
{
    using System.Configuration;
    using System.IO;
    using System.Threading.Tasks;
    using Common;
    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;

    public class IntergrationHelper
    {
        public static VirgilClient GetVirgilClient()
        {
            var parameters = new VirgilClientParams(AppAccessToken);
            
            parameters.SetCardsServiceAddress(ConfigurationManager.AppSettings["virgil:CardsServicesAddress"]);
            parameters.SetReadCardsServiceAddress(ConfigurationManager.AppSettings["virgil:CardsReadServicesAddress"]);
            parameters.SetIdentityServiceAddress(ConfigurationManager.AppSettings["virgil:IdentityServiceAddress"]);

            var client = new VirgilClient(parameters);

            return client;
        }

        public static string AppID => ConfigurationManager.AppSettings["virgil:AppID"];
        public static byte[] AppKey => File.ReadAllBytes(ConfigurationManager.AppSettings["virgil:AppKeyPath"]);
        public static string AppKeyPassword = ConfigurationManager.AppSettings["virgil:AppKeyPassword"];
        public static string AppAccessToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];

        public static async Task RevokeCard(string cardId)
        {
            var client = GetVirgilClient();
            var crypto = new VirgilCrypto();
            var requestSigner = new RequestSigner(crypto);

            var appKey = crypto.ImportPrivateKey(AppKey, AppKeyPassword);

            var revokeRequest = new RevokeCardRequest(cardId, RevocationReason.Unspecified);
            requestSigner.AuthoritySign(revokeRequest, AppID, appKey);

            await client.RevokeCardAsync(revokeRequest);
        }
    }
}