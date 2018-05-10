using Java.Security;
using Javax.Crypto;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using Java.Util;
using Virgil.SDK;

namespace Virgil.SDK
{

    /// <summary>
    /// This class implements a secure storage for cryptographic keys.
    /// </summary>
    public class SecureStorage
    {
        /// <summary>
        /// Storage identity
        /// </summary>
        public static string StorageIdentity = "Virgil.SecureStorage";

        /// <summary>
        /// User-scoped isolated storage 
        /// </summary>
        private readonly KeyStore keyStorage = KeyStore.GetInstance(KeyStore.DefaultType);

        /// <summary>
        /// Password for storage
        /// </summary>
        private char[] password;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="password">The password for storage.</param>
        public SecureStorage(string password)
        {
            if (string.IsNullOrWhiteSpace(StorageIdentity))
            {
                throw new SecureStorageException("StorageIdentity can't be empty");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new SecureStorageException("Password can't be empty");
            }
            this.password = password.ToCharArray();

            // if store exists, load it from the file
            try
            {
                using (var stream = new IsolatedStorageFileStream(StorageIdentity, FileMode.Open, FileAccess.Read))
                {
                    keyStorage.Load(stream, this.password);
                }
            }
            catch (Exception e)
            {
                var s = e.Message;
                // store doesn't exist, create it
                keyStorage.Load(null, this.password);
            }
        }

        /// <summary>
        /// Stores the key data to the given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="data">The key data.</param>
        /// <exception cref="DuplicateKeyException"></exception>
        public void Save(string alias, byte[] data)
        {
            this.ValidateAlias(alias);
            this.ValidateData(data);

            if (this.Exists(alias))
            {
                throw new DuplicateKeyException(alias);
            }

            var keyEntry = new KeyStore.SecretKeyEntry(new KeyEntry(data));
            keyStorage.SetEntry(alias, keyEntry, new KeyStore.PasswordProtection(this.password));

            try
            {
                using (var stream = new IsolatedStorageFileStream(StorageIdentity, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    keyStorage.Store(stream, this.password);
                }
            }
            catch (Exception)
            {
                throw new SecureStorageException($"The key under alias '{alias}' can't be saved.");
            }
        }

        /// <summary>
        /// Checks if the key data exists in this storage by given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>true if the key data exists, false otherwise</returns>
        public bool Exists(string alias)
        {
            this.ValidateAlias(alias);

            return this.keyStorage.ContainsAlias(alias);
        }

        /// <summary>
        /// Loads the key data associated with the given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>
        /// The requested data, or exception if the given key does not exist.
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public byte[] Load(string alias)
        {
            this.ValidateAlias(alias);

            if (this.Exists(alias))
            {
                var keyEntry = keyStorage.GetEntry(alias, new KeyStore.PasswordProtection(this.password));
                return ((KeyStore.SecretKeyEntry)keyEntry).SecretKey.GetEncoded();
            }
            throw new KeyNotFoundException(alias);
        }

        /// <summary>
        /// Delete key data by the alias in this storage.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Delete(string alias)
        {
            this.ValidateAlias(alias);

            if (!this.Exists(alias))
            {
                throw new KeyNotFoundException(alias);
            }
            this.keyStorage.DeleteEntry(alias);
        }

        /// <summary>
        /// Returns the list of aliases
        /// </summary>
        public string[] Aliases()
        {
            var aliasesJv = Collections.List(this.keyStorage.Aliases());
            var aliases = new string[aliasesJv.Count];
            for (var i = 0; i < aliasesJv.Count; i++)
            {
                aliases[i] = aliasesJv[i].ToString();
            }
            return aliases;
        }

        private void ValidateAlias(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException($"{nameof(alias)} can't be empty.");
            }
        }

        private void ValidateData(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentException($"{nameof(data)} can't be empty.");
            }
        }

        #region IKey implementation
        /// <summary>
        /// This implementation of ISecretKey is used for storing 
        /// raw bytes as a key with password protection in Java key store
        /// </summary>
        private class KeyEntry : Java.Lang.Object, ISecretKey
        {
            private byte[] bytes;

            public KeyEntry(byte[] rawKey)
            {
                this.bytes = rawKey;
            }
            public string Algorithm => "NONE";

            public string Format => "RAW";

            public byte[] GetEncoded()
            {
                return bytes;
            }
        }
        #endregion

    }

}
