namespace Virgil.Examples.Crypto
{
    using System;
    using System.ComponentModel;
    using Virgil.Crypto;
    using Virgil.Examples.Common;

    [Description("Decrypt text with password.")]
    public class DecryptWithPassword : SyncExample
    {
        public override void Execute()
        {
            var cipherTextBase64 = Param<string>.Mandatory("Enter encrypted text").WaitInput();
            var password = Param<string>.Mandatory("Enter the password").WaitInput();

            Console.WriteLine();

            this.StartWatch();

            // Decrypt encrypted text using password provided in parameters.
            var text = CryptoHelper.Decrypt(cipherTextBase64, password);

            this.StopWatch();

            Console.WriteLine("Decrypted Text:\n");
            Console.WriteLine(text);

            this.DisplayElapsedTime();
        }
    }
}