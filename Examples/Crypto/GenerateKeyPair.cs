namespace Virgil.Examples.Crypto
{
    using System;
    using System.ComponentModel;
    using System.Text;

    using Virgil.Crypto;
    using Virgil.Examples.Common;

    [Description("Generating a public/private key pair with options")]
    public class GenerateKeys : SyncExample
    {
        public override void Execute()
        {
            var password = ConsoleHelper.ReadValue<string>("Enter password (optional)");
            var type = ConsoleHelper.ReadValue<VirgilKeyPair.Type>("Enter keys type");
            Console.WriteLine();

            this.StartWatch();
            
            var keyPair = VirgilKeyPair.Generate(type, Encoding.UTF8.GetBytes(password));
            
            var publicKey = keyPair.PublicKey();
            var privateKey = keyPair.PrivateKey();
            
            Console.WriteLine(Encoding.UTF8.GetString(publicKey));
            Console.WriteLine(Encoding.UTF8.GetString(privateKey));

            this.DisplayElapsedTime();
        }
    }
}