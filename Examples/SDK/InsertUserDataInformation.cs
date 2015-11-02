namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Model;

    public class InsertUserDataInformation : IExample
    {
        public async Task Run()
        {
            var keysService = new KeysClient(Constants.AppToken); // use your application access token

            var userData = new UserData
            {
                Class = UserDataClass.UserInfo,
                Type = UserDataType.FirstNameInfo,
                Value = "Denis"
            };

            try
            {
                await keysService.UserData.Insert(userData, Constants.PublicKeyId, Constants.PrivateKey);
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}