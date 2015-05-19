using System;
using System.IO;

namespace Virgil.Samples
{
    class GetPublicKey
    {
        public static void Run()
        {
            Console.WriteLine("Get user ({0}) information from the Virgil PKI service...", Program.UserId);
            var virgilPublicKey = Program.GetPkiPublicKey(Program.UserIdType, Program.UserId);

            Console.WriteLine("Prepare output file: virgil_public.key...");
            using (var outFile = File.Create("virgil_public.key"))
            {
                Console.WriteLine("Store virgil public key to the output file...");

                byte[] virgilPublickKeyBytes = virgilPublicKey.ToAsn1();
                outFile.Write(virgilPublickKeyBytes, 0, virgilPublickKeyBytes.Length);
            }
        }
    }
}