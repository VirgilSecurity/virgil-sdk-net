namespace Virgil.SDK
{
    using System;

    /// <summary>
    /// Represents a Private Key provides key storage functionality and base cryptographic 
    /// operaations such as signatures and encryption.
    /// </summary>
    public class VirgilKey
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        private VirgilKey()
        {
        }
        
        public VirgilBuffer Export()
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Create()
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Create(IVirgilKeyStorageProvider storageProvider)
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Open()
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Generate()
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Import()
        {
            throw new NotImplementedException();
        }
    }

    public interface IVirgilKeyStorageProvider
    {
    }
}