using System;
using System.IO;

namespace Virgil.Samples
{
    class RegisterUser
    {
        public static void Run()
        {
            Console.WriteLine("Prepare input file: public.key...");
            using (var inFile = File.OpenRead("public.key"))
            {
                Console.WriteLine("Prepare output file: virgil_public.key...");
                using (var outFile = File.Create("virgil_public.key"))
                {
                    var publicKey = new byte[inFile.Length];
                    inFile.Read(publicKey, 0, (int) inFile.Length);

                    Console.WriteLine("Create user ({0}) account on the Virgil PKI service...", Program.UserId);
                    VirgilCertificate virgilPublicKey = Program.CreateUser(publicKey, Program.UserIdType, Program.UserId);

                    Console.WriteLine("Store virgil public key to the output file...");

                    byte[] virgilPublickKeyBytes = virgilPublicKey.ToAsn1();
                    outFile.Write(virgilPublickKeyBytes, 0, virgilPublickKeyBytes.Length);
                }
            }
        }
    }
}