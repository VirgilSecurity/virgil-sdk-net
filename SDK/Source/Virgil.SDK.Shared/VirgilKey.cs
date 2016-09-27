#region Copyright (C) 2016 Virgil Security Inc.
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

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Newtonsoft.Json;

    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Cryptography;

    /// <summary>
    /// The <see cref="VirgilKey"/> object represents an opaque reference to keying material 
    /// that is managed by the user agent.
    /// </summary>
    public sealed partial class VirgilKey
    {
        /// <summary>
        /// Gets or sets the name of the key.
        /// </summary>
        private string KeyName { get; set; }

        /// <summary>
        /// Gets or sets the private key.
        /// </summary>
        private KeyPair KeyPair { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        private VirgilKey()
        {
        }

        public static VirgilKey Create(string keyName, string password = null)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));
            }

            var keyStorage = ServiceLocator.Resolve<IKeyStore>();
            var crypto = ServiceLocator.Resolve<VirgilCrypto>();

            if (keyStorage.Exists(keyName))
            {
                throw new VirgilKeyIsAlreadyExistsException();
            }

            var privateKey = crypto.GenerateKeys();
            var virgilKey = new VirgilKey
            {
                KeyName = keyName,
                KeyPair = privateKey
            };

            var keyData = Encoding.UTF8
                .GetBytes(JsonConvert.SerializeObject(privateKey));

            var entry = new KeyEntry
            {
                Id = virgilKey.KeyName,
                Value = keyData,
                MetaData = new Dictionary<string, string>
                {
                    { "Type", privateKey.GetType().ToString() }
                }
            };

            keyStorage.Store(entry);
            
            return virgilKey;
        }
        
        public static VirgilKey Load(string keyName, string password = null)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));
            }

            var keyStorage = ServiceLocator.Resolve<IKeyStore>();

            if (!keyStorage.Exists(keyName))
            {
                throw new VirgilKeyIsNotFoundException();
            }

            var entry = keyStorage.Load(keyName);
            var privateKeyType = Type.GetType(entry.MetaData["Type"]);
            var keyData = Encoding.UTF8.GetString(entry.Value);

            var privateKey = (KeyPair)JsonConvert.DeserializeObject(keyData, privateKeyType);

            var key = new VirgilKey
            {
                KeyName = keyName,
                KeyPair = privateKey
            };

            return key;
        }
        
        /// <summary>
        /// Exports the <see cref="VirgilKey"/> to default Virgil Security format.
        /// </summary>
        public byte[] Export()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a digital signature for specified data using current <see cref="VirgilKey"/>.
        /// </summary>
        /// <param name="data">The data for which the digital signature will be generated.</param>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public byte[] Sign(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var crypto = ServiceLocator.Resolve<VirgilCrypto>();
            var signature = crypto.Sign(data, this.KeyPair.PrivateKey);

            return signature;
        }

        /// <summary>
        /// Decrypts the specified cipherdata using <see cref="VirgilKey"/>.
        /// </summary>
        /// <param name="cipherData">The encrypted data.</param>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public byte[] Decrypt(byte[] cipherData)
        {
            if (cipherData == null)
                throw new ArgumentNullException(nameof(cipherData));

            var crypto = ServiceLocator.Resolve<VirgilCrypto>();
            var data = crypto.Decrypt(cipherData, this.KeyPair.PrivateKey);
            
            return data;
        }
    }
}