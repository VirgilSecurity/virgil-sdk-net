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
        public static string AppKeyPath => ConfigurationManager.AppSettings["virgil:AppKeyPath"];
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

        public static string RandomText =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eget ipsum augue. " +
                "Nulla facilisi. Praesent eu laoreet felis. Vivamus scelerisque justo magna, variu" +
                "s pharetra augue ultricies non. Vestibulum convallis in urna nec vehicula. Maecen" +
                "as mattis suscipit cursus. In iaculis dui ut quam molestie, vel facilisis lacus s" +
                "celerisque. Morbi at fermentum felis. Vivamus tincidunt ultricies feugiat. In hac" +
                " habitasse platea dictumst. Donec metus diam, luctus et nisi ac, ornare tincidunt" +
                " mauris. Fusce posuere lacus non ligula cursus convallis. Proin facilisis ut diam" +
                " in cursus. Vivamus in velit a nisi rhoncus vestibulum."                           +
                "Curabitur blandit aliquam ex, et pellentesque elit pharetra at. Donec elementum r" +
                "honcus finibus. Duis risus est, porttitor vitae ante vel, sagittis tincidunt veli" +
                "In enim ipsum, sagittis at vehicula ut, accumsan nec sapien. Vestibulum conse"     +
                "ctetur eros a nulla tristique, in lobortis nibh consectetur. Phasellus tincidunt " +
                "luctus bibendum. Praesent orci turpis, luctus eget interdum ultricies, vestibulum" +
                " a eros. Phasellus sagittis vulputate justo ut condimentum.";
    }
}