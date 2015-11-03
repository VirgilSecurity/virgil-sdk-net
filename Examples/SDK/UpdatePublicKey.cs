namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.Crypto;
    
    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;

    public class UpdatePublicKey : AsyncExample
    {
        public override async Task Execute()
        {
            try
            {
                byte[] newPublicKey;
                byte[] newPrivateKey;

                using (var keysPair = new VirgilKeyPair())
                {
                    newPublicKey = keysPair.PublicKey();
                    newPrivateKey = keysPair.PrivateKey();
                }

                var keysService = new KeysClient(Constants.AppToken); // use your application access token

                await keysService.PublicKeys.Update(Constants.PublicKeyId, newPublicKey, 
                    newPrivateKey, Constants.PrivateKey);
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}