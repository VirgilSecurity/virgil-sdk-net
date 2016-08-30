namespace Virgil.SDK.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class CryptoSession : ICryptoSession, IDisposable
    {
        public void GenerateKeyPair(out PublicKey publicKey, out PrivateKey privateKey)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Encrypt(byte[] data, IEnumerable<IRecipient> recipients)
        {
            throw new System.NotImplementedException();
        }

        public Stream Encrypt(Stream stream, IEnumerable<IRecipient> recipients)
        {
            throw new System.NotImplementedException();
        }

        public bool Verify(byte[] data, byte[] signature, PublicKey publicKey)
        {
            throw new System.NotImplementedException();
        }

        public bool Verify(Stream stream, byte[] signature, PublicKey publicKey)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Decrypt(byte[] cipherdata, byte[] recipientId, PrivateKey privateKey)
        {
            throw new System.NotImplementedException();
        }

        public Stream Decrypt(Stream cipherstream, byte[] recipientId, PrivateKey privateKey)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Sign(byte[] data, PrivateKey privateKey)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Sign(Stream cipherstream, PrivateKey privateKey)
        {
            throw new System.NotImplementedException();
        }

        public byte[] ExportObject(ObjectHandle handle)
        {
            throw new NotImplementedException();
        }

        public void DestroyObject(ObjectHandle handle)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}