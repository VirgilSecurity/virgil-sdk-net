using Java.Security;
using Javax.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;
using Virgil.CryptoApi.Storage.Exceptions;
using Virgil.SDK.Temp.Storage;
using static Java.Security.KeyStore;

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
            catch (Exception)
            {
                // store doesn't exist, create it
                keyStorage.Load(null, this.password);
            }
        }

        public void Delete(string key)
        {
            if (!this.Exists(key))
            {
                throw new KeyNotFoundSecureStorageException(key);
            }
            this.keyStorage.DeleteEntry(key);
        }

        public bool Exists(string key)
        {
            return this.keyStorage.ContainsAlias(key);
        }

        public byte[] Load(string key)
        {
            if (this.Exists(key))
            {
                    try
                    {
                    var keyEntry = keyStorage.GetEntry(key, new KeyStore.PasswordProtection(this.password));
                    return ((KeyEntry)keyEntry).GetEncoded();
                    }
                    catch (CryptographicException)
                    {
                        throw new SecureStorageException("Wrong password.");
                    }

                
            }
            throw new KeyNotFoundSecureStorageException(key);
        }

        public string[] Keys()
        {
            var aliasesJv = this.keyStorage.Aliases();
            var aliases = new List<string>();
            while (true)
            {
                var el = aliasesJv.NextElement();
                if (el != null)
                {
                    aliases.Add(el.ToString());
                }
                else
                {
                    break;
                }
            }
            return aliases.ToArray();
        }


        public void Save(string key, byte[] data)
        {
            //todo validate
            if (this.Exists(key))
            {
                throw new DuplicateKeySecureStorageException(key);
            }

            var keyEntry = new KeyStore.SecretKeyEntry(new KeyEntry(data));
            keyStorage.SetEntry(key, keyEntry, new KeyStore.PasswordProtection(this.password));

            using (var stream = new IsolatedStorageFileStream(StorageIdentity, FileMode.OpenOrCreate, FileAccess.Write))
            {
                keyStorage.Store(stream, this.password );
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
