namespace Virgil.SDK.Storage
{
    /// <summary>
    /// This interface describes a storage facility for cryptographic keys.
    /// </summary>
    public interface ISecureStorage
        {
        /// <summary>
        /// Stores the data to the given key.
        /// </summary>
        /// <param name="data">The key.</param>
        /// <exception cref="DuplicateKeySecureStorageException"></exception>
        void Save(string key, byte[] data);

            /// <summary>
            /// Loads the data associated with the given key.
            /// </summary>
            /// <param name="key">The name.</param>
            /// <returns>
            /// The requested data, or exception if the given key does not exist.
            /// </returns>
            /// <exception cref="KeyNotFoundSecureStorageException"></exception>
            byte[] Load(string key);

            /// <summary>
            /// Checks if the data exists in this storage by given key.
            /// </summary>
            /// <param name="key">The alias name.</param>
            /// <returns>true if the data exists, false otherwise</returns>
            bool Exists(string key);

            /// <summary>
            /// Delete data by the key in this storage.
            /// </summary>
            /// <param name="key">The alias name.</param>
            /// <exception cref="KeyNotFoundSecureStorageException"></exception>
            void Delete(string key);

            /// <summary>
            /// Returns the list of keys
            /// </summary>
            string[] Keys();
        }
}
