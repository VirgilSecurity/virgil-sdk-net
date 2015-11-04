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
                var sign = signer.Sign(dataToSign, privateKey);

                Console.WriteLine("Digital signature in Base64: {0}", Convert.ToBase64String(sign));
                
                var isValid = signer.Verify(dataToSign, sign, publicKey);

                Console.WriteLine("Is valid: {0}", isValid);
            }
        }
    }
}