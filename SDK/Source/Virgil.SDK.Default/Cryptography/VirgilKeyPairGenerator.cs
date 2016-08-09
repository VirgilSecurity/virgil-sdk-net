namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The implementation of <see cref="IKeyPairInfo"/> interface used to generate pairs of public and private keys.
    /// </summary>
    public class VirgilKeyPairGenerator : IKeyPairGenerator
    {
        /// <summary>
        /// Generates a key pair using specified info.
        /// </summary>
        /// <param name="info">The key pair info.</param>
        /// <returns>Generated public/private key pair.</returns>
        public IKeyPair Generate(IKeyPairInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}