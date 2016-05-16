namespace Virgil.Examples.Crypto
{
    using System;

    using Virgil.Crypto;
    using Virgil.Examples.Common;

    public class EncryptAndDecryptWithPassword : SyncExample
    {
        public override void Execute()
        {
            var text = Param<string>.Mandatory("Enter text to encrypt").WaitInput();
            var password = Param<string>.Mandatory("Enter the password").WaitInput();

            Console.WriteLine();

            this.StartWatch();

            // Encrypt text using password provided in parameters.
            var encryptedText = CryptoHelper.Encrypt(text, password);

            this.StopWatch();

            Console.WriteLine("Encrypted Text in Base64 format:\n");
            Console.WriteLine(encryptedText);
            
            this.DisplayElapsedTime();

            this.StartWatch();

            // Decrypt encrypted text using password
            var decryptedText = CryptoHelper.Decrypt(encryptedText, password);

            this.StopWatch();

            Console.WriteLine("\nDecrypted original text:\n");
            Console.WriteLine(decryptedText);

            this.DisplayElapsedTime();
        }
    }
}