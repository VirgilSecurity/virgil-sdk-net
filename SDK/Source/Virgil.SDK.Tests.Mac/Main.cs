using AppKit;

namespace Virgil.SDK.Tests.Mac
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            NSApplication.Init();
            // var AppId = ConfigurationManager.AppSettings["virgil:AppID"];
            // var cryptoTestData = File.ReadAllText(assembly.GetFile("crypto_compatibility_data.json"));
            // AppSettings.CryptoCompatibilityData = cryptoTestData;
            //var cryptoTestData = assembly.GetFile("crypto_compatibility_data.json").ToString();
            NSApplication.Main(args);
        }
    }
}
