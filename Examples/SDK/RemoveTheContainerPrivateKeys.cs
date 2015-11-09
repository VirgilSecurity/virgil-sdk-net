namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.SDK.PrivateKeys;
    using Virgil.SDK.PrivateKeys.Http;

    public class RemoveTheContainerPrivateKeys : AsyncExample
    {
        public async override Task Execute()
        {
            Console.Write("Enter container password: ");
            var password = Console.ReadLine();

            var credentials = new Credentials(Constants.EmailId, password);

            var keyringClient = new KeyringClient(Constants.AppToken);
            keyringClient.Connection.SetCredentials(credentials);

            this.StartWatch();

            await keyringClient.Container.Remove(Constants.PublicKeyId, Constants.PrivateKey);

            this.StopAndDisplayElapsedTime();
        }
    }
}