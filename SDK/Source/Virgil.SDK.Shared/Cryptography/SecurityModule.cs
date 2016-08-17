namespace Virgil.SDK.Cryptography
{
    using System;

    public class SecurityModule : ISecurityModule, IDisposable
    {
        private readonly ICryptoService cryptoService;

        private byte[] id;
        private PrivateKey key;

        public SecurityModule(ICryptoService cryptoService)
        {
            this.cryptoService = cryptoService;
        }

        public void Initialize(byte[] recipientId, PrivateKey privateKey)
        {
            this.id = recipientId;
            this.key = privateKey;
        }

        public virtual byte[] DecryptData(byte[] cipherdata)
        {
            return this.cryptoService.DecryptData(cipherdata, this.id, this.key);
        }

        public virtual byte[] SignData(byte[] data)
        {
            return this.cryptoService.SignData(data, this.key);
        }

        public void Dispose()
        {
            this.key = null;
            this.id = null;
        }
    }
}