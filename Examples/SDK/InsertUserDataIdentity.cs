namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Model;

    public class InsertUserDataIdentity : IExample
    {
        public async Task Run()
        {
            var keysService = new KeysClient(Constants.AppToken); // use your application access token

            var userData = new UserData
            {
                Class = UserDataClass.UserId, 
                Type = UserDataType.EmailId,
                Value = "virgil-demo+1@freeletter.me"
            };

            try
            {
                var insertingResult = await keysService.UserData.Insert(userData, Constants.PublicKeyId, Constants.PrivateKey);

                // check an email box for confirmation code.

                var userDataId = insertingResult.UserDataId;

                var code = "R6H1E4"; // confirmation code you received on email.
                await keysService.UserData.Confirm(userDataId, code, Constants.PublicKeyId, Constants.PrivateKey);
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}