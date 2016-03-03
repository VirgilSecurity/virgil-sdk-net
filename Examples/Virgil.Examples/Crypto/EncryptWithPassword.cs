namespace Virgil.Examples.Crypto
{
    using System;
    using System.ComponentModel;

    using Virgil.Crypto;
    using Virgil.Examples.Common;

    [Description("Encrypt text with password.")]
    public class EncryptWithPassword : SyncExample
    {
        public override void Execute()
        {
            var text = Param<string>.Mandatory("Enter text to encrypt").WaitInput();
            var password = Param<string>.Mandatory("Enter the password").WaitInput();

            Console.WriteLine();
            
            this.StartWatch();

            // Encrypt text using Public Key provided in parameters.
            var encryptedText = CryptoHelper.Encrypt(text, password);

            this.StopWatch();

            Console.WriteLine("Encrypted Text in Base64 format:\n");
            Console.WriteLine(encryptedText);

            this.DisplayElapsedTime();
        }
    }
}