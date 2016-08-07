namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// This interface describes a storage facility for cryptographic keys.
    /// </summary>
    public interface IPrivateKeyStorage
    {
        /// <summary>
        /// Stores the private key (that has already been protected) to the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <param name="privateKey">The private key.</param>
        void Store(string alias, PrivateKey privateKey);

        /// <summary>
        /// Loads the private key associated with the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>
        /// The requested private key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        PrivateKey Load(string alias);

        /// <summary>
        /// Checks if the private key exists in this storage by given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>true if the private key exists, false otherwise</returns>
        bool Exists(string alias);

        /// <summary>
        /// Checks if the given alias exists in this storage.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        void Delete(string alias);
    }
}