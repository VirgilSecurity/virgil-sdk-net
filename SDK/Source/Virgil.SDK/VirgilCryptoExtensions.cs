namespace Virgil.SDK
{
    using System;
    using System.IO;

    public static class VirgilCryptoExtensions
    {
        //public static VirgilBuffer EncryptText(this IVirgilCrypto crypto, string plainText, IEquatable<IVirgilCard> recipients)
        //{
        //    throw new NotImplementedException();
        //}

        //public static VirgilBuffer EncryptText(this IVirgilCrypto crypto, string plainText, IVirgilCard recipient)
        //{
        //    throw new NotImplementedException();
        //}

        //public static VirgilBuffer EncryptText(this IVirgilCrypto crypto, string plainText, string recipientId, byte[] recipientPublicKey)
        //{
        //    throw new NotImplementedException();
        //}

        //public static VirgilBuffer EncryptText(this IVirgilCrypto crypto, string plainText, string password)
        //{
        //    throw new NotImplementedException();
        //}

        public static VirgilBuffer EncryptBytes(this IVirgilCrypto crypto, byte[] bytes, string password)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer EncryptStream(this IVirgilCrypto crypto, Stream stream, string password)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer DecryptText(this IVirgilCrypto crypto, string plainText, string password)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer DecryptBase64String(this IVirgilCrypto crypto, string base64String, string password)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer DecryptBytes(this IVirgilCrypto crypto, byte[] bytes, string password)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer DecryptStream(this IVirgilCrypto crypto, Stream stream, string password)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer SignText(this IVirgilCrypto crypto, string plainText, IVirgilKey key)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer SignBytes(this IVirgilCrypto crypto, byte[] bytes, IVirgilKey key)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer VerifyText(this IVirgilCrypto crypto, string plainText, VirgilBuffer signature, IVirgilCard ownerCard)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer VerifyText(this IVirgilCrypto crypto, string plainText, VirgilBuffer signature, byte[] ownerPublicKey)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer VerifyBytes(this IVirgilCrypto crypto, byte[] data, VirgilBuffer signature, IVirgilCard ownerCard)
        {
            throw new NotImplementedException();
        }

        public static VirgilBuffer VerifyBytes(this IVirgilCrypto crypto, byte[] data, VirgilBuffer signature, byte[] ownerPublicKey)
        {
            throw new NotImplementedException();
        }
    }
}   