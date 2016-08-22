namespace Virgil.SDK.Shared.Cryptography
{
    using System.Collections.Generic;

    using Virgil.Crypto;
    using Virgil.SDK.Cryptography;

    public class VirgilCryptoService : ICryptoService
    {
        public byte[] EncryptData(byte[] data, IDictionary<byte[], PublicKey> recipients)
        {
            using (var cipher = new VirgilCipher())
            {
                foreach (var recipient in recipients)
                {
                    cipher.AddKeyRecipient(recipient.Key, recipient.Value.Value);
                }

                var encryptedData = cipher.Encrypt(data, true);
                return encryptedData;
            }
        }

        public byte[] DecryptData(byte[] cipherdata, byte[] recipientId, PrivateKey privateKey)
        {
            using (var cipher = new VirgilCipher())
            {
                var data = cipher.DecryptWithKey(cipherdata, recipientId, privateKey.Value);
                return data;
            }
        }

        public byte[] SignData(byte[] data, PrivateKey privateKey)
        {
            using (var signer = new VirgilSigner())
            {
                var signature = signer.Sign(data, privateKey.Value);
                return signature;
            }
        }

        public bool VerifyData(byte[] data, byte[] signature, PublicKey publicKey)
        {
            using (var signer = new VirgilSigner())
            {
                var isValid = signer.Verify(data, signature, publicKey.Value);
                return isValid;
            }
        }
    }
}
