namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The <see cref="IKeyPairDetails"/> interface that describes class used to generate pairs of public and private keys.
    /// </summary>
    public interface IKeyPairGenerator
    {
        /// <summary>
        /// Generates a key pair using specified details.
        /// </summary>
        /// <param name="details">The key pair details.</param>
        /// <returns>Generated public/private key pair.</returns>
        IKeyPair Generate(IKeyPairDetails details);
    }
}