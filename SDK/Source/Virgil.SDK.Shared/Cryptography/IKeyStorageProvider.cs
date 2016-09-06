namespace Virgil.SDK.Cryptography
{
    public interface IKeyStorageProvider
    {
        /// <summary>
        /// Stores the private key (that has already been protected) to the given alias.
        /// </summary>
        /// <param name="keyEntry">The key entry.</param>
        void Store(KeyEntry keyEntry);

        /// <summary>
        /// Loads the private key associated with the given alias.
        /// </summary>
        /// <param name="keyName">The name.</param>
        /// <returns>
        /// The requested private key, or null if the given alias does not exist or does
        /// not identify a key-related entry.
        /// </returns>
        KeyEntry Load(string keyName);

        /// <summary>
        /// Checks if the private key exists in this storage by given alias.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <returns>true if the private key exists, false otherwise</returns>
        bool Exists(string keyName);

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        void Delete(string keyName);
    }
}