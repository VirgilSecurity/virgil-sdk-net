namespace Virgil.SDK
{
    /// <summary>
    /// The <see cref="VirgilKey"/> creating parameters.
    /// </summary>
    public class VirgilKeyDetails : IKeyPairDetails
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="VirgilKey"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Private Key password.
        /// </summary>
        public string Password { get; set; }
    }
}