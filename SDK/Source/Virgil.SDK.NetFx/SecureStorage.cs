using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Virgil.SDK.Storage.Exceptions;
using Virgil.SDK.Storage;

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

        public void Delete(string alias)
        {
            if (!this.Exists(alias))
            {
                throw new KeyNotFoundSecureStorageException(alias);
            }
            this.appStorage.DeleteFile(this.FilePath(alias));
        }

        public bool Exists(string alias)
        {
            return this.appStorage.FileExists(this.FilePath(alias));
        }

        public byte[] Load(string alias)
        {
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
            throw new KeyNotFoundSecureStorageException(alias);
        }

        public string[] Aliases()
        {
            //all filenames at the root of app storage
            var fileNames = this.appStorage.GetFileNames($"{StorageIdentity}\\*");
            //all keys
            return fileNames.Select(x => Encoding.UTF8.GetString(Convert.FromBase64String(x))).ToArray();
        }


        public void Save(string alias, byte[] data)
        {
            //todo validate
            if (this.Exists(alias))
            {
                throw new DuplicateKeySecureStorageException(alias);
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

        private string FilePath(string key)
        {
            var keyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            return $"{StorageIdentity}\\{keyBase64}";
        }
    }
}
