namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The <see cref="IKeyPairInfo"/> interface that describes class used to generate pairs of public and private keys.
    /// </summary>
    public interface IKeyPairGenerator
    {
        /// <summary>
        /// Generates a key pair using specified info.
        /// </summary>
        /// <param name="info">The key pair info.</param>
        /// <returns>Generated public/private key pair.</returns>
        IKeyPair Generate(IKeyPairInfo info);
    }
}