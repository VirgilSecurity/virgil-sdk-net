namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The implementation of <see cref="IKeyPairDetails"/> interface used to generate pairs of public and private keys.
    /// </summary>
    public class VirgilKeyPairGenerator : IKeyPairGenerator
    {
        /// <summary>
        /// Generates a key pair using specified details.
        /// </summary>
        /// <param name="details">The key pair details.</param>
        /// <returns>Generated public/private key pair.</returns>
        public IKeyPair Generate(IKeyPairDetails details)
        {
            throw new System.NotImplementedException();
        }
    }
}