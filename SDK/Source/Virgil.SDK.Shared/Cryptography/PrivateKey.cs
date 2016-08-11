namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// Represents an asymmetric cryptography Private Key.
    /// </summary>
    public class PrivateKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKey"/> class.
        /// </summary>
        public PrivateKey(byte[] privateKey)
        {
            this.Value = privateKey;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        internal byte[] Value { get; }
    }
}