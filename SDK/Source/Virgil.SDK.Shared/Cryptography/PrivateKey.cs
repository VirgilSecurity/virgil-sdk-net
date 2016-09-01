namespace Virgil.SDK.Cryptography
{
    using Virgil.Crypto;

    public abstract class PrivateKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKey"/> class.
        /// </summary>
        protected PrivateKey()
        {
        }

        /// <summary>
        /// Gets the public key.
        /// </summary>
        public abstract PublicKey PublicKey { get; }

        /// <summary>
        /// Exports this instance.
        /// </summary>
        public abstract byte[] Export();
    }

    internal class VirgilPrivateKey : PrivateKey
    {
        private readonly byte[] privateKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilPrivateKey"/> class.
        /// </summary>
        public VirgilPrivateKey(byte[] privateKey)
        {
            this.privateKey = privateKey;
        }

        /// <summary>
        /// Gets the public key.
        /// </summary>
        public override PublicKey PublicKey
        {
            get
            {
                var publicKey = VirgilKeyPair.ExtractPublicKey(this.privateKey, new byte[] {});
                return new PublicKey(publicKey);
            }
        }

        /// <summary>
        /// Exports this instance.
        /// </summary>
        public override byte[] Export()
        {
            return this.privateKey;
        }
    }
}