namespace Virgil.SDK.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    using Newtonsoft.Json;

    using Virgil.SDK.Exceptions;

    /// <summary>
    /// This class provides a storage facility for cryptographic keys.
    /// </summary>
    public class KeyStorage : IKeyStorage
    {
        private readonly string keysPath;
            
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyStorage"/> class.
        /// </summary>
        public KeyStorage()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            this.keysPath = Path.Combine(appData, "VirgilSecurity", "Keys");
        }

        /// <summary>
        /// Stores the private key (that has already been protected) to the given alias.
        /// </summary>
        /// <param name="entry">The private key.</param>
        public void Store(KeyEntry entry)
        {
            Directory.CreateDirectory(this.keysPath);

            if (this.Exists(entry.Name))
            {
                throw new KeyPairAlreadyExistsException();
            }
            
            var keyEntryJson = new
            {
                value = entry.Value,
                meta_data = entry.MetaData
            };

            var keyEntryData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(keyEntryJson));
            var keyEntryCipher = ProtectedData.Protect(keyEntryData, null, DataProtectionScope.CurrentUser);

            var keyPath = this.GetKeyPairPath(entry.Name);

            File.WriteAllBytes(keyPath, keyEntryCipher);
        }

        /// <summary>
        /// Loads the private key associated with the given alias.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <returns>
        /// The requested private key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        public KeyEntry Load(string keyName)
        {
            if (!this.Exists(keyName))
            {
                throw new KeyPairNotFoundException();
            }

            var keyEntryType = new
            {
                value = new byte[] { },
                meta_data = new Dictionary<string, string>()
            };

            var encryptedData = File.ReadAllBytes(this.GetKeyPairPath(keyName));
            var data = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            var keyEntryJson = Encoding.UTF8.GetString(data);

            var keyEntryObject = JsonConvert.DeserializeAnonymousType(keyEntryJson, keyEntryType);

            return new KeyEntry
            {
                Name = keyName,
                Value = keyEntryObject.value,
                MetaData = keyEntryObject.meta_data
            };
        }

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        public bool Exists(string keyName)
        {
            return File.Exists(this.GetKeyPairPath(keyName));
        }

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        public void Delete(string keyName)
        {
            if (this.Exists(keyName))
            {
                throw new KeyPairAlreadyExistsException();
            }

            File.Delete(this.GetKeyPairPath(keyName));
        }

        /// <summary>
        /// Gets the key pair path.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns></returns>
        private string GetKeyPairPath(string alias)
        {
            using (var hasher = SHA1.Create())
            {
                var data = Encoding.UTF8.GetBytes(alias.ToUpper());
                var name = BitConverter.ToString(hasher.ComputeHash(data)).Replace("-", "").ToLower();

                return Path.Combine(this.keysPath, name);
            }
        }
    }
}