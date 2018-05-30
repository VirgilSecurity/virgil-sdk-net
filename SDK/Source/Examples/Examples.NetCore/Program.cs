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
            var keyStorage = new PrivateKeyStorage(new VirgilPrivateKeyExporter(crypto), "lalala");
            var exist = keyStorage.Exists("alice");
            keyStorage.Store(keypair.PrivateKey, "alice");
            var key = keyStorage.Load("alice");
            Console.WriteLine("Hello World!");



        }
    }
}
