namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// This interface describes a storage facility for cryptographic keys.
    /// </summary>
    internal interface IKeyStore
    {
        /// <summary>
        /// Stores the private key (that has already been protected) to the given alias.
        /// </summary>
        /// <param name="keyEntry">The key entry.</param>
        void Store(KeyEntry keyEntry);

        /// <summary>
        /// Loads the private key associated with the given alias.
        /// </summary>
        /// <param name="keyId">The key identifier.</param>
        /// <returns>
        /// The requested private key, or null if the given alias does not exist or does
        /// not identify a key-related entry.
        /// </returns>
        KeyEntry Load(string keyId);

        /// <summary>
        /// Checks if the private key exists in this storage by given alias.
        /// </summary>
        /// <param name="keyId">The key identifier.</param>
        /// <returns>true if the private key exists, false otherwise</returns>
        bool Exists(string keyId);

        /// <summary>
        /// Deletes the private key from key store by given Id.
        /// </summary>
        /// <param name="keyId">The key Id.</param>
        void Delete(string keyId);
    }
}
