namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a key pair storage entry.
    /// </summary>
    public class KeyPairEntry
    {
        /// <summary>
        /// Gets or sets the key pair.
        /// </summary>
        public byte[] PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the key pair.
        /// </summary>
        public byte[] PrivateKey { get; set; }
        
        /// <summary>
        /// Gets or sets the meta data associated with key pair.
        /// </summary>
        public IDictionary<string, string> Meta { get; set; }
    }
}