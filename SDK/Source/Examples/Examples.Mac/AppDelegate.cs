using System;
using AppKit;
using Bogus;
using Foundation;
using Plugin.SecureStorage;
using Security;
using Virgil.Crypto;
using Virgil.SDK;

namespace Examples.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
            Console.WriteLine("Hello World!");
           
            Faker faker = new Faker();
            var alias = faker.Person.UserName;

            // try to save in raw KeyChain
            string status = "";
            var s = new SecRecord(SecKind.GenericPassword)
            {
                Label = alias,
                Account = alias,
                Service = "ServiceTest",
            };
            var c = s.AccessGroup;
            s.ValueData = NSData.FromString("my-secret-password");
            var err = SecKeyChain.Add(s);

            if (err != SecStatusCode.Success)
            {
                status = "Error adding record";
            }
            else
            {
                status = "Success";
            };

            System.Console.WriteLine("KeyChain status= {0}", status);

            //try to use SDK PrivateKeyStorage
            alias = faker.Person.UserName;

            var crypto = new VirgilCrypto();
            var keypair = crypto.GenerateKeys();
            var keyStorage = new PrivateKeyStorage(new VirgilPrivateKeyExporter(crypto));
            keyStorage.Store(keypair.PrivateKey, alias);
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
