namespace Virgil.Examples.Crypto
{
    using System;
    using System.Runtime.Remoting.Services;
    using Virgil.Examples.Common;

    public class DecryptData : SyncExample
    {
        public override void Execute()
        {
            Console.WriteLine("Enter cipher text to decrypt: ");
            var cipherText = Console.ReadLine();
        }
    }
}