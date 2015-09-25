namespace Virgil.Basics
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Virgil.Crypto;
    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Model;
    using Virgil.SDK.PrivateKeys;
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;

    public class Quickstart
    {
        private const string AppToken = "45fd8a505f50243fa8400594ba0b2b29";
        private const string EmailId = "virgiltest@freeletter.me";
        
        public async Task Run()
        {
            var keysService = new KeysClient(AppToken);
            var privateKeysService = new KeyringClient(AppToken);

            byte[] publicKey;
            byte[] privateKey;

            // Step 1. Generate Public/Private key pair.

            using (var keyPair = new VirgilKeyPair())
            {
                publicKey = keyPair.PublicKey();
                privateKey = keyPair.PrivateKey();
            }

            Console.WriteLine("Generated Public/Private keys\n");
            Console.WriteLine(Encoding.UTF8.GetString(publicKey));
            Console.WriteLine(Encoding.UTF8.GetString(privateKey));

            // Step 2. Register Public Key on Keys Service.

            var userData = new UserData
            {
                Class = UserDataClass.UserId,
                Type = UserDataType.EmailId,
                Value = EmailId
            };

            var vPublicKey = await keysService.PublicKeys.Create(publicKey, privateKey, userData);

            // Step 3. Confirm UDID (User data identity) with code recived on email box.

            Console.WriteLine("Enter Confirmation Code:");
            var confirmCode = Console.ReadLine();

            await keysService.UserData.Confirm(vPublicKey.UserData.First().UserDataId, confirmCode, vPublicKey.PublicKeyId, privateKey);
            
            Console.WriteLine("Public Key has been successfully published.");

            // Step 4. Store Private Key on Private Keys Service.

            var containerPassword = "2SZGfXN7WJmsmpQs";
            await privateKeysService.Container.Initialize(ContainerType.Easy, vPublicKey.PublicKeyId, privateKey, containerPassword);

            privateKeysService.Connection.SetCredentials(new Credentials(EmailId, containerPassword));

            await privateKeysService.PrivateKeys.Add(vPublicKey.PublicKeyId, privateKey);

            // Step 5. Encrypt data for another Virgil Security user

            var data = Encoding.UTF8.GetBytes("Encrypt me!");
            var recipientPublicKey = await keysService.PublicKeys.Search("virgiltest1@freeletter.me");

            byte[] encryptedData;
            using (var cipher = new VirgilCipher())
            {
                cipher.AddKeyRecipient(Encoding.UTF8.GetBytes(
                    recipientPublicKey.PublicKeyId.ToString()), recipientPublicKey.Key);

                encryptedData = cipher.Encrypt(data);
            } 

            // Step 6. Sign encrypted data

            byte[] signature;
            using (var signer = new VirgilSigner())
            {
                signature = signer.Sign(encryptedData, privateKey);
            }

            Console.ReadKey();
        }

        private void 
    }
}