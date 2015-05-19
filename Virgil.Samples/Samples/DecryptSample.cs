using System;
using System.IO;
using System.Text;

namespace Virgil.Samples
{
    class DecryptSample
    {
        public static void Run()
        {
            Console.WriteLine("Prepare input file: test.txt.enc...");

            using (var input = File.OpenRead("test.txt.enc"))
            {
                Console.WriteLine("Prepare output file: decrypted_test.txt...");

                using (var output = File.Create("decrypted_test.txt"))
                {
                    Console.WriteLine("Initialize cipher...");
                    var virgilStreamCipher = new VirgilStreamCipher();

                    Console.WriteLine("Read virgil public key...");
                    var publicKeyBytes = File.ReadAllBytes("virgil_public.key");
                    var virgilPublicKey = new VirgilCertificate();
                    virgilPublicKey.FromAsn1(publicKeyBytes);

                    Console.WriteLine("Read private key...");

                    var privateKey = File.ReadAllBytes("private.key");

                    Console.WriteLine("Decrypt...");

                    var source = new StreamSource(input);
                    var sink = new StreamSink(output);
                    byte[] password = Encoding.UTF8.GetBytes("password");

                    virgilStreamCipher.DecryptWithKey(source, sink, virgilPublicKey.Id().CertificateId(),
                        privateKey, password);

                    Console.WriteLine("Decrypted data is successfully stored in the output file...");
                }
            }
        }
    }
}