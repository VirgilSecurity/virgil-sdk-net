namespace Virgil.Examples.Crypto
{
    using System;
    using System.Text;
    using Virgil.Crypto;
    using Virgil.Examples.Common;

    public class SingAndVerify : SyncExample
    {
        public override void Execute()
        {
            Console.Write("Enter text to sign: ");
            var dataToSign = Encoding.UTF8.GetBytes(Console.ReadLine());

            // generate public/private key pair

            byte[] publicKey;
            byte[] privateKey;

            using (var keyPair = new VirgilKeyPair())
            {
                publicKey = keyPair.PublicKey();
                privateKey = keyPair.PrivateKey();
            }
            
            using (var signer = new VirgilSigner())
            {
                this.StartWatch();

                var sign = signer.Sign(dataToSign, privateKey);
                
                this.StopWatch();

                Console.WriteLine("Digital signature in Base64: {0}", Convert.ToBase64String(sign));
                this.DisplayElapsedTime();
                
                this.RestartWatch();

                var isValid = signer.Verify(dataToSign, sign, publicKey);

                this.StopWatch();

                Console.WriteLine("Verification result is: {0}", isValid);

                this.DisplayElapsedTime();
            }
        }
    }
}