namespace Virgil.Examples.Crypto
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Virgil.Crypto;
    using Virgil.Examples.Common;

    public class Encryption : SyncExample
    {
        public override void Execute()
        {
            Console.Write("Enter text to encrypt: ");
            var textToEncrypt = Convert.ToString(Console.ReadLine());
            
            // generate public/private key pair for key recipient 

            byte[] publicKey;
            byte[] privateKey;

            using (var keyPair = new VirgilKeyPair())
            {
                publicKey = keyPair.PublicKey();
                privateKey = keyPair.PrivateKey();
            }

            var keyRecepinet = new
            {
                Id = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()),
                PublicKey = publicKey,
                PrivateKey = privateKey
            };

            Console.WriteLine("Generated keys for <Key Recepient>");
            Console.WriteLine(Encoding.UTF8.GetString(keyRecepinet.PublicKey));
            Console.WriteLine(Encoding.UTF8.GetString(keyRecepinet.PrivateKey));

            // generate password based on Guid for password recipient.

            var password = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 8));

            Console.WriteLine("Generated password for <Password Recepient> {0}", Encoding.UTF8.GetString(password));
            
            // encrypting data for multiple recipients key/password

            byte[] cipherData;

            using (var cipher = new VirgilCipher())
            {
                cipher.AddPasswordRecipient(password);
                cipher.AddKeyRecipient(keyRecepinet.Id, keyRecepinet.PublicKey);

                this.StartWatch();

                cipherData = cipher.Encrypt(Encoding.UTF8.GetBytes(textToEncrypt), true);

                this.StopWatch();
            }
            
            Console.WriteLine("Cipher Text in Base64: {0}", Convert.ToBase64String(cipherData));
            this.DisplayElapsedTime();

            Console.ReadKey();

            // decrypting data with private key

            byte[] decryptedData;
            using (var cipher = new VirgilCipher())
            {
                this.RestartWatch();
                decryptedData = cipher.DecryptWithKey(cipherData, keyRecepinet.Id, keyRecepinet.PrivateKey);
                this.StopWatch();
            }

            Console.WriteLine("Decrypted Text with Private Key: {0}", Encoding.UTF8.GetString(decryptedData));
            this.DisplayElapsedTime();

            // decrypting data with password

            using (var cipher = new VirgilCipher())
            {
                this.RestartWatch();
                decryptedData = cipher.DecryptWithPassword(cipherData, password);
                this.StopWatch();
            }

            Console.WriteLine("Decrypted Text with Password: {0}", Encoding.UTF8.GetString(decryptedData));
            this.DisplayElapsedTime();
        }
    }
}