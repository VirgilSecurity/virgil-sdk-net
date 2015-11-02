namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;
    using Virgil.Examples.Common;
    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;

    public class DeletePublicKeySigned : IExample
    {
        public async Task Run()
        {
            try
            {
                var keysService = new KeysClient(Constants.AppToken); // use your application access token
                await keysService.PublicKeys.Delete(Constants.PublicKeyId, Constants.PrivateKey);
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}