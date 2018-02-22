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
using Virgil.CryptoAPI;

namespace Virgil.SDK.Storage
{
    public class PrivateKeyStorage
    {
        private readonly KeyStorage keyStorage;
        private readonly IPrivateKeyExporter privateKeyExporter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeyStorage" /> class.
        /// </summary>
        /// <param name="keyExporter">The instance of <see cref="IPrivateKeyExporter"/> that is 
        /// used for private key export/import.</param>
        /// <param name="password">Password for storage.</param>
        public PrivateKeyStorage(IPrivateKeyExporter keyExporter, string password = null)
        {
            privateKeyExporter = keyExporter;
            keyStorage = new KeyStorage(password);
        }

        /// <summary>
        /// Stores the private key to the given alias.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="meta">Some additional data.</param>
        /// <exception cref="DuplicateKeyException"></exception>
        public void Store(IPrivateKey privateKey, string alias, IDictionary<string, string> meta = null)
        {
            var keyEntry = new KeyEntry()
            {
                Meta = meta,
                Name = alias,
                Value = privateKeyExporter.ExportPrivatekey(privateKey)
            };
            this.keyStorage.Store(keyEntry);
        }

        /// <summary>
        /// Checks if the private key exists in this storage by given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>true if the key data exists, false otherwise</returns>
        public bool Exists(string alias)
        {
            return keyStorage.Exists(alias);
        }

        /// <summary>
        /// Loads the private key associated with the given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>
        /// The requested private key and its additional data, or exception if the given key does not exist.
        /// </returns>
        /// <exception cref="Exceptions.KeyNotFoundException"></exception>
        public Tuple<IPrivateKey, IDictionary<string, string>> Load(string alias)
        {
            var keyEntry = this.keyStorage.Load(alias);
            var key = privateKeyExporter.ImportPrivateKey(keyEntry.Value);
            return new Tuple<IPrivateKey, IDictionary<string, string>>(key, keyEntry.Meta);
        }

        /// <summary>
        /// Delete key and its additional data by the alias in this storage.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <exception cref="Exceptions.KeyNotFoundException"></exception>
        public void Delete(string alias)
        {
            this.keyStorage.Delete(alias);
        }

        /// <summary>
        /// Returns the list of aliases that are kept in the storage.
        /// </summary>
        public string[] Aliases()
        {
            return keyStorage.Names();
        }
    }
}
