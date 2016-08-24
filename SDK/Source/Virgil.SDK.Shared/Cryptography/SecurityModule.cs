namespace Virgil.SDK.Cryptography
{
    using System;
    using System.Text;

    using Virgil.Crypto;

    public class SecurityModule : ISecurityModule, IDisposable
    {
        private readonly ICryptoService cryptoService;

        private byte[] id;
        private PrivateKey key;
        private string password;

        public SecurityModule(ICryptoService cryptoService)
        {
            this.cryptoService = cryptoService;
        }

        public void Initialize(byte[] recipientId, PrivateKey privateKey, string privateKeyPassword)
        {
            this.id = recipientId;
            this.key = privateKey;
            this.password = privateKeyPassword;
        }

        public PublicKey GetPublicKey()
        {
            var passwordData = Encoding.UTF8.GetBytes(this.password ?? "");
            return new PublicKey(VirgilKeyPair.ExtractPublicKey(this.key.Value, passwordData));
        }

        public virtual byte[] DecryptData(byte[] cipherdata)
        {
            return this.cryptoService.Decrypt(cipherdata, this.id, this.key);
        }

        public virtual byte[] SignData(byte[] data)
        {
            return this.cryptoService.Sign(data, this.key);
        }

        public void Dispose()
        {
            this.key = null;
            this.id = null;
        }
    }
}