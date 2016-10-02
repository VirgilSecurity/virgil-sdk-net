namespace Virgil.SDK.Storage
{
    /// <summary>
    /// This interface describes a storage facility for cryptographic keys.
    /// </summary>
    public interface IKeyStorage
    {
        /// <summary>
        /// Stores the key to the given alias.
        /// </summary>
        /// <param name="keyEntry">The key entry.</param>
        void Store(KeyEntry keyEntry);

        /// <summary>
        /// Loads the key associated with the given alias.
        /// </summary>
        /// <param name="keyName">The name.</param>
        /// <returns>
        /// The requested key, or null if the given alias does not exist or does
        /// not identify a key-related entry.
        /// </returns>
        KeyEntry Load(string keyName);

        /// <summary>
        /// Checks if the key exists in this storage by given alias.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <returns>true if the key exists, false otherwise</returns>
        bool Exists(string keyName);

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        void Delete(string keyName);
    }
}
