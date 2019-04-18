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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Virgil.SDK
{
    /// <summary>
    /// The <see cref="KeyStorage"/> provides protected storage using the user 
    /// credentials to encrypt or decrypt keys.
    /// </summary>
    public class KeyStorage
    {
        private readonly SecureStorage coreStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyStorage"/> class.
        /// </summary>
#if (__IOS__ || __MACOS__)
        public KeyStorage()
        {
            coreStorage = new SecureStorage();
        }
#else
        public KeyStorage(string password)
        {
            //implementation for __WINDOWS__ || __ANDROID__ || Linux
            coreStorage = new SecureStorage(password);
        }
#endif


        /// <summary>
        /// Stores the specified instance of <see cref="KeyEntry"/>.
        /// </summary>
        /// <param name="entry">The instance of <see cref="KeyEntry"/>.</param>
        /// <exception cref="DuplicateKeyException"></exception>
        public void Store(KeyEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            var keyEntryData = Encoding.UTF8.GetBytes(
                 JsonConvert.SerializeObject(new KeyEntryData() { Meta = entry.Meta, Value = entry.Value }));
            coreStorage.Save(entry.Name, keyEntryData);
        }

        /// <summary>
        /// Loads the instance of <see cref="KeyEntry"/> associated with the given alias.
        /// </summary>
        /// <param name="name">The alias name.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <returns>
        /// The requested key, or null if the given alias does not exist or does 
        /// not identify a key-related entry.
        /// </returns>
        public KeyEntry Load(string name)
        {
            var keyEntryData = coreStorage.Load(name);
            var keyEntryJson = Encoding.UTF8.GetString(keyEntryData, 0, keyEntryData.Length);
            var keyEntryObject = JsonConvert.DeserializeObject<KeyEntryData>(keyEntryJson);

            return new KeyEntry
            {
                Name = name,
                Value = keyEntryObject.Value,
                Meta = keyEntryObject.Meta
            };
        }

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="name">The alias name.</param>
        public bool Exists(string name)
        {
            return coreStorage.Exists(name);
        }

        /// <summary>
        /// Delete the instance of <see cref="KeyEntry"/> by the given name.
        /// </summary>
        /// <param name="name">The alias name.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Delete(string name)
        {
            coreStorage.Delete(name);
        }

        /// <summary>
        /// Returns the list of aliases that are kept in the storage.
        /// </summary>
        public string[] Names()
        {
            return coreStorage.Aliases();
        }

    }

    internal class KeyEntryData
    {
        [DataMember(Name = "value")]
        public byte[] Value { get; set; }

        [DataMember(Name = "meta_data")]
        public IDictionary<string, string> Meta { get; set; }
    }
}
