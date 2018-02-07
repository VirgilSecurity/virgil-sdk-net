/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

using Virgil.SDK.Storage.Virgil.SDK.Exceptions;

namespace Virgil.SDK.Storage
{
    public class KeyStorageReaderV4
    {
        /// <summary>
        /// The <see cref="KeyStorageReaderV4"/> provides an opportunity to load keys 
        /// which were saved in previous storage version from SDK v4
        /// </summary>

        private readonly string keysPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyStorageReaderV4"/> class.
        /// </summary>
        public KeyStorageReaderV4(string keysFolderPath, bool isAbsolutePath = true)
            {
                if (isAbsolutePath)
                {
                    this.keysPath = keysFolderPath;
                }
                else
                {
                    var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    this.keysPath = Path.Combine(appData, "VirgilSecurity", keysFolderPath);
                }
            }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyStorageReaderV4"/> class.
        /// </summary>
        public KeyStorageReaderV4()
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                this.keysPath = Path.Combine(appData, "VirgilSecurity", "Keys");
            }


        /// <summary>
        /// Loads the key associated with the given alias.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <returns>
        /// The requested key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        public KeyEntry Load(string keyName)
            {
            //todo make import with crypto(UseSHA256Fingerprints) and password
            /*
              var exportedPrivateKey = this.context.Crypto.ExportPrivateKey(this.privateKey, password);
            var keyEntry = new KeyEntry
            {
                Name = keyName,
                Value = exportedPrivateKey
            };

             */
      /*      if (!this.Exists(keyName))
                {
                    throw new KeyNotFoundException();
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

            public string[] Names()
            {
                if (Directory.Exists(this.keysPath))
                {
                    return Directory.GetFiles(this.keysPath).Select(f => Path.GetFileName(f)).ToArray();
                }
                else
                {
                    return new string[] { };
                }
            }

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Delete(string keyName)
        {
            if (!this.Exists(keyName))
                throw new KeyNotFoundException();

            File.Delete(this.GetKeyPairPath(keyName));
        }

        
        private string GetKeyPairPath(string alias)
            {
                return Path.Combine(this.keysPath, alias.ToLower());
            }
        
    }

    /// <summary>
    /// Represents a key pair storage entry.
    /// </summary>
    public class KeyEntry
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the key pair.
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// Gets or sets the meta data associated with key pair.
        /// </summary>
        public IDictionary<string, string> MetaData { get; set; }
    }
}
*/