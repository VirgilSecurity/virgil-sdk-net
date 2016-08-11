namespace Virgil.SDK.Cryptography
{
    using System;
    using System.IO;

    using Virgil.SDK.Exceptions;

    public class VirgilSecurityModule : ISecurityModule
    {
        private readonly IKeyStorage keyStorage;
        private readonly IKeyPairGenerator keyPairGenerator;

        private PrivateKey privateKey;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilSecurityModule"/> class.
        /// </summary>
        public VirgilSecurityModule(IKeyStorage keyStorage, IKeyPairGenerator keyPairGenerator)
        {
            this.keyStorage = keyStorage;
            this.keyPairGenerator = keyPairGenerator;
        }

        /// <summary>
        /// Initializes the specified key pair by specified name and other parameters.
        /// </summary>
        /// <param name="pairName">The name of the key pair.</param>
        /// <param name="behavior">The security module initialization behavior.</param>
        /// <param name="keyPairParameters">The key pair parameters.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyPairAlreadyExistsException"></exception>
        public void Initialize(string pairName, SecurityModuleBehavior behavior, IKeyPairParameters keyPairParameters)
        {
            if (string.IsNullOrWhiteSpace(pairName))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(pairName));

            KeyPairEntry keyPairEntry;

            switch (behavior)
            {
                case SecurityModuleBehavior.CreateLongTermKeyPair:
                    keyPairEntry = this.CreateNewKeyPair(pairName, keyPairParameters);
                    break;
                case SecurityModuleBehavior.UseExistingKeyPair:
                    keyPairEntry = this.LoadExistingKeyPair(pairName);
                    break;
                default:
                    throw new NotSupportedException();  
            }

            this.privateKey = new PrivateKey(keyPairEntry.PrivateKey);
        }
        
        public byte[] DecryptData(byte[] cipherdata)
        {
            throw new System.NotImplementedException();
        }

        public byte[] DecryptStream(Stream cipherstream)
        {
            throw new System.NotImplementedException();
        }

        public byte[] SignData(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public byte[] SignStream(Stream cipherstream)
        {
            throw new System.NotImplementedException();
        }

        public PublicKey ExportPublicKey()
        {
            throw new System.NotImplementedException();
        }

        public PrivateKey ExportPrivateKey()
        {
            throw new System.NotImplementedException();
        }
        
        private KeyPairEntry CreateNewKeyPair(string name, IKeyPairParameters parameters)   
        {
            if (this.keyStorage.Exists(name))
                throw new KeyPairAlreadyExistsException();

            if (parameters == null)
            {
                parameters = new VirgilKeyPairParameters();
            }

            var keyPair = this.keyPairGenerator.Generate(parameters);
            var keyPairEntry = new KeyPairEntry
            {
                PublicKey = keyPair.PublicKey.Value,
                PrivateKey = keyPair.PrivateKey.Value
            };

            this.keyStorage.Store(name, keyPairEntry);

            return keyPairEntry;
        }

        private KeyPairEntry LoadExistingKeyPair(string name)
        {
            throw new NotImplementedException();
        }
    }
}