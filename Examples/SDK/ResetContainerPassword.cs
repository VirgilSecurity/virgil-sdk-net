namespace Virgil.Examples.SDK
{
    using System;
    using System.Threading.Tasks;
    using Virgil.Examples.Common;
    using Virgil.SDK.PrivateKeys.Http;

    public class ResetContainerPassword : AsyncExample
    {
        public async override Task Execute()
        {
            Console.Write("Enter your email ID: ");
            var userId = Console.ReadLine();

            Console.Write("Enter new Container password: ");
            var newPassword = Console.ReadLine();

            var keyringClient = new Virgil.SDK.PrivateKeys.KeyringClient(Constants.AppToken);
            
            await keyringClient.Container.ResetPassword(userId, newPassword);

            var confirmationCode = "G7L1T4"; // confirmation code you received on email.
            await keyringClient.Container.Confirm(confirmationCode);

            keyringClient.Connection.SetCredentials(new Credentials(userId, newPassword));

            Console.Write("Container has been successfully removed.");
        }
    }
}