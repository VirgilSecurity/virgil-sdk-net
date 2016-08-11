namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// This class is a simple holder for a key pair (a public key and a private key). It does not enforce 
    /// any security, and, when initialized, should be treated like a PrivateKey.
    /// </summary>
    public class KeyPair
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPair"/> class.
        /// </summary>
        public KeyPair(byte[] publicKey, byte[] privateKey)
        {
            this.PublicKey = new PublicKey(publicKey);
            this.PrivateKey = new PrivateKey(privateKey);
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