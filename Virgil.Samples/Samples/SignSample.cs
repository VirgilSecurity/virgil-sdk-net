using System;
using System.IO;
using System.Text;

namespace Virgil.Samples
{
    class SignSample
    {
        public static void Run()
        {
            Console.WriteLine("Prepare input file: test.txt...");

            using (var input = File.OpenRead("test.txt"))
            {
                Console.WriteLine("Prepare output file: test.txt.sign...");

                using (var output = File.Create("test.txt.sign"))
                {
                    Console.WriteLine("Read virgil public key...");
                    var publicKeyBytes = File.ReadAllBytes("virgil_public.key");
                    var virgilPublicKey = new VirgilCertificate();
                    virgilPublicKey.FromAsn1(publicKeyBytes);

                    Console.WriteLine("Read private key...");

                    var privateKey = File.ReadAllBytes("private.key");

                    Console.WriteLine("Initialize signer...");

                    var signer = new VirgilStreamSigner();
                        
                    byte[] password = Encoding.UTF8.GetBytes("password");

                    Console.WriteLine("Sign data...");

                    var source = new StreamSource(input);

                    VirgilSign sign = signer.Sign(source, virgilPublicKey.Id().CertificateId(), privateKey, password);

                    Console.WriteLine("Save sign...");
                    var asn1Sign = sign.ToAsn1();
                    output.Write(asn1Sign, 0, asn1Sign.Length);

                    Console.WriteLine("Sign is successfully stored in the output file.");
                }
            }
        }
    }
}