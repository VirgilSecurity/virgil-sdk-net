namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;

    public class InitializeContainerForPrivateKeys : AsyncExample
    {
        public async override Task Execute()
        {
            Console.Write("Enter container password: ");
            var password = Console.ReadLine();

            var keyringClient = new Virgil.SDK.PrivateKeys.KeyringClient(Constants.AppToken);

            this.StartWatch();

            await keyringClient.Container.Initialize(ContainerType.Easy, Constants.PublicKeyId, Constants.PrivateKey, password);

            this.StopWatch();

            Console.WriteLine("Container has been successfully initialized.");

            this.DisplayElapsedTime();    
        }
    }
}