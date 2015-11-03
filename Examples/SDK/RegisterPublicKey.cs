namespace Virgil.Examples.SDK
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.Crypto;
    
    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Model;

    public class RegisterPublicKey : AsyncExample
    {
        public override async Task Execute()
        {
            byte[] publicKey;
            byte[] privateKey;

            // generate Public/Private key pair without password

            using (var keysPair = new VirgilKeyPair())
            {
                publicKey = keysPair.PublicKey();
                privateKey = keysPair.PrivateKey();
            }

            var userData = new UserData
            {
                Class = UserDataClass.UserId,
                Type = UserDataType.EmailId,
                Value = Constants.EmailId // here should be your email address
            };

            try
            {
                var keysService = new KeysClient(Constants.AppToken); // use your application access token
                var result = await keysService.PublicKeys.Create(publicKey, privateKey, userData);

                // check an email box for confirmation code.

                var userDataId = result.UserData.First().UserDataId;

                var confirmationCode = "K5J1E4"; // confirmation code you received on email.
                await keysService.UserData.Confirm(userDataId, confirmationCode, result.PublicKeyId, privateKey);
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}