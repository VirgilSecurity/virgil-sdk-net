using System;
using Foundation;
using Security;
using Virgil.SDK.Storage;
using Virgil.SDK.Storage.Exceptions;

namespace Virgil.SDK
{

    public class SecureStorage : ISecureStorage
    {
        /// <summary>
        /// Name of the storage dir.
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

        public void Delete(string alias)
        {
            (var secStatusCode, var foundSecRecord) = this.FindRecord(alias);
            if (secStatusCode != SecStatusCode.Success)
            {
                throw new KeyNotFoundSecureStorageException(alias);
            }
            var secRecord = NewSecRecord(alias, null);
            SecKeyChain.Remove(secRecord);
        }

        public bool Exists(string alias)
        {
            return (this.FindRecord(alias).Item1 == SecStatusCode.Success);
        }


        public byte[] Load(string alias)
        {
            (var secStatusCode, var foundSecRecord) = this.FindRecord(alias);
            if (secStatusCode == SecStatusCode.Success)
            {
                return foundSecRecord.ValueData.ToArray();
            }

            throw new KeyNotFoundSecureStorageException(alias);
        }

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



        public void Save(string alias, byte[] data)
        {
            //todo validate
            if (this.Exists(alias))
            {
                throw new DuplicateKeySecureStorageException(alias);
            }
            var record = NewSecRecord(alias, data);
            var result = SecKeyChain.Add(record);
            if (result != SecStatusCode.Success)
            {
                throw new SecureStorageException($"The key under alias '{alias}' can't be saved.");
            };
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
            if (data != null && data.Length > 0){
                secRecord.ValueData = NSData.FromArray(data);
            }
            return secRecord;
        }
    }
}
