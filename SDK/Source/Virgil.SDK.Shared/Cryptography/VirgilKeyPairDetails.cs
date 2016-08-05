namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// Represents the details for generating asymmetric keys.
    /// </summary>
    public sealed class VirgilKeyPairDetails : IKeyPairDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyPairDetails" /> class.
        /// </summary>
        /// <param name="password">The private key password.</param>
        public VirgilKeyPairDetails(string password)
        {
            this.Password = password;
        }

        /// <summary>
        /// Gets the private key password.
        /// </summary>
        public string Password { get; }
    }
}