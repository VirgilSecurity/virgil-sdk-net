namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;
    using Virgil.Examples.Common;

    public class GetContainerType : AsyncExample
    {
        public async override Task Execute()
        {
            var keyringClient = new Virgil.SDK.PrivateKeys.KeyringClient(Constants.AppToken);
            var type = await keyringClient.Container.GetContainerType(Constants.PublicKeyId);
            
            Console.WriteLine("Container Type: {0}", type);
        }
    }
}