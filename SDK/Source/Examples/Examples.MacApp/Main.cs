using AppKit;
using Virgil.Crypto;
using Virgil.SDK;
namespace Examples.MacApp
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            var crypto = new VirgilCrypto();
            var keypair = crypto.GenerateKeys();
            var keyStorage = new PrivateKeyStorage(new VirgilPrivateKeyExporter(crypto));
            var exist = keyStorage.Exists("alice");
            keyStorage.Store(keypair.PrivateKey, "alice");
            var key = keyStorage.Load("alice");
            NSApplication.Init();
            NSApplication.Main(args);
        }
    }
}
