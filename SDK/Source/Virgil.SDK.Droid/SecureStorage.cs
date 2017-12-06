using Java.Security;
using Javax.Crypto;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using Java.Util;
using Virgil.SDK.Storage;
using Virgil.SDK.Storage.Exceptions;

namespace Virgil.SDK
{

    public class SecureStorage : ISecureStorage
    {
        /// <summary>
        /// Name of the storage dir.
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

        public void Delete(string alias)
        {
            if (!this.Exists(alias))
            {
                throw new KeyNotFoundSecureStorageException(alias);
            }
            this.keyStorage.DeleteEntry(alias);
        }

        public bool Exists(string alias)
        {
            return this.keyStorage.ContainsAlias(alias);
        }

        public byte[] Load(string alias)
        {
            if (this.Exists(alias))
            {
                var keyEntry = keyStorage.GetEntry(alias, new KeyStore.PasswordProtection(this.password));
                return ((KeyStore.SecretKeyEntry)keyEntry).SecretKey.GetEncoded();
            }
            throw new KeyNotFoundSecureStorageException(alias);
        }

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


        public void Save(string alias, byte[] data)
        {
            //todo validate
            if (this.Exists(alias))
            {
                throw new DuplicateKeySecureStorageException(alias);
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
