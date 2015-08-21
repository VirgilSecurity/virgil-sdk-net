namespace Virgil.Samples.Examples
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys;
    using Virgil.SDK.PrivateKeys.Model;

    using Virgil.Crypto;
    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Model;

    public class PublishKeysExample
    {
        public static async Task Launch()
        {
            // The following code example creates a new public/private key 
            // pair usign Virgil Crypto library

            byte[] publicKey;
            byte[] privateKey;

            using (var keyPair = new VirgilKeyPair())
            {
                publicKey = keyPair.PublicKey();
                privateKey = keyPair.PrivateKey();
            }

            // This example shows how to upload a public key and register 
            // a new account on Virgil’s Keys Service.

            var keysService = new PkiClient(new SDK.Keys.Http.Connection(Constants.ApplicationToken, 
                new Uri(Constants.KeysServiceUrl)));

            var userData = new UserData
            {
                Class = UserDataClass.UserId,
                Type = UserDataType.EmailId,
                Value = "your.email@server.com"
            };
            
            var vPublicKey = await keysService.PublicKeys.Create(publicKey, publicKey, userData);
            var vUserData = vPublicKey.UserData.First();

            var confirmCode = ""; // Confirmation code you received on your email box.

            await keysService.UserData.Confirm(vUserData.UserDataId, confirmCode, vPublicKey.PublicKeyId, privateKey);

            // Store private key in Virgil Private Keys service.

            var privateKeysClient = new KeyringClient(new SDK.PrivateKeys.Http.Connection(Constants.ApplicationToken, 
                new Uri(Constants.PrivateKeysServiceUrl)));
            
            // This password will be used to authenticate access to the container keys.

            var containerPassword = "12345678";

            // You can choose between few types of container. Easy and Normal
            //   Easy   - service keeps your private keys encrypted with container password, all keys should be sent 
            //            encrypted with container password, before sent to the service.
            //   Normal - responsibility for the security of the private keys at your own risk. 

            var containerType = ContainerType.Easy;
            //var containerType = ContainerType.Normal;

            // Initializes an container for private keys storage. It is important to have 
            // public key published on Virgil Keys service.

            await privateKeysClient.Container.Initialize(containerType, vPublicKey.PublicKeyId, privateKey, containerPassword);
            
            // Authenticate requests to Virgil Private Keys service.

            privateKeysClient.Connection.SetCredentials(new SDK.PrivateKeys.Http.Credentials(vUserData.Value, containerPassword));

            // Add your private key to Virgil Private Keys service.

            if (containerType == ContainerType.Easy)
            {
                // private key will be encrypted with container password, provided on authentication
                await privateKeysClient.PrivateKeys.Add(vPublicKey.PublicKeyId, privateKey);
            }
            else
            {
                // use your own password to encrypt the private key.
                var privateKeyPassword = "47N6JwTGUmFvn4Eh";
                await privateKeysClient.PrivateKeys.Add(vPublicKey.PublicKeyId, privateKey, privateKeyPassword);
            }

            // Get recepient public key by UDID (User Data Indentity)

            var recepientPublicKey = await keysService.PublicKeys.Search("recepient.email@server.hz");

            byte[] recepientId = Encoding.UTF8.GetBytes(recepientPublicKey.PublicKeyId.ToString());
            byte[] encryptedData;

            using (var cipher = new VirgilCipher())
            {                
                byte[] data = Encoding.UTF8.GetBytes("Some data to be encrypted");

                cipher.AddKeyRecipient(recepientId, data);
                encryptedData = cipher.Encrypt(data, true);
            }

            // Sign the encrypted that the recipient can trust you..

            byte[] sign;
            using (var signer = new VirgilSigner())
            {
                sign = signer.Sign(encryptedData, privateKey);
            }

            bool isValid;
            using (var signer = new VirgilSigner())
            {
                isValid = signer.Verify(encryptedData, sign, publicKey);
            }

            var recepientContainerPassword = "UhFC36DAtrpKjPCE";

            var recepientPrivateKeysClient = new KeyringClient(new Connection(Constants.ApplicationToken));
            recepientPrivateKeysClient.Connection.SetCredentials(
                new Credentials("recepient.email@server.hz", recepientContainerPassword));

            var recepientPrivateKey = await recepientPrivateKeysClient.PrivateKeys.Get(recepientPublicKey.PublicKeyId);

            byte[] decryptedData;
            using (var cipher = new VirgilCipher())
            {
                decryptedData = cipher.DecryptWithKey(encryptedData, recepientId, recepientPrivateKey.Key);
            }
        }
    }
}