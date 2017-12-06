namespace Virgil.SDK.Storage
{
    /// <summary>
    /// This interface describes a storage facility for cryptographic keys.
    /// </summary>
    public interface ISecureStorage
        {
        /// <summary>
        /// Stores the key data to the given alias.
        /// </summary>
        /// <param name="data">The key data.</param>
        /// <exception cref="DuplicateKeySecureStorageException"></exception>
        void Save(string alias, byte[] data);

            /// <summary>
            /// Loads the key data associated with the given alias.
            /// </summary>
            /// <param name="alias">The alias.</param>
            /// <returns>
            /// The requested data, or exception if the given key does not exist.
            /// </returns>
            /// <exception cref="KeyNotFoundSecureStorageException"></exception>
            byte[] Load(string alias);

            /// <summary>
            /// Checks if the key data exists in this storage by given alias.
            /// </summary>
            /// <param name="alias">The alias.</param>
            /// <returns>true if the key data exists, false otherwise</returns>
            bool Exists(string alias);

            /// <summary>
            /// Delete data by the key in this storage.
            /// </summary>
            /// <param name="alias">The alias.</param>
            /// <exception cref="KeyNotFoundSecureStorageException"></exception>
            void Delete(string alias);

            /// <summary>
            /// Returns the list of aliases
            /// </summary>
            string[] Aliases();
        }
}
