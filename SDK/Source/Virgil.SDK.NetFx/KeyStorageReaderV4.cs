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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Virgil.SDK
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
        /// Loads the key associated with the given alias from SDK v4 storage.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <returns>
        /// The requested key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        public Tuple<byte[], Dictionary<string, string>> Load(string keyName)
            {
            
            if (!this.Exists(keyName))
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

            return new Tuple<byte[], Dictionary<string, string>>(keyEntryObject.value, keyEntryObject.meta_data);
               
            }

        /// <summary>
        /// Checks if the key associated with the given alias exists in this SDK v4 keystore.
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
        /// Delete the key associated with the given alias from this SDK v4 keystore.
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
}
