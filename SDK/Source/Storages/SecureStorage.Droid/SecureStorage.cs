#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2018 Virgil Security Inc.
// 
// Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions 
// are met:
// 
//   (1) Redistributions of source code must retain the above copyright
//   notice, this list of conditions and the following disclaimer.
//   
//   (2) Redistributions in binary form must reproduce the above copyright
//   notice, this list of conditions and the following disclaimer in
//   the documentation and/or other materials provided with the
//   distribution.
//   
//   (3) Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived 
//   from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
#endregion

namespace Virgil.SDK
{
    using Java.Security;
    using Javax.Crypto;
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using Java.Util;

    /// <summary>
    /// This class implements a secure storage for cryptographic keys.
    /// </summary>
    internal class SecureStorage
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
