using AppKit;
using Bogus;
using Foundation;
using Virgil.Crypto;
using Virgil.SDK;

namespace Examples.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
            var faker = new Faker();
            //try to use SDK PrivateKeyStorage
            var alias = faker.Person.UserName;

            var crypto = new VirgilCrypto();
            var keypair = crypto.GenerateKeys();
            var keyStorage = new PrivateKeyStorage(new VirgilPrivateKeyExporter(crypto));
            keyStorage.Store(keypair.PrivateKey, alias);
            keyStorage.Delete(alias);
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
