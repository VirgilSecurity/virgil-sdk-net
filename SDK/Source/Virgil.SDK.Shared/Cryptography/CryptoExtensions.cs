namespace Virgil.SDK.Cryptography
{
    using System.Text;

    public static class CryptoExtensions
    {
        public static byte[] Sign(this Crypto crypto, string text, PrivateKey privateKey)
        {
            var data = Encoding.UTF8.GetBytes(text);
            return crypto.Sign(data, privateKey);
        }

        public static bool Verify(this Crypto crypto, string text, byte[] signature, PublicKey signer)
        {
            var data = Encoding.UTF8.GetBytes(text);
            return crypto.Verify(data, signature, signer);
        }
    }
}