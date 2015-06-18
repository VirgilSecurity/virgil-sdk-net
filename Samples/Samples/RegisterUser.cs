using System.Text;
using Virgil.PKI;
using Virgil.PKI.Models;

namespace Virgil.Samples
{
    class RegisterUser
    {
        public static void Run()
        {
            byte[] publicKeyPassword = Encoding.UTF8.GetBytes("password");
            var virgilKeyPair = new VirgilKeyPair(publicKeyPassword);
            byte[] publicKeyBytes = virgilKeyPair.PublicKey();

            var pkiClient = new PkiClient("{Token}");
            var virgilUserData = new VirgilUserData(UserDataType.Email, "mail@server.com");
            var virgilAccount = pkiClient.Accounts.Register(virgilUserData, publicKeyBytes).Result;

        }
    }
}