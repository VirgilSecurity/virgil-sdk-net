namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class VirgilCardExtensions
    {
        public static VirgilBuffer Encrypt(this VirgilCard virgilCard, string plainText)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer GetSigned(this VirgilCard virgilCard, string plainText)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer GetSignedThenEncrypted(this VirgilCard virgilCard, string plainText, VirgilKey signerKey)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer Encrypt(this IEnumerable<VirgilCard> virgilCards, string plainText)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer GetSigned(this IEnumerable<VirgilCard> virgilCards, string plainText)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer GetSignedThenEncrypted(this IEnumerable<VirgilCard> virgilCards, string plainText, VirgilKey signerKey)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilBuffer> ThenEncrypt(this Task<IEnumerable<VirgilCard>> promise, string plainText)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilBuffer> ThenSignAndEncrypt(this Task<IEnumerable<VirgilCard>> promise, string plainText, VirgilKey signerKey)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilBuffer> ThenSignAndEncrypt(this Task<IEnumerable<VirgilCard>> promise, string plainText, string signerKeyName)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilBuffer> ThenDecryptAndVerify(this Task<IEnumerable<VirgilCard>> promise, VirgilBuffer cipherData, VirgilKey recipientKey)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilBuffer> ThenDecryptAndVerify(this Task<IEnumerable<VirgilCard>> promise, VirgilBuffer cipherData, string signerKeyName)
        {
            throw new NotImplementedException();
        }

        public static Task<bool> ThenVerify(this Task<IEnumerable<VirgilCard>> promise, VirgilBuffer data, VirgilBuffer signature, string signerKeyName)
        {
            throw new NotImplementedException();
        }
    }
}