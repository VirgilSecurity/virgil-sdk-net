using System;
using System.Text;
using Foundation;
using Security;
using Virgil.Crypto;
using Virgil.SDK;

namespace Examples.NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var queryRec = new SecRecord(SecKind.GenericPassword)
            {
                Service = "KEYCHAIN_SERVICE",
                Label = "KEYCHAIN_SERVICE",
                Account = "KEYCHAIN_ACCOUNT"
            };
            var secRecord = new SecRecord(SecKind.GenericPassword);
            secRecord.Account = "aaaa";
            secRecord.Service = "bbbb";
            secRecord.Label = "aaaa";
            secRecord.ValueData = NSData.FromArray(Encoding.UTF8.GetBytes("hi"));
            var result = SecKeyChain.Add(secRecord);

            var crypto = new VirgilCrypto();
            var keypair = crypto.GenerateKeys();
            var keyStorage = new PrivateKeyStorage(new VirgilPrivateKeyExporter(crypto));
            var exist = keyStorage.Exists("alice");
            keyStorage.Store(keypair.PrivateKey, "alice");
            var key = keyStorage.Load("alice");
            Console.WriteLine("Hello World!");



        }
    }
}
