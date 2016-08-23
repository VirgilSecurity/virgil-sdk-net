namespace Virgil.SDK.Storage
{
    /// <summary>
    /// This interface describes a storage facility for cryptographic keys.
    /// </summary>
    public interface IKeyStorage
    {
        /// <summary>
        /// Stores the private key (that has already been protected) to the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <param name="entry">The entry.</param>
        void Store(string alias, KeyEntry entry);

        /// <summary>
        /// Loads the private key associated with the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>
        /// The requested private key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        KeyEntry Load(string alias);

        /// <summary>
        /// Checks if the private key exists in this storage by given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>true if the private key exists, false otherwise</returns>
        bool Exists(string alias);

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        void Delete(string alias);
    }
}