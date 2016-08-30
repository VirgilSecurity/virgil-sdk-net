namespace Virgil.SDK.Cryptography
{
    using System.IO;
    using System.Collections.Generic;

    public interface ICryptoSession
    {
        void GenerateKeyPair(out PublicKey publicKey, out PrivateKey privateKey);

        byte[] Encrypt(byte[] data, IEnumerable<IRecipient> recipients);

        Stream Encrypt(Stream stream, IEnumerable<IRecipient> recipients);

        bool Verify(byte[] data, byte[] signature, PublicKey publicKey);

        bool Verify(Stream stream, byte[] signature, PublicKey publicKey);

        byte[] Decrypt(byte[] cipherdata, byte[] recipientId, PrivateKey privateKey);

        Stream Decrypt(Stream cipherstream, byte[] recipientId, PrivateKey privateKey);

        byte[] Sign(byte[] data, PrivateKey privateKey);

        byte[] Sign(Stream cipherstream, PrivateKey privateKey);

        byte[] ExportObject(ObjectHandle handle);
    }
}