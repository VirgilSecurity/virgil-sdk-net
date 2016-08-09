namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ICryptoService"/> implementation that represents cryptographic operations and key storage.
    /// </summary>
    public class VirgilCryptoService : ICryptoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCryptoService"/> class.
        /// </summary>
        public VirgilCryptoService
        (
            IKeyPairGenerator keyPairGenerator,
            IPrivateKeyStorage privateKeyStorage
        )
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Initializes a crypto service with the private key previosly saved in private key storage.
        /// </summary>
        /// <param name="details">The parameters.</param>
        public void Initialize(VirgilCryptoServiceDetails details)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Encrypts data for the specified list of recipients.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        /// <param name="recipients">The recipients.</param>
        /// <returns>The encrypted data.</returns>
        public byte[] Encrypt(byte[] data, IEnumerable<PublicKey> recipients)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Decrypts data that was previously encrypted <see cref="Encrypt(byte[], IEnumerable{PublicKey})"/> method.
        /// </summary>
        /// <param name="cipherdata">The data to decrypt.</param>
        /// <returns>The decrypted data.</returns>
        public byte[] Decrypt(byte[] cipherdata)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Computes the hash value of the specified byte array, and signs the resulting hash value.
        /// </summary>
        /// <param name="data">The input data for which to compute the hash.</param>
        /// <returns>The signature for the specified data.</returns>
        public byte[] Sign(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Verifies that a digital signature is valid by calculating the hash value of the specified data, 
        /// and comparing it to the provided signature.
        /// </summary>
        /// <param name="data">The signed data.</param>
        /// <param name="signature">The signature data to be verified.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns>true if the signature is valid; otherwise, false.</returns>
        public bool Verify(byte[] data, byte[] signature, PublicKey publicKey)
        {
            throw new System.NotImplementedException();
        }
    }
}