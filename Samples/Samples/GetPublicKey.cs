using System;
using System.IO;
using System.Linq;
using Virgil.PKI;
using Virgil.PKI.Models;

namespace Virgil.Samples
{
    class GetPublicKey
    {
        public static void Run()
        {
            var pkiClient = new PkiClient("{Token}");
            var publicKeys = pkiClient.PublicKeys
                .SearchKey("some@mail.com", UserDataType.Email).Result.ToList();
            var publicKey = publicKeys.First();
        }
    }
}