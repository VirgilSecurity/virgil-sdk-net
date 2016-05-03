namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Configuration;

    public class EnvironmentVariables
    {
        public static string IdentityServiceAddress = ConfigurationManager.AppSettings["virgil:IdentityServiceAddress"];
        public static string PrivateServicesAddress = ConfigurationManager.AppSettings["virgil:PrivateServicesAddress"];
        public static string PublicServicesAddress = ConfigurationManager.AppSettings["virgil:PublicServicesAddress"];
        public static string AppBundleId = ConfigurationManager.AppSettings["virgil:AppBundleID"];
        public static string ApplicationAccessToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];
        public static byte[] ApplicationPublicKey = Convert.FromBase64String(ConfigurationManager.AppSettings["virgil:AppPublicKey"]);
        public static byte[] AppPrivateKey = Convert.FromBase64String(ConfigurationManager.AppSettings["virgil:AppPrivateKey"]);
        public static string AppPrivateKeyPassword = ConfigurationManager.AppSettings["virgil:AppPrivateKeyPassword"];
        public static string MailinatorAuthToken = ConfigurationManager.AppSettings["mailinator:AuthToken"];
        public static string MailinatorAPIUrl = ConfigurationManager.AppSettings["mailinator:APIUrl"];
    }
}