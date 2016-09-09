namespace Virgil.SDK.Cryptography
{
    using System.IO;

    public abstract class Crypto<TPublicKey, TPrivateKey> : ICrypto
        where TPublicKey : IPublicKey
        where TPrivateKey : IPrivateKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Crypto{TPublicKey, TPrivateKey}"/> class.
        /// </summary>
        protected internal Crypto()
        {
        }

        public abstract IPrivateKey GenerateKey();
        public abstract IPrivateKey ImportKey(byte[] keyData);
        public abstract IPublicKey ImportPublicKey(byte[] keyData);
        public abstract byte[] ExportKey(IPrivateKey privateKey);
        public abstract byte[] ExportPublicKey(IPublicKey publicKey);
        public abstract byte[] Encrypt(byte[] data, params IPublicKey[] recipients);
        public abstract void Encrypt(Stream stream, Stream cipherStream, params IPublicKey[] recipients);
        public abstract bool Verify(byte[] data, byte[] signature, IPublicKey signer);
        public abstract bool Verify(Stream stream, byte[] signature, IPublicKey signer);
        public abstract byte[] Decrypt(byte[] cipherData, IPrivateKey privateKey);
        public abstract void Decrypt(Stream cipherStream, Stream outStream, IPrivateKey privateKey);
        public abstract byte[] Sign(byte[] data, IPrivateKey privateKey);
        public abstract byte[] Sign(Stream stream, IPrivateKey privateKey);
    }
}