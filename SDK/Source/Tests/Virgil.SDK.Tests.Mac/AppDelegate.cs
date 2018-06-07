using System;
using System.Linq;
using AppKit;
using Bogus;
using Foundation;

namespace Virgil.SDK.Tests.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
            var faker = new Faker();
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Tests.Mac";
            var key = faker.Person.UserName;
            var data = faker.Random.Bytes(32);

            var storage = new SecureStorage();

            storage.Save(key, data);
            var storedData = storage.Load(key);
            if (!storedData.SequenceEqual(data)){
                throw new Exception("loaded value is different from original.");
            }
            storage.Delete(key);
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
