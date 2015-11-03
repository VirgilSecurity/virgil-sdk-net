namespace Virgil.Examples.Crypto
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Virgil.Crypto;
    using Virgil.Examples.Common;

    public class GenerateKeys : SyncExample
    {
        public override void Execute()
        {
            Console.Write("Enter password (optional): ");
            var password = Console.ReadLine();

            var isPassword = !string.IsNullOrWhiteSpace(password);

            this.StartWatch();
            
            var keyPair = isPassword
                ? new VirgilKeyPair(Encoding.UTF8.GetBytes(password))
                : new VirgilKeyPair();

            this.StopWatch();

            byte[] publicKey;
            byte[] privateKey;

            using (keyPair)
            {
                publicKey = keyPair.PublicKey();
                privateKey = keyPair.PrivateKey();
            }

            Console.WriteLine(Encoding.UTF8.GetString(publicKey));
            Console.WriteLine(Encoding.UTF8.GetString(privateKey));

            this.DisplayElapsedTime();
        }
    }
}