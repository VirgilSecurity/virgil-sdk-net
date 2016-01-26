namespace Virgil.Examples.Crypto
{
    using System;
    using System.ComponentModel;

    using Virgil.Crypto;
    using Virgil.Examples.Common;

    [Description("Encrypt text with recipient's Public Key.")]
    public class EncryptWithPublicKey : SyncExample
    {
        public override void Execute()
        {
            var text = Param<string>.Mandatory("Enter text to encrypt").WaitInput();
            var publicKeyString = Param<string>.Mandatory("Enter recipient's Public Key in Base64 format").WaitInput();

            Console.WriteLine();

            // Convert Public Key to Base64 string 
            var publicKey = Convert.FromBase64String(publicKeyString);

            this.StartWatch();

            // Encrypt text using Public Key provided in parameters.
            var encryptedText = CryptoHelper.Encrypt(text, "RecipientId", publicKey);

            this.StopWatch();
            
            Console.WriteLine("Encrypted Text in Base64 format:\n");
            Console.WriteLine(encryptedText);
            
            this.DisplayElapsedTime();
        }
    }
}