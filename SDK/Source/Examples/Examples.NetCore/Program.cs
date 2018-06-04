using System;
using System.Text;
using Virgil.Crypto.Foundation;
using Virgil.Crypto;
using Virgil.SDK;

namespace Examples.NetCore
{
    class Program
    {
        static void Main(string[] args)
        {

            var crypto = new VirgilCrypto();
            var keypair = crypto.GenerateKeys();
            var keyStorage = new PrivateKeyStorage(new VirgilPrivateKeyExporter(crypto));
            var aliase = "Alice";
            if (!keyStorage.Exists(aliase))
            {
                Console.WriteLine($"Key with aliase '{aliase}' is missing.\n I'll try to add");
                keyStorage.Store(keypair.PrivateKey, aliase);
                var key = keyStorage.Load(aliase);
                if (key != null)
                {
                    Console.WriteLine($"I have added the key with aliase '{aliase}'.\n I'll try to delete");
                    keyStorage.Delete(aliase);
                    Console.WriteLine($"Now key with aliase '{aliase}' exists? '{keyStorage.Exists(aliase)}'");
                }
            };
        }
    }
}
