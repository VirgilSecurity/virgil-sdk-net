namespace Virgil.SDK.Cryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    using Newtonsoft.Json;
    using Virgil.Crypto;
    using Virgil.Crypto.Foundation;
    using Virgil.SDK.Exceptions;

    /// <summary>
    /// This class provides a storage facility for cryptographic keys.
    /// </summary>
    public class VirgilKeyStorage : IKeyStorage
    {
        private readonly string keysPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyStorage"/> class.
        /// </summary>
        public VirgilKeyStorage()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            this.keysPath = Path.Combine(appData, "VirgilSecurity", "Keys");
        }

        /// <summary>
        /// Stores the private key (that has already been protected) to the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <param name="entry">The private key.</param>
        public void Store(string alias, KeyPairEntry entry)
        {
            Directory.CreateDirectory(this.keysPath);
            if (this.Exists(alias))
            {
                throw new KeyPairAlreadyExistsException();
            }
            
            var keyEntryJson = new
            {
                public_key = entry.PublicKey,
                private_key = entry.PrivateKey,
                meta_data = entry.Meta
            };

            var keyEntryData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(keyEntryJson));
            var keyEntryCipher = ProtectedData.Protect(keyEntryData, null, DataProtectionScope.CurrentUser);

            var keyPath = this.GetKeyPairPath(alias);

            File.WriteAllBytes(keyPath, keyEntryCipher);
        }

        /// <summary>
        /// Loads the private key associated with the given alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>
        /// The requested private key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        public KeyPairEntry Load(string alias)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        public bool Exists(string alias)
        {
            return File.Exists(this.GetKeyPairPath(alias));
        }

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        public void Delete(string alias)
        {
            if (this.Exists(alias))
            {
                throw new KeyPairAlreadyExistsException();
            }

            File.Delete(this.GetKeyPairPath(alias));
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