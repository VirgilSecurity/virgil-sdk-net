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

namespace Virgil.SDK
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using Newtonsoft.Json;
    // using Foundation;
    // using Security;
    using Virgil.SDK;

    /// <summary>
    /// This class implements a secure storage for cryptographic keys.
    /// </summary>
    public class SecureStorage
    {
        /// <summary>
        /// Storage identity
        /// </summary>
        public static string StorageIdentity = "Virgil.SecureStorage";

        private byte[] ServiceNameBytes;



        /// <summary>
        /// Constructor
        /// </summary>
        public SecureStorage()
        {
            if (string.IsNullOrWhiteSpace(StorageIdentity))
            {
                throw new SecureStorageException("StorageIdentity can't be empty");
            }
            this.ServiceNameBytes = Encoding.UTF8.GetBytes(StorageIdentity);
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

            var status = SecKeychainAddGenericPassword(alias, data);
            if (status != OSStatus.Ok)
            {
                throw new SecureStorageException($"The key under alias '{alias}' can't be saved.");
            }

           //todo UpdateAliaseList(alias);

        }

        private void UpdateAliaseList(string alias)
        {
            OSStatus status;
            var found = FindKeyChainItem("aliaseList");
            var aliases = new string[] { alias };
            if (found.Item1 == OSStatus.Ok)
            {
                var kept = JsonConvert.DeserializeObject<string[]>(
                   Encoding.UTF8.GetString(found.Item2));

                Array.Copy(kept, aliases, kept.Length);
                Delete("aliaseList");
            }

            status = SecKeychainAddGenericPassword(
                    StorageIdentity, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(aliases))
                );
            if (status != OSStatus.Ok)
            {
                throw new SecureStorageException($"Can't update aliases list");
            }

        }

        private OSStatus SecKeychainAddGenericPassword(string alias, byte[] data)
        {
            var item = IntPtr.Zero;
            var aliasBytes = Encoding.UTF8.GetBytes(alias);
            var status = Keychain.SecKeychainAddGenericPassword(IntPtr.Zero, (uint)ServiceNameBytes.Length,
                                                   ServiceNameBytes,
                                                   (uint)aliasBytes.Length,
                                                   aliasBytes,
                                                   (uint)data.Length,
                                                   data,
                                                   ref item);
            return status;
        }

        /// <summary>
        /// Checks if the key data exists in this storage by given alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>true if the key data exists, false otherwise</returns>
        public bool Exists(string alias)
        {
            this.ValidateAlias(alias);

            var found = FindKeyChainItem(alias);
            return (found.Item1 == OSStatus.Ok);
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

            var found = FindKeyChainItem(alias);
            if (found.Item1 == OSStatus.Ok){
                return found.Item2;
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

            IntPtr dataPtr;
            var aliasBytes = Encoding.UTF8.GetBytes(alias);
            uint dataLength;
            IntPtr keyChainItem;
            var findStatus = SecKeychainFindGenericPassword(out dataPtr, 
                                                            aliasBytes, 
                                                            out dataLength, 
                                                            out keyChainItem);

            if (findStatus != OSStatus.Ok){
                throw new KeyNotFoundException(alias);
            }
            var deleteStatus = Keychain.SecKeychainItemDelete(keyChainItem);

            if (deleteStatus != OSStatus.Ok)
            {
                throw new KeyNotFoundException(alias);
            }
        }

        private OSStatus SecKeychainFindGenericPassword(out IntPtr dataPtr,
                                                    byte[] aliasBytes, 
                                                    out uint dataLength, 
                                                    out IntPtr keyChainItem)
        {
            keyChainItem = IntPtr.Zero;
            return Keychain.SecKeychainFindGenericPassword(
                IntPtr.Zero,
                (uint)ServiceNameBytes.Length,
                ServiceNameBytes,
                (uint)aliasBytes.Length,
                aliasBytes,
                out dataLength,
                out dataPtr,
                ref keyChainItem);
        }

        /// <summary>
        /// Returns the list of aliases
        /// </summary>
        public string[] Aliases()
        {
            // all labels at the Virgil storage
            //todo
            return new string[] { };
        }

        private Tuple<OSStatus, byte[]> FindKeyChainItem(string alias)
        {
            IntPtr dataPtr;
            var aliasBytes = Encoding.UTF8.GetBytes(alias);
            uint dataLength;
            var keyChainItem = IntPtr.Zero;

            var status = SecKeychainFindGenericPassword(out dataPtr, aliasBytes, out dataLength, out keyChainItem);

            byte[] data = null;
            if (status == OSStatus.Ok && dataLength > 0)
            {
                data = new byte[dataLength];
                Marshal.Copy(dataPtr, data, 0, (int)dataLength);
            }
            return new Tuple<OSStatus, byte[]>(status, data);
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