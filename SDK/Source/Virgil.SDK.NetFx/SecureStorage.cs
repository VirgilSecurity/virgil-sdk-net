using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Virgil.CryptoApi.Storage.Exceptions;
using Virgil.SDK.Temp.Storage;

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

        public void Delete(string key)
        {
            if (!this.Exists(key))
            {
                throw new KeyNotFoundSecureStorageException(key);
            }
            this.appStorage.DeleteFile(this.FilePath(key));
        }

        public bool Exists(string key)
        {
            return this.appStorage.FileExists(this.FilePath(key));
        }

        public byte[] Load(string key)
        {
            if (this.Exists(key))
            {
                using (var stream = this.appStorage.OpenFile(this.FilePath(key),
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
            throw new KeyNotFoundSecureStorageException(key);
        }

        public string[] Keys()
        {
            //all filenames at the root of app storage
            var fileNames = this.appStorage.GetFileNames($"{StorageIdentity}\\*");
            //all keys
            return fileNames.Select(x => Encoding.UTF8.GetString(Convert.FromBase64String(x))).ToArray();
        }


        public void Save(string key, byte[] data)
        {
            //todo validate
            if (this.Exists(key))
            {
                throw new DuplicateKeySecureStorageException(key);
            }
            var encryptedData = ProtectedData.Protect(data, this.password, DataProtectionScope.CurrentUser);

            if (!this.appStorage.DirectoryExists(StorageIdentity))
            {
               this.appStorage.CreateDirectory(StorageIdentity); 
            }
            // save encrypted bytes to file
            using (var stream = this.appStorage.CreateFile(this.FilePath(key)))
            {
                stream.Write(encryptedData, 0, encryptedData.Length);
                stream.Close();
            }
        }

        private string FilePath(string key)
        {
            var keyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            return $"{StorageIdentity}\\{keyBase64}";
        }



    }
}
