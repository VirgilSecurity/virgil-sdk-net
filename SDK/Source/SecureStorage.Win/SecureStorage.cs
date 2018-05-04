using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Virgil.SDK
{

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
