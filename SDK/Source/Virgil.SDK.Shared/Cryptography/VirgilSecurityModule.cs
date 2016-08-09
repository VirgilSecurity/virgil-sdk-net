namespace Virgil.SDK.Cryptography
{
    using System;
    using System.IO;

    public class VirgilSecurityModule : ISecurityModule, IDisposable
    {
        private readonly IKeyStorage keyStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilSecurityModule"/> class.
        /// </summary>
        public VirgilSecurityModule(IKeyStorage keyStorage)
        {
            this.keyStorage = keyStorage;
        }
        
        public void GenerateKeyPair(IKeyPairParameters parameters)
        {
            throw new NotImplementedException();
        }

        public byte[] DecryptData(byte[] cipherdata, string recipientId, string keyName)
        {
            throw new NotImplementedException();
        }

        public byte[] DecryptStream(Stream cipherstream, string recipientId, string keyName)
        {
            throw new NotImplementedException();
        }

        public byte[] SignData(byte[] data, string keyName)
        {
            throw new NotImplementedException();
        }

        public byte[] SignStream(Stream cipherstream, string keyName)
        {
            throw new NotImplementedException();
        }

        public PublicKey ExportPublicKey(string keyName)
        {
            throw new NotImplementedException();
        }

        public PrivateKey ExportPrivateKey(string keyName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}