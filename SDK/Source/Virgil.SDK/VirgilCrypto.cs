namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;

    using Virgil.SDK.Cryptography;

    /// <summary>
    /// The Virgil Cryptography high level API that provides a cryptographic operations in applications, such as
    /// signature generation and verification, and encryption and decryption.
    /// </summary>
    public sealed class VirgilCrypto : IVirgilCrypto
    {
        private readonly ICryptoProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCrypto"/> class.
        /// </summary>
        public VirgilCrypto()
        {
            this.provider = new VirgilCryptoProvider();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCrypto"/> class.
        /// </summary>
        public VirgilCrypto(ICryptoProvider provider)
        {
            this.provider = provider;
        }

        public IVirgilKey CreateKey(string keyName)
        {
            throw new NotImplementedException();
        }

        public IVirgilKey CreateOrLoadKey(string keyName)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Encrypt(VirgilBuffer data, string password)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Encrypt(VirgilBuffer data, IEnumerable<IVirgilCard> recipients)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer EncryptText(string plainText, IEnumerable<IVirgilCard> recipients)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer EncryptText(string plainText, IVirgilCard recipient)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer EncryptText(string plainText, string recipientId, byte[] recipientPublicKey)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer EncryptText(string plainText, string password)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer SignThenEncrypt(VirgilBuffer data, IVirgilKey key, IEnumerable<IVirgilCard> recipients)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Decrypt(VirgilBuffer cipherData, string password)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Decrypt(VirgilBuffer cipherData, IVirgilKey key)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer DecryptThenVerify(VirgilBuffer cipherData, IVirgilKey key, IVirgilCard senderCard)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Sign(VirgilBuffer data, IVirgilKey key)
        {
            throw new NotImplementedException();
        }

        public bool Verify(VirgilBuffer data, VirgilBuffer signature, IVirgilCard card)
        {
            throw new NotImplementedException();
        }

        public static IVirgilCrypto Default { get; }

        public static void SetDefaultCryptoProvider(ICryptoProvider cryptoProvider)
        {
            throw new NotImplementedException();
        }

        public static void SetDefaultKeyStorage(IKeyStorage keyStorage)
        {
            throw new NotImplementedException();
        }
    }

    public interface IKeyStorage
    {
    }
}