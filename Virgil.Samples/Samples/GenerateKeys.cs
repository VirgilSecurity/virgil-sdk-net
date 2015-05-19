using System;
using System.IO;
using System.Text;

namespace Virgil.Samples
{
    class GenerateKeys
    {
        public static void Run()
        {
            Console.WriteLine("Generate keys with with password: 'password'");
            var virgilKeyPair = new VirgilKeyPair(Encoding.UTF8.GetBytes("password"));

            Console.WriteLine("Store public key: public.key ...");
            using (var fileStream = File.Create("public.key"))
            {
                byte[] publicKey = virgilKeyPair.PublicKey();
                fileStream.Write(publicKey, 0, publicKey.Length);
            }

            Console.WriteLine("Store private key: private.key ...");
            using (var fileStream = File.Create("private.key"))
            {
                byte[] privateKey = virgilKeyPair.PrivateKey();
                fileStream.Write(privateKey, 0, privateKey.Length);
            }
        }
    }
}