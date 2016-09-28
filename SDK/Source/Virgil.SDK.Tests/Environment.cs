namespace Virgil.SDK.Tests
{
    using System.Configuration;
    using System.IO;

    using Virgil.SDK.Client;

    public class Environment
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
    }
}