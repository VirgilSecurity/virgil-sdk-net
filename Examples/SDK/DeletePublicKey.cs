namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Model;

    public class DeletePublicKey : IExample
    {
        public async Task Run()
        {
            try
            {
                var keysService = new KeysClient(Constants.AppToken); // use your application access token
                var resetResult = await keysService.PublicKeys.Delete(Constants.PublicKeyId);

                // once you deleted the Public Key you need to confirm this action with all User Data 
                // identities registered for this key.

                var resetConfirmation = new PublicKeyOperationComfirmation
                {
                    ActionToken = resetResult.ActionToken,
                    ConfirmationCodes = new[] { "F0G4T3", "D9S6J1" }
                };

                await keysService.PublicKeys.ConfirmDelete(Constants.PublicKeyId, resetConfirmation);
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}