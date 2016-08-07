namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// Contains parameters that are passed to the cryptographic service that performs cryptographic computations.
    /// </summary>
    public class VirgilCryptoServiceDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCryptoServiceDetails"/> class.
        /// </summary>
        /// <param name="containerName">The name of the container.</param>
        /// <param name="keyPairInfo">The key pair information.</param>
        /// <param name="initAction">if set to <c>true</c> use existing key pair.</param>
        public VirgilCryptoServiceDetails
        (
            string containerName, 
            IKeyPairInfo keyPairInfo,
            VirgilCryptoServiceInitAction initAction
        )
        {
            this.ContainerName = containerName;
            this.KeyPairInfo = keyPairInfo;
            this.InitAction = initAction;
        }

        /// <summary>
        /// Gets the name of the key pair container.
        /// </summary>
        public string ContainerName { get; }

        /// <summary>
        /// Gets the key pair information.
        /// </summary>
        public IKeyPairInfo KeyPairInfo { get; }

        /// <summary>
        /// Gets a value indicating whether use existing key pair.
        /// </summary>
        public VirgilCryptoServiceInitAction InitAction { get; }
    }
}