namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ICryptoProvider"/> implementation that provides a set of methods for dealing with 
    /// low-level cryptographic primitives and algorithms used in Virgil.Crypto library.
    /// </summary>
    internal class VirgilCryptoProvider : ICryptoProvider
    {
        public byte[] Encrypt(byte[] clearData, IDictionary<string, byte[]> recipients)
        {
            throw new System.NotImplementedException();
        }
    }
}