using System;
using System.IO;

namespace Virgil.Samples
{
    class EncryptSample
    {
        public static void Run()
        {
            Console.WriteLine("Prepare input file: test.txt...");

            using (var input = File.OpenRead("test.txt"))
            {
                Console.WriteLine("Prepare output file: test.txt.enc...");

                using (var output = File.Create("test.txt.enc"))
                {
                    Console.WriteLine("Initialize cipher...");
                    var virgilStreamCipher = new VirgilStreamCipher();

                    Console.WriteLine("Get recipient (" + Program.UserId + ") information from the Virgil PKI service...");
                    var virgilPublicKey = Program.GetPkiPublicKey(Program.UserIdType, Program.UserId);

                    Console.WriteLine("Add recipient...");
                    virgilStreamCipher.AddKeyRecipient(virgilPublicKey.Id().CertificateId(),
                        virgilPublicKey.PublicKey());

                    Console.WriteLine("Encrypt and store results...");

                    var source = new StreamSource(input);
                    var sink = new StreamSink(output);

                    virgilStreamCipher.Encrypt(source, sink, true);

                    Console.WriteLine("Encrypted data is successfully stored in the output file...");
                }
            }
        }
    }
}
