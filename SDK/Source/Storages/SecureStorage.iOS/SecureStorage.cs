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

namespace Virgil.SDK
{
    using System;
    using Foundation;
    using Security;

    /// <summary>
    /// This class implements a secure storage for cryptographic keys.
    /// </summary>
    public class SecureStorage
    {
        /// <summary>
        /// Storage identity
        /// </summary>
        public static string StorageIdentity = "Virgil.SecureStorage";

        /// <summary>
        /// The partition allows you keep data in different groups. 
        /// For example group data by username.
        /// </summary>
        public readonly string Partition;

        /// <summary>
        /// Constructor
        /// </summary>
        public SecureStorage(string partition = null)
        {
            if (string.IsNullOrWhiteSpace(StorageIdentity))
            {
                throw new SecureStorageException("StorageIdentity can't be empty");
            }
            if (!string.IsNullOrWhiteSpace(partition))
            {
                this.Partition = partition.Trim();
            }
        }

        /// <summary>
        /// Stores the key data to the given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="data">The key data.</param>
        /// <exception cref="DuplicateKeyException"></exception>
        public void Save(string alias, byte[] data)
        {
            this.ValidateAlias(alias);
            this.ValidateData(data);

            if (this.Exists(alias))
            {
                throw new DuplicateKeyException(alias);
            }
            var record = NewSecRecord(alias, data);
            var result = SecKeyChain.Add(record);
            if (result != SecStatusCode.Success)
            {
                throw new SecureStorageException($"The key under alias '{alias}' can't be saved.");
            };
        }

        /// <summary>
        /// Checks if the key data exists in this storage by given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>true if the key data exists, false otherwise</returns>
        public bool Exists(string alias)
        {
            this.ValidateAlias(alias);

            return (this.FindRecord(alias).Item1 == SecStatusCode.Success);
        }

        /// <summary>
        /// Loads the key data associated with the given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>
        /// The requested data, or exception if the given key does not exist.
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public byte[] Load(string alias)
        {
            this.ValidateAlias(alias);

            (var secStatusCode, var foundSecRecord) = this.FindRecord(alias);
            if (secStatusCode == SecStatusCode.Success)
            {
                return foundSecRecord.ValueData.ToArray();
            }

            throw new KeyNotFoundException(alias);
        }

        /// <summary>
        /// Delete key data by the alias in this storage.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Delete(string alias)
        {
            this.ValidateAlias(alias);

            (var secStatusCode, var foundSecRecord) = this.FindRecord(alias);
            if (secStatusCode != SecStatusCode.Success)
            {
                throw new KeyNotFoundException(alias);
            }
            var secRecord = NewSecRecord(alias, null);
            SecKeyChain.Remove(secRecord);
        }

        /// <summary>
        /// Returns the list of aliases
        /// </summary>
        public string[] Aliases()
        {
            // all labels at the Virgil storage
            var secRecord = new SecRecord(SecKind.GenericPassword) { Service = ServiceName() };
            var foundSecRecords = SecKeyChain.QueryAsRecord(secRecord, 100, out var secStatusCode);
            var aliases = new string[foundSecRecords.Length];

            for (var i = 0; i < foundSecRecords.Length; i++)
            {
                aliases[i] = foundSecRecords[i].Label;
            }
            return aliases;
        }

        private Tuple<SecStatusCode, SecRecord> FindRecord(string alias)
        {
            var secRecord = NewSecRecord(alias, null);
            var found = SecKeyChain.QueryAsRecord(secRecord, out var secStatusCode);
            return new Tuple<SecStatusCode, SecRecord>(secStatusCode, found);
        }

        private string ServiceName()
        {
            return Partition != null ? $"{StorageIdentity}_{Partition}" : StorageIdentity;
        }

        private SecRecord NewSecRecord(string alias, byte[] data)
        {
            var secRecord = new SecRecord(SecKind.GenericPassword)
            {
                Account = alias,
                Service = ServiceName(),
                Label = alias
            };
            if (data != null && data.Length > 0)
            {
                secRecord.ValueData = NSData.FromArray(data);
            }
            return secRecord;
        }

        private void ValidateAlias(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException($"{nameof(alias)} can't be empty.");
            }
        }

        private void ValidateData(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentException($"{nameof(data)} can't be empty.");
            }
        }
    }
}
