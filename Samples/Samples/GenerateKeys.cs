using System;
using System.IO;
using System.Text;

namespace Virgil.Samples
{
    class GenerateKeys
    {
        public static void Run()
        {
            byte[] publicKeyPassword = Encoding.UTF8.GetBytes("password");
            var virgilKeyPair = new VirgilKeyPair(publicKeyPassword);
            byte[] publicKeyBytes = virgilKeyPair.PublicKey();
            byte[] privateKeyBytes = virgilKeyPair.PrivateKey();
        }
    }
}