namespace Virgil.Examples.Crypto
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Virgil.Crypto;
    using Virgil.Examples.Common;

    public class Encryption : SyncExample
    {
        public override void Execute()
        {
            Console.Write("Enter text to encrypt:");
            var textToEncrypt = Convert.ToString(Console.ReadLine());

            Console.Write("Enter number of recepients: ");
            var recepientsCount = Convert.ToInt32(Console.ReadLine());

            // generate list of key recepients 
                 
            var recepients = new List<Recepient>();
            for (var index = 0; index < recepientsCount; index++)
            {
                // generate new key pair for each recepient.

                using (var keyPair = new VirgilKeyPair())
                {
                    recepients.Add(new Recepient
                    {
                        Id = Guid.NewGuid(),
                        PublicKey = keyPair.PublicKey(),
                        PrivateKey = keyPair.PrivateKey()
                    });
                }
            }

            // decrypt text for multiple recepients.

            byte[] cipherData;

            using (var cipher = new VirgilCipher())
            {
                foreach (var recepient in recepients)
                {
                    var recepientId = Encoding.UTF8.GetBytes(recepient.Id.ToString());
                    cipher.AddKeyRecipient(recepientId, recepient.PublicKey);
                }

                this.StartWatch();

                cipherData = cipher.Encrypt(Encoding.UTF8.GetBytes(textToEncrypt), true);

                this.StopWatch();
            }
            
            Console.WriteLine("Cipher Text: {0}", Convert.ToBase64String(cipherData));

            this.DisplayElapsedTime();
        }
    }
}