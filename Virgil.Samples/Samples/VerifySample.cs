using System;
using System.IO;

namespace Virgil.Samples
{
    class VerifySample
    {
        public static void Run()
        {
            Console.WriteLine("Prepare input file: test.txt...");

            using (var input = File.OpenRead("test.txt"))
            {
                Console.WriteLine("Read virgil sign...");

                using (var signStream = File.OpenRead("test.txt.sign"))
                {
                    var signBytes = new byte[signStream.Length];
                    signStream.Read(signBytes, 0, signBytes.Length);

                    var virgilSign = new VirgilSign();
                    virgilSign.FromAsn1(signBytes);

                    Console.WriteLine("Get signer (" + Program.SignerId + ") information from the Virgil PKI service...");

                    VirgilCertificate virgilPublicKey = Program.GetPkiPublicKey(Program.UserIdType, Program.SignerId);

                    Console.WriteLine("Initialize verifier...");

                    var signer = new VirgilStreamSigner();

                    Console.WriteLine("Verify data...");
                    var dataSource = new StreamSource(input);
                    bool verified = signer.Verify(dataSource, virgilSign, virgilPublicKey.PublicKey());

                    Console.WriteLine("Data is " + (verified ? "" : "not ") + "verified!");
                }
            }
        }
    }
}