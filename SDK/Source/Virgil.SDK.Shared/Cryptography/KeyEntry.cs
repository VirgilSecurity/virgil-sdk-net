namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a key pair storage entry.
    /// </summary>
    internal class KeyEntry
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the key pair.
        /// </summary>
        public byte[] Value { get; set; }
        
        /// <summary>
        /// Gets or sets the meta data associated with key pair.
        /// </summary>
        public IDictionary<string, string> MetaData { get; set; }
    }
}