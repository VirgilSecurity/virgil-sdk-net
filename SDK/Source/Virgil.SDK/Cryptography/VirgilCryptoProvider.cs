namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The <see cref="ICryptoProvider"/> implementation that provides cryptographic operations in applications, 
    /// such as signature generation and verification, and encryption and decryption.
    /// </summary>
    internal class VirgilCryptoProvider : ICryptoProvider
    {
        private readonly ICryptoPrimitives primitives;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCryptoProvider"/> class.
        /// </summary>
        public VirgilCryptoProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCryptoProvider"/> class.
        /// </summary>
        internal VirgilCryptoProvider(ICryptoPrimitives primitives)
        {
            this.primitives = primitives;
        }

        public byte[] Encrypt(byte[] clearData, string password)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Decrypt(byte[] cipherData, string password)
        {
            throw new System.NotImplementedException();
        }
    }
} 