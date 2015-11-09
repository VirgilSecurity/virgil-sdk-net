namespace Virgil.Examples.SDK
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.Crypto;
    using Virgil.Examples.Common;
    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Model;
    using Virgil.SDK.PrivateKeys.Http;

    public class AddPrivateKeyToExistingContainer : AsyncExample
    {
        public async override Task Execute()
        {
            Console.Write("Enter your email ID: ");
            var userId = Console.ReadLine();

            Console.Write("Enter container password: ");
            var password = Console.ReadLine();

            // generate new public/private key pair.
            
            byte[] publicKey;
            byte[] privateKey;

            using (var keyPair = new VirgilKeyPair())
            {
                publicKey = keyPair.PublicKey();
                privateKey = keyPair.PrivateKey();
            }

            var keysService = new KeysClient(Constants.AppToken);

            // register new public key on Keys Service using your Email.

            var createdPublicKey = await keysService.PublicKeys.Create(publicKey, privateKey,
                new UserData
                {
                    Class = UserDataClass.UserId,
                    Type = UserDataType.EmailId,
                    Value = userId
                });

            var createdUserData = createdPublicKey.UserData.First();

            Console.Write("Enter confirmation code: ");
            var confirmationCode = Console.ReadLine();

            // confirm email address using confirmation code.

            await keysService.UserData.Confirm(createdUserData.UserDataId, 
                confirmationCode, createdPublicKey.PublicKeyId, privateKey);
            
            var keyringClient = new Virgil.SDK.PrivateKeys.KeyringClient(Constants.AppToken);
            keyringClient.Connection.SetCredentials(new Credentials(userId, password));

            // add private key to your container. 

            await keyringClient.PrivateKeys.Add(createdPublicKey.PublicKeyId, privateKey);

            Console.Write("Private Key has been successfully added to your Container.");
        }
    }
}