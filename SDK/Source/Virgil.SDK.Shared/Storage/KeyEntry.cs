namespace Virgil.SDK.Storage
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a key pair storage entry.
    /// </summary>
    public class KeyEntry
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the key pair.
        /// </summary>
        public byte[] Value { get; set; }
        
        /// <summary>
        /// Gets or sets the meta data associated with key pair.
        /// </summary>
        public IDictionary<string, object> MetaData { get; set; }
    }
}