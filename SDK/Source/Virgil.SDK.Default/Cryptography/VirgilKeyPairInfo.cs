namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// Represents the info for generating asymmetric keys.
    /// </summary>
    public sealed class VirgilKeyPairInfo : IKeyPairInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyPairInfo" /> class.
        /// </summary>
        /// <param name="password">The private key password.</param>
        public VirgilKeyPairInfo(string password)
        {
            this.Password = password;
        }

        /// <summary>
        /// Gets the private key password.
        /// </summary>
        public string Password { get; }
    }
}