namespace Virgil.SDK
{
    using System;

    /// <summary>
    /// The <see cref="VirgilKey"/> object represents an opaque reference to keying material 
    /// that is managed by the user agent.
    /// </summary>
    public sealed class VirgilKey
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        private VirgilKey()
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilKey"/> object that represents a new named key, 
        /// using default key storage provider.
        /// </summary>
        /// <param name="keyName">The name of the key.</param>
        /// <returns>A newly created key.</returns>
        public static VirgilKey Create(string keyName)
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Create(string keyName, byte[] publicKey, byte[] privateKey)
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Import(VirgilBuffer key)
        {
            throw new NotImplementedException();
        }

        public static bool Exists(string keyName)
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Load(string keyName)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Export()
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer GetDecrypted(VirgilBuffer cipherData)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Sign(VirgilBuffer buffer)
        {
            throw new NotImplementedException();
        }
    }
}