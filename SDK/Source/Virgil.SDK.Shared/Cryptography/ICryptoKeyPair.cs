namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The <see cref="ICryptoKeyPair"/> represents an asymmetric key pair that is 
    /// comprised of both public and private keys.
    /// </summary>
    public interface ICryptoKeyPair
    {
        /// <summary>
        /// Gets the value of Public Key.
        /// </summary>
        byte[] PublicKey { get; }

        /// <summary>
        /// Gets the value of Private Key.
        /// </summary>
        byte[] PrivateKey { get; }
    }
}