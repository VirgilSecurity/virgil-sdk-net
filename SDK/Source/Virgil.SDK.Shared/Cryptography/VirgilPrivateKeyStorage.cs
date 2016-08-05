namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// This class represents a storage facility for cryptographic keys.
    /// </summary>
    public sealed class VirgilPrivateKeyStorage : IPrivateKeyStorage
    {
        /// <summary>
        /// Stores the private key (that has already been protected) to the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <param name="privateKey">The private key.</param>
        public void Store(string alias, PrivateKey privateKey)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Loads the private key associated with the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>
        /// The requested private key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        public PrivateKey Load(string alias)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if the private key exists in this storage by given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>true if the private key exists, false otherwise</returns>
        public bool Exists(string alias)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        public void Delete(string alias)
        {
            throw new System.NotImplementedException();
        }
    }
}