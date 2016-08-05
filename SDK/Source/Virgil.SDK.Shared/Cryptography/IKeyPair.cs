namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// Represents a pair of public/private keys.
    /// </summary>
    public interface IKeyPair
    {
        /// <summary>
        /// Gets the public key.
        /// </summary>
        PublicKey PublicKey { get; }

        /// <summary>
        /// Gets the private key.
        /// </summary>
        PrivateKey PrivateKey { get; }  
    }
}