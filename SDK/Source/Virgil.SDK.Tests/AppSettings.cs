using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgil.SDK.Tests
{
    public class AppSettings
    {
        public static string AppId = ConfigurationManager.AppSettings["virgil:AppID"];
        public static string AccounId = ConfigurationManager.AppSettings["virgil:AccountID"];
        public static string AppPrivateKeyPassword = ConfigurationManager.AppSettings["virgil:AppKeyPassword"];
        public static string ApiPublicKeyId = ConfigurationManager.AppSettings["virgil:AccessPublicKeyId"];
        public static string ApiPrivateKeyBase64 = ConfigurationManager.AppSettings["virgil:AccessPrivateKeyBase64"];
        public static string ServiceCardId = ConfigurationManager.AppSettings["virgil:ServiceCardId"];
        public static string ServicePublicKeyPemBase64 = ConfigurationManager.AppSettings["virgil:ServicePublicKeyPemBase64"];
        public static string ServicePublicKeyDerBase64 = ConfigurationManager.AppSettings["virgil:ServicePublicKeyDerBase64"];
        public static string CryptoCompatibilityDataPath = ConfigurationManager.AppSettings["test:CryptoCompatibilityDataPath"];
        public static string OutputTestDataPath = ConfigurationManager.AppSettings["test:OutputDataPath"];
        public static string CardsServiceAddress = ConfigurationManager.AppSettings["virgil:CardsServicesAddressV5"];

        public static string PrivateKeySTC31_1 = ConfigurationManager.AppSettings["test:PrivateKeySTC31_1"];
        public static string PrivateKeySTC31_2 = ConfigurationManager.AppSettings["test:PrivateKeySTC31_2"];
        public static string PublicKeySTC32 = ConfigurationManager.AppSettings["test:PublicKeySTC32"];
        public static string OldKeyStoragePath = ConfigurationManager.AppSettings["test:OldKeyStoragePath"];
        public static string OldKeyAliase = ConfigurationManager.AppSettings["test:OldKeyAliase"];

        public static string ImportedAccessPublicKeyId = ConfigurationManager.AppSettings["test:ImportedAccessPublicKeyId"];
        public static string ImportedAccessPublicKey = ConfigurationManager.AppSettings["test:ImportedAccessPublicKey"];

        public static string PredefinedPrivateKeyBase64 = ConfigurationManager.AppSettings["test:PredefinedPrivateKeyBase64"];

        public static string ImportedJwt = ConfigurationManager.AppSettings["test:ImportedJwt"];
    }
}
