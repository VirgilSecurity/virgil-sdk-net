using AppKit;
using Foundation;
using Security;

namespace Virgil.SDK.Tests.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
            var tests = new SecureStorageTests();
            Faker faker = new Faker();
            SecureStorage.StorageIdentity = "Virgil.SecureStorage.Test";
            var storage = new SecureStorage();
            var data = faker.Random.Bytes(32);
            var key = faker.Person.UserName;

            storage.Save(key, data);
            var storedData = storage.Load(key);


            string st = "";
            var s = new SecRecord(SecKind.GenericPassword)
            {
                Label = "Item Label1",
                Description = "Item description",
                Account = "Account1",
                Service = "ServiceTest",
                Comment = "Your comment here"
            };
            var c = s.AccessGroup;
            s.ValueData = NSData.FromString("my-secret-password");
            var err = SecKeyChain.Add(s);

            if (err != SecStatusCode.Success && err != SecStatusCode.DuplicateItem)
            {
                st = "Error adding record: {0}";
            }
            else
            {
                st = "you are done!!!";
            };
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
