namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// Represents a pair of public/private keys.
    /// </summary>
    public sealed class VirgilKeyPair : IKeyPair
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyPair"/> class.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key.</param>
        public VirgilKeyPair(PublicKey publicKey, PrivateKey privateKey)
        {
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
        }

        /// <summary>
        /// Gets the public key.
        /// </summary>
        public PublicKey PublicKey { get; }

        /// <summary>
        /// Gets the private key.
        /// </summary>
        public PrivateKey PrivateKey { get; }
    }
}