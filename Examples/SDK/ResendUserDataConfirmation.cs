namespace Virgil.Examples.SDK
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;

    public class ResendUserDataConfirmation : IExample
    {
        public async Task Run()
        {
            var keysService = new KeysClient(Constants.AppToken); // use your application access token

            try
            {
                var publicKeyResult = await keysService.PublicKeys.SearchExtended(Constants.PublicKeyId, Constants.PrivateKey);

                // get all unconfirmed user data items from public key
                var userDataList = publicKeyResult.UserData
                    .Where(it => !it.Confirmed);

                foreach (var userData in userDataList)
                {
                    await keysService.UserData.ResendConfirmation(userData.UserDataId, Constants.PublicKeyId, 
                        Constants.PrivateKey);
                }
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}