namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;
    using Virgil.Examples.Common;
    using Virgil.SDK.PrivateKeys.Http;

    public class DeleteContainerWithPrivateKeys : AsyncExample
    {
        public async override Task Execute()
        {
            Console.Write("Enter your email ID: ");
            var userId = Console.ReadLine();

            Console.Write("Enter container password: ");
            var password = Console.ReadLine();

            var keyringClient = new Virgil.SDK.PrivateKeys.KeyringClient(Constants.AppToken);
            keyringClient.Connection.SetCredentials(new Credentials(userId, password));

            await keyringClient.Container.Remove(Constants.PublicKeyId, Constants.PrivateKey);

            Console.Write("Container has been successfully removed.");
        }
    }
}