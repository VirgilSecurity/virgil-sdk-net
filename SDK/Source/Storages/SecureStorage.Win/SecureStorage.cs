#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2019 Virgil Security Inc.
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
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class SecureStorage
    {
        /// <summary>
        /// Storage identity
        /// </summary>
        public static string StorageIdentity = "Virgil.SecureStorage";

        /// <summary>
        /// User-scoped isolated storage 
        /// </summary>
        private readonly IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForAssembly();

        /// <summary>
        /// Password for storage
        /// </summary>
        private byte[] password;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="password">Password for storage</param>
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
            this.password = Encoding.UTF8.GetBytes(password);
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
            var encryptedData = ProtectedData.Protect(data, this.password, DataProtectionScope.CurrentUser);

            if (!this.appStorage.DirectoryExists(StorageIdentity))
            {
                this.appStorage.CreateDirectory(StorageIdentity);
            }

            try
            {
                // save encrypted bytes to file
                using (var stream = this.appStorage.CreateFile(this.FilePath(alias)))
                {
                    stream.Write(encryptedData, 0, encryptedData.Length);
                    stream.Close();
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

            return this.appStorage.FileExists(this.FilePath(alias));
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
                using (var stream = this.appStorage.OpenFile(this.FilePath(alias),
                    FileMode.Open,
                    FileAccess.ReadWrite))
                {
                    // allocate and read the protected data
                    var protectedData = new byte[stream.Length];
                    stream.Read(protectedData, 0, (int)stream.Length);

                    try
                    {
                        // obtain clear data by decrypting
                        return ProtectedData.Unprotect(protectedData, this.password,
                            DataProtectionScope.CurrentUser);
                    }
                    catch (CryptographicException)
                    {
                        throw new SecureStorageException("Wrong password.");
                    }
                }
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
            this.appStorage.DeleteFile(this.FilePath(alias));
        }

        /// <summary>
        /// Returns the list of aliases that are kept in the storage.
        /// </summary>
        public string[] Aliases()
        {
            //all filenames at the root of app storage
            var fileNames = this.appStorage.GetFileNames($"{StorageIdentity}\\*");
            //all keys
            return fileNames.Select(x => Encoding.UTF8.GetString(Convert.FromBase64String(x))).ToArray();
        }

        private string FilePath(string key)
        {
            var keyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            return $"{StorageIdentity}\\{keyBase64}";
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
    }
}
