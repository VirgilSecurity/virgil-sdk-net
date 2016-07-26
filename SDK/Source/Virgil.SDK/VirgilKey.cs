namespace Virgil.SDK
{
    using System;

    /// <summary>
    /// The <see cref="IVirgilKey"/> object represents an opaque reference to keying material 
    /// that is managed by the user agent.
    /// </summary>
    public class VirgilKey : IVirgilKey
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        private VirgilKey
        (
            string keyName,
            IStorageProvider storageProvider
        )
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="IVirgilKey"/> object that represents a new named key, 
        /// using default key storage provider.
        /// </summary>
        /// <param name="keyName">The name of the key.</param>
        /// <returns>A newly created key.</returns>
        public static IVirgilKey Create(string keyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an instance of <see cref="IVirgilKey"/> object that represents a new named key, 
        /// using the specified key storage provider.
        /// </summary>
        /// <param name="keyName">The name of the key.</param>
        /// <param name="storageProvider">The instance of keys storage provider.</param>
        /// <returns>An existing key.</returns>
        public static IVirgilKey Create(string keyName, IStorageProvider storageProvider)
        {
            throw new NotImplementedException();
        }

        public static IVirgilKey Create(string keyName, byte[] publicKey, byte[] privateKey)
        {
            throw new NotImplementedException();
        }

        public static IVirgilKey Import(VirgilBuffer key)
        {
            throw new NotImplementedException();
        }

        public static bool Exists(string keyName)
        {
            throw new NotImplementedException();
        }

        public static IVirgilKey Load(string keyName)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Export()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// The <see cref="IVirgilKey"/> object represents an opaque reference to keying material 
    /// that is managed by the user agent.
    /// </summary>
    public interface IVirgilKey
    {
    }

    public interface IStorageProvider
    {
    }
}