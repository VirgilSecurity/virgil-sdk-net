#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2016 Virgil Security Inc.
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
    /// The <see cref="DefaultKeyStorage"/> provides protected storage using the user 
    /// credentials to encrypt or decrypt keys.
    /// </summary>
    public class DefaultKeyStorage : IKeyStorage
    {
        private readonly string keysPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultKeyStorage"/> class.
        /// </summary>
        public DefaultKeyStorage(string keysFolderPath)
        {
            this.keysPath = keysFolderPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultKeyStorage"/> class.
        /// </summary>
        public DefaultKeyStorage()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            this.keysPath = Path.Combine(appData, "VirgilSecurity", "Keys");
        }

        /// <summary>
        /// Stores the key to the given alias.
        /// </summary>
        /// <param name="entry">The key entry.</param>
        /// <exception cref="KeyEntryAlreadyExistsException"></exception>
        public void Store(KeyEntry entry)
        {
            Directory.CreateDirectory(this.keysPath);

            if (this.Exists(entry.Name))
            {
                throw new KeyEntryAlreadyExistsException();
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
        /// Loads the key associated with the given alias.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <exception cref="KeyEntryNotFoundException"></exception>
        /// <returns>
        /// The requested key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        public KeyEntry Load(string keyName)
        {
            if (!this.Exists(keyName))
            {
                throw new KeyEntryNotFoundException();
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
        /// <exception cref="KeyEntryNotFoundException"></exception>
        public void Delete(string keyName)
        {
            if (!this.Exists(keyName))
                throw new KeyEntryNotFoundException();
          
            File.Delete(this.GetKeyPairPath(keyName));
        }

        private string GetKeyPairPath(string alias)
        {
            return Path.Combine(this.keysPath, alias.ToLower());
        }
    }
}