namespace Virgil.Examples.SDK
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Model;

    public class DeleteUserData : AsyncExample
    {
        public override async Task Execute()
        {
            var keysService = new KeysClient(Constants.AppToken); // use your application access token

            try
            {
                var publicKeyResult = await keysService.PublicKeys.SearchExtended(Constants.PublicKeyId, Constants.PrivateKey);
                var userData = publicKeyResult.UserData
                    .Where(it => it.Type == UserDataType.FirstNameInfo)
                    .Single(it => it.Value == Constants.EmailId);

                await keysService.UserData.Delete(userData.UserDataId, Constants.PublicKeyId, Constants.PrivateKey);
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}