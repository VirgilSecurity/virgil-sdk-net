namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The <see cref="CryptoContainer"/> abstract class that represents cryptographic operations and key storage.
    /// </summary>
    public abstract class CryptoContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoContainer"/> class.
        /// </summary>
        /// <param name="params">The name of the key container.</param>
        protected CryptoContainer(CryptoContainerParams @params)
        {
            this.Parameters = @params;
        }

        /// <summary>
        /// Gets the container parameters
        /// </summary>
        /// <value>The parameters.</value>
        protected CryptoContainerParams Parameters { get; }

        /// <summary>
        /// Performs the decryption for specified <paramref name="cipherdata" />.
        /// </summary>
        /// <param name="cipherdata">The encrypted data to be decrypted.</param>
        /// <returns>A byte array containing the result from decrypt operation.</returns>
        public abstract byte[] PerformDecryption(byte[] cipherdata);

        /// <summary>
        /// Performs the signature generation for specified <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data to be signed.</param>
        /// <returns>A byte array containing the result from sign operation.</returns>
        public abstract byte[] PerformSignatureGeneration(byte[] data);

        /// <summary>
        /// Release resources and delete the key from the storage.
        /// </summary>
        public abstract void Remove();
    }
}