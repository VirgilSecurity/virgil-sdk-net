namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// This class provides a storage facility for cryptographic keys.
    /// </summary>
    public class VirgilKeyStorage : IKeyStorage
    {
        /// <summary>
        /// Stores the private key (that has already been protected) to the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <param name="entry">The private key.</param>
        public void Store(string alias, KeyPairEntry entry)
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
        public KeyPairEntry Load(string alias)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="alias">The alias name.</param>
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