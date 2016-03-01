namespace Virgil.Examples.Crypto
{
    using System;
    using System.ComponentModel;
    using System.Text;

    using Virgil.Crypto;
    using Virgil.Examples.Common;

    [Description("Generating a Public/Private Key Pair with options")]
    public class GenerateKeyPair : SyncExample
    {
        public override void Execute()
        {
            var password = Param<string>.Optional("Enter password").WaitInput();
            var type = Param<VirgilKeyPair.Type>.Optional("Enter keys type").WaitInput();
            var isBase64 = Param<bool>.Optional("Output in Base64 format yes/no?").WaitInput();
           
            Console.WriteLine();

            this.StartWatch();
            
            var keyPair = string.IsNullOrWhiteSpace(password) 
                ? VirgilKeyPair.Generate(type)
                : VirgilKeyPair.Generate(type, Encoding.UTF8.GetBytes(password));

            this.StopWatch();

            // Output
            
            var publicKey = keyPair.PublicKey();
            var privateKey = keyPair.PrivateKey();

            if (isBase64)
            {
                Console.WriteLine("Public Key\n\n" + Convert.ToBase64String(publicKey) + "\n");
                Console.WriteLine("Private Key\n\n" + Convert.ToBase64String(privateKey) + "\n");
            }
            else
            {
                Console.WriteLine(Encoding.UTF8.GetString(publicKey));
                Console.WriteLine(Encoding.UTF8.GetString(privateKey));
            }

            this.DisplayElapsedTime();
        }
    }
}