namespace Virgil.SDK.Cryptography
{
    using System;
    using System.IO;

    using Virgil.SDK.Storage;

    public class SecurityModule : ISecurityModule, IDisposable
    {
        private readonly SecurityModuleParameters parameters;

        private readonly CryptoService cryptoService;
        private readonly IPrivateKeyStorage privateKeyStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityModule"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public SecurityModule(SecurityModuleParameters parameters)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityModule" /> class.
        /// </summary>
        /// <param name="cryptoService">The crypto service.</param>
        /// <param name="privateKeyStorage">The private key storage.</param>
        public SecurityModule
        (
            CryptoService cryptoService, 
            IPrivateKeyStorage privateKeyStorage
        )
        {
            this.cryptoService = cryptoService;
            this.privateKeyStorage = privateKeyStorage;
        }
        
        public PublicKey PublicKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public byte[] Decrypt(byte[] cipherdata)
        {
            throw new NotImplementedException();
        }

        public Stream Decrypt(Stream cipherstream)
        {
            throw new NotImplementedException();
        }

        public byte[] Sign(byte[] data)
        {
            throw new NotImplementedException();
        }

        public Stream Sign(Stream stream)
        {
            throw new NotImplementedException();
        }

        public byte[] Export()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class SecurityModuleParameters
    {
        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the storage.
        /// </summary>
        public IPrivateKeyStorage PrivateKeyStorage { get; set; }
    }
}