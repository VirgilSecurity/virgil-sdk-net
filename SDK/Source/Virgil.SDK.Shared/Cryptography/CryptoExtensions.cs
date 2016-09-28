namespace Virgil.SDK.Cryptography
{
    using System;
    using System.Text;

    public static partial class CryptoExtensions
    {
        public static string EncryptText(this Crypto crypto, string plaintext, params PublicKey[] recipients)
        {
            var data = Encoding.UTF8.GetBytes(plaintext);
            var ciphertext = Convert.ToBase64String(crypto.Encrypt(data, recipients));

            return ciphertext;
        }

        public static string DecryptText(this Crypto crypto, string ciphertext, PrivateKey privateKey)
        {
            var cipherdata = Convert.FromBase64String(ciphertext);
            var plaintext = Encoding.UTF8.GetString(crypto.Decrypt(cipherdata, privateKey));

            return plaintext;
        }

        public static string SignText(this Crypto crypto, string text, PrivateKey privateKey)
        {
            var data = Encoding.UTF8.GetBytes(text);
            var signature = Convert.ToBase64String(crypto.Sign(data, privateKey));

            return signature;
        }

        public static bool VerifyText(this Crypto crypto, string text, string signature, PublicKey signer)
        {
            var data = Encoding.UTF8.GetBytes(text);
            var signatureData = Convert.FromBase64String(signature);

            return crypto.Verify(data, signatureData, signer);
        }
    }
}   