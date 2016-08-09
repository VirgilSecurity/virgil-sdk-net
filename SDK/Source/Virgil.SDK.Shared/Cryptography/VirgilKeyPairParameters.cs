namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public class VirgilKeyPairParameters : IKeyPairParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyPairParameters"/> class.
        /// </summary>
        /// <param name="alias">The name of the key pair.</param>
        public VirgilKeyPairParameters(string alias)
        {
            this.Alias = alias;
        }

        /// <summary>
        /// Gets the key pair alias.
        /// </summary>
        public string Alias { get; }
    }
}