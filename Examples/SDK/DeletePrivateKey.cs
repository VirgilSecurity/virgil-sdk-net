namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;
    using Virgil.Examples.Common;
    using Virgil.SDK.Keys;
    using Virgil.SDK.PrivateKeys.Http;

    public class DeletePrivateKey : AsyncExample
    {
        public async override Task Execute()
        {
            Console.Write("Enter your email ID: ");
            var userId = Console.ReadLine();

            Console.Write("Enter container password: ");
            var password = Console.ReadLine();

            var keysService = new KeysClient(Constants.AppToken);
            var publicKey = await keysService.PublicKeys.Search(userId);

            var keyringClient = new Virgil.SDK.PrivateKeys.KeyringClient(Constants.AppToken);
            keyringClient.Connection.SetCredentials(new Credentials(userId, password));

            var privateKey = await keyringClient.PrivateKeys.Get(publicKey.PublicKeyId);

            await keyringClient.PrivateKeys.Remove(publicKey.PublicKeyId, privateKey.Key);

            Console.WriteLine("Your Private Key:\n{0}", privateKey.Key);
        }
    }
}