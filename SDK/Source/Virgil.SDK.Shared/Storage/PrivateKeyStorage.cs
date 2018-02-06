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
using System.Text;
using Virgil.CryptoAPI;
using Virgil.SDK.Storage.Exceptions;

namespace Virgil.SDK.Storage
{
    public class PrivateKeyStorage
    {
        private readonly KeyStorage keyStorage;
        private readonly IPrivateKeyExporter privateKeyExporter;

        public PrivateKeyStorage(IPrivateKeyExporter keyExporter, string password = null)
        {
            privateKeyExporter = keyExporter;
            keyStorage = new KeyStorage(password);
        }

        /// <summary>
        /// Stores the key data to the given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="data">The key data.</param>
        /// <exception cref="DuplicateKeySecureStorageException"></exception>
        public void Store(IPrivateKey privateKey, string name, IDictionary<string, string> meta)
        {
            var keyEntry = new KeyEntry()
            {
                Meta = meta,
                Name = name,
                Value = privateKeyExporter.ExportPrivatekey(privateKey)
            };
            this.keyStorage.Store(keyEntry);
        }

        /// <summary>
        /// Checks if the key data exists in this storage by given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>true if the key data exists, false otherwise</returns>
        public bool Exists(string name)
        {
            return keyStorage.Exists(name);
        }

        /// <summary>
        /// Loads the key data associated with the given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>
        /// The requested data, or exception if the given key does not exist.
        /// </returns>
        /// <exception cref="KeyNotFoundSecureStorageException"></exception>
        public Tuple<byte[], IDictionary<string, string>> Load(string name)
        {
            var keyEntry = this.keyStorage.Load(name);
            return new Tuple<byte[], IDictionary<string, string>>(keyEntry.Value, keyEntry.Meta);
        }

        /// <summary>
        /// Delete key data by the alias in this storage.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <exception cref="KeyNotFoundSecureStorageException"></exception>
        public void Delete(string name)
        {
            this.keyStorage.Delete(name);
        }

        /// <summary>
        /// Returns the list of aliases
        /// </summary>
        public string[] Aliases()
        {
            return keyStorage.Names();
        }

    }
}
