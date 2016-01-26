namespace Virgil.Examples.Crypto
{
    using System;
    using System.ComponentModel;
    using Virgil.Crypto;
    using Virgil.Examples.Common;

    [Description("Decrypt encrypted string with Private Key.")]
    public class DecryptWithPrivateKey : SyncExample
    {
        public override void Execute()
        {
            var cipherTextBase64 = Param<string>.Mandatory("Enter encrypted text").WaitInput();
            var privateKeyString = Param<string>.Mandatory("Enter the Private Key in Base64 format").WaitInput();

            Console.WriteLine();

            // Convert Public Key to Base64 string 
            var privateKey = Convert.FromBase64String(privateKeyString);

            this.StartWatch();

            // Decrypt encrypted text using Private Key provided in parameters.
            var text = CryptoHelper.Decrypt(cipherTextBase64, "RecipientId", privateKey);

            this.StopWatch();

            Console.WriteLine("Decrypted Text:\n");
            Console.WriteLine(text);

            this.DisplayElapsedTime();
        }
    }
}