namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;
    using System.IO;

    public abstract class Crypto : ICrypto
    {
        public abstract PrivateKey GeneratePrivateKey();
        public abstract PrivateKey GeneratePrivateKey(PrivateKeyPatameters parameters);
        public abstract PrivateKey RevealPrivateKey(byte[] privateKey);
        public abstract byte[] ExportPrivateKey(PrivateKey privateKey);
        public abstract byte[] Encrypt(byte[] data, IEnumerable<IRecipient> recipients);
        public abstract Stream Encrypt(Stream stream, IEnumerable<IRecipient> recipients);
        public abstract byte[] Decrypt(byte[] cipherdata, byte[] recipientId, PrivateKey privateKey);
        public abstract Stream Decrypt(Stream cipherstream, byte[] recipientId, PrivateKey privateKey);
        public abstract byte[] Sign(byte[] data, PrivateKey privateKey);
        public abstract byte[] Sign(Stream stream, PrivateKey privateKey);
        public abstract bool Verify(byte[] data, byte[] signature, PublicKey signer);
        public abstract bool Verify(Stream stream, byte[] signature, PublicKey signer);
    }
}