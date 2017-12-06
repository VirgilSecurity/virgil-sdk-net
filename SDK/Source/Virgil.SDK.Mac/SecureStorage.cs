using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;
using MonoMac.Foundation;
using MonoMac.Security;
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

        public void Delete(string key)
        {
            var recordWithStatus = this.FindRecord(key);
            if (recordWithStatus.Item1 != SecStatusCode.Success)
            {
                throw new KeyNotFoundSecureStorageException(key);
            }
            var secRecord = new SecRecord(SecKind.GenericPassword)
            {
                Account = key,
                ApplicationLabel = StorageIdentity,
                ValueData = recordWithStatus.Item2.ValueData
            };
            SecKeyChain.Remove(secRecord);
        }

        public bool Exists(string key)
        {
            return (this.FindRecord(key).Item1 == SecStatusCode.Success);
        }

        private Tuple<SecStatusCode, SecRecord> FindRecord(string key)
        {
            var secRecord = new SecRecord(SecKind.GenericPassword) { Account = key, ApplicationLabel = StorageIdentity };
            var found = SecKeyChain.QueryAsRecord(secRecord, out var secStatusCode);
            return new Tuple<SecStatusCode, SecRecord>(secStatusCode, found);
        }

        public byte[] Load(string key)
        {
            var recordWithStatus = this.FindRecord(key);
            if (recordWithStatus.Item1 == SecStatusCode.Success)
            {
                return recordWithStatus.Item2.ValueData.ToArray();
            }

            throw new KeyNotFoundSecureStorageException(key);
        }

        public string[] Keys()
        {
            //all filenames at the root of app storage
            return new string[]{};
        }



        public void Save(string key, byte[] data)
        {
            //todo validate
            if (this.Exists(key))
            {
                throw new DuplicateKeySecureStorageException(key);
            }
            var record = new SecRecord(SecKind.GenericPassword)
            {
                Account = key,
                Service = "Virgil.SDK",
                Label = key,
                Description = "Item description",
                Comment = "Your comment here",
                ValueData = NSData.FromString("dddd", NSStringEncoding.UTF8),
                Generic = NSData.FromString("foo")
            };
            var result = SecKeyChain.Add(record);
            if (result != SecStatusCode.Success)
            {
                throw  new SecureStorageException($"The key {key} can't be saved.");
            };
        }

    }
}
