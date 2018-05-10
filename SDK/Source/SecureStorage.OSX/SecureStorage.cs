using System;
using Foundation;
using Security;
using Virgil.SDK;

namespace Virgil.SDK
{
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
        /// Constructor
        /// </summary>
        public SecureStorage()
        {
            if (string.IsNullOrWhiteSpace(StorageIdentity))
            {
                throw new SecureStorageException("StorageIdentity can't be empty");
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
            var secRecord = new SecRecord(SecKind.GenericPassword) { Service = StorageIdentity };
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

        private SecRecord NewSecRecord(string alias, byte[] data)
        {
            var secRecord = new SecRecord(SecKind.GenericPassword)
            {
                Account = alias,
                Service = StorageIdentity,
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
