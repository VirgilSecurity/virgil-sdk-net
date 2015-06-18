using System;
using System.IO;
using System.Linq;
using Virgil.PKI;
using Virgil.PKI.Models;

namespace Virgil.Samples
{
    class EncryptSample
    {
        public static void Run()
        {

            var virgilStreamCipher = new VirgilCipher();

            var pkiClient = new PkiClient("{Token}");

            var publicKey = pkiClient.PublicKeys.Get(Guid.Parse("public key id")).Result;

            virgilStreamCipher.AddKeyRecipient(publicKey.PublicKeyId, virgilPublicKey.PublicKey());
                    
            virgilStreamCipher.Encrypt(source, sink, true);

            var virgilCipher = new VirgilCipher();
            virgilCipher.Encrypt()
                
            }
        }
    }
}
