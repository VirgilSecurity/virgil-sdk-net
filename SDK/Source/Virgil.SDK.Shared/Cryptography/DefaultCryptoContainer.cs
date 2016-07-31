namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The default implementation of <see cref="CryptoContainer"/> interface which provides a 
    /// cryptographic operations and key storage.
    /// </summary>
    public class DefaultCryptoContainer : CryptoContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCryptoContainer" /> class.
        /// </summary>
        /// <param name="parameters">The container parameters.</param>
        public DefaultCryptoContainer(CryptoContainerParams parameters) : base(parameters)
        {   
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Performs the decryption for specified <paramref name="cipherdata" />.
        /// </summary>
        /// <param name="cipherdata">The encrypted data to be decrypted.</param>
        /// <returns>A byte array containing the result from decrypt operation.</returns>
        public override byte[] PerformDecryption(byte[] cipherdata)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Performs the signature generation for specified <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data to be signed.</param>
        /// <returns>A byte array containing the result from sign operation.</returns>
        public override byte[] PerformSignatureGeneration(byte[] data)   
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Release resources and delete the key from the storage.
        /// </summary>
        public override void Remove()
        {
            throw new System.NotImplementedException();
        }
    }
}