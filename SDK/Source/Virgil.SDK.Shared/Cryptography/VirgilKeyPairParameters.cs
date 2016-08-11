namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// Represents a key pair generation parameters.
    /// </summary>
    public class VirgilKeyPairParameters : IKeyPairParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyPairParameters"/> class.
        /// </summary>
        /// <param name="keyPairType">Type of the key pair.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        public VirgilKeyPairParameters(VirgilKeyPairType keyPairType, string privateKeyPassword = null)
        {
            this.KeyPairType = keyPairType;
            this.PrivateKeyPassword = privateKeyPassword;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyPairParameters"/> class.
        /// </summary>
        /// <param name="privateKeyPassword">The private key password.</param>
        public VirgilKeyPairParameters(string privateKeyPassword = null)
        {
            this.KeyPairType = VirgilKeyPairType.Default;
            this.PrivateKeyPassword = privateKeyPassword;
        }

        /// <summary>
        /// Gets the type of the key pair.
        /// </summary>
        public VirgilKeyPairType KeyPairType { get; }

        /// <summary>
        /// Gets the private key password.
        /// </summary>
        public string PrivateKeyPassword { get; }
    }
}