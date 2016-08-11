namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// Represents an asymmetric cryptography Public Key.
    /// </summary>
    public class PublicKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKey"/> class.
        /// </summary>
        public PublicKey(byte[] publicKey)
        {
            this.Value = publicKey;
        }

        /// <summary>
        /// Gets the public key value.
        /// </summary>
        internal byte[] Value { get; }
    }
}