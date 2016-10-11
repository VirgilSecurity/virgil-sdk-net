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

namespace Virgil.SDK.HighLevel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Virgil.SDK.Client;
    using Virgil.SDK.Common;
    using Virgil.SDK.Storage;
    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Cryptography;
    
    public sealed partial class VirgilKey
    {
        /// <summary>
        /// Gets or sets the private key.
        /// </summary>
        private KeyPair KeyPair { get; set; }

        /// <summary>
        /// Gets or sets the name of the key.
        /// </summary>
        private string KeyName { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        private VirgilKey()
        {
        }

        /// <summary>
        /// Creates a new <see cref="VirgilKey"/> with custom Public/Private key pair. 
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="keyPair">The key pair.</param>
        /// <param name="password">The password.</param>
        /// <returns>The instance of <see cref="VirgilKey"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="VirgilKeyIsAlreadyExistsException"></exception>
        public static VirgilKey Create(string keyName, KeyPair keyPair, string password = null)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));

            if (keyPair == null)
                throw new ArgumentNullException(nameof(keyPair));

            var crypto = VirgilConfig.GetService<Crypto>();
            var storage = VirgilConfig.GetService<IKeyStorage>();

            if (storage.Exists(keyName))
                throw new VirgilKeyIsAlreadyExistsException();
            
            var virgilKey = new VirgilKey { KeyPair = keyPair, KeyName = keyName };
            var exportedPrivateKey = crypto.ExportPrivateKey(virgilKey.KeyPair.PrivateKey, password);

            storage.Store(new KeyEntry
            {
                Name = keyName,
                Value = exportedPrivateKey
            });

            return virgilKey;
        }

        /// <summary>
        /// Creates a <see cref="VirgilKey"/> with specified key name.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="password">The password.</param>
        /// <returns>The instance of <see cref="VirgilKey"/></returns>
        public static VirgilKey Create(string keyName, string password = null)
        {
            var crypto = VirgilConfig.GetService<Crypto>();
            var keyPair = crypto.GenerateKeys();

            return Create(keyName, keyPair, password);
        }

        /// <summary>
        /// Loads the <see cref="VirgilKey"/> by specified key name.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="password">The password.</param>
        /// <returns>The instance of <see cref="VirgilKey"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="VirgilKeyIsNotFoundException"></exception>
        public static VirgilKey Load(string keyName, string password = null)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));
    
            var crypto = VirgilConfig.GetService<Crypto>();
            var storage = VirgilConfig.GetService<IKeyStorage>();
            
            if (!storage.Exists(keyName))
                throw new VirgilKeyIsNotFoundException();
            
            var entry = storage.Load(keyName);
            var privateKey = crypto.ImportPrivateKey(entry.Value, password);
            var publicKey = crypto.ExtractPublicKey(privateKey);

            var virgilKey = new VirgilKey
            {
                KeyName = keyName,
                KeyPair = new KeyPair(publicKey, privateKey)
            };

            return virgilKey;
        }
        
        /// <summary>
        /// Exports the <see cref="VirgilKey"/> to default Virgil Security format.
        /// </summary>
        public byte[] Export(string password = null)
        {
            var crypto = VirgilConfig.GetService<Crypto>();
            return crypto.ExportPrivateKey(this.KeyPair.PrivateKey, password);
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

            var crypto = VirgilConfig.GetService<VirgilCrypto>();
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

            var crypto = VirgilConfig.GetService<VirgilCrypto>();
            var data = crypto.Decrypt(cipherData, this.KeyPair.PrivateKey);
            
            return data;
        }

        /// <summary>
        /// Encrypts and signs the data.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        /// <param name="recipients">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <returns>The encrypted data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public byte[] SignThenEncrypt(byte[] data, IEnumerable<VirgilCard> recipients)
        {
            if (recipients == null)
                throw new ArgumentNullException(nameof(recipients));

            var crypto = VirgilConfig.GetService<Crypto>();
            var publicKeys = recipients.Select(p => crypto.ImportPublicKey(p.PublicKey)).ToArray();

            var cipherdata = crypto.SignThenEncrypt(data, this.KeyPair.PrivateKey, publicKeys);

            return cipherdata;
        }

        /// <summary>
        /// Decrypts and verifies the data.
        /// </summary>
        /// <param name="cipherData">The data to be decrypted.</param>
        /// <param name="signer">The signer's <see cref="VirgilCard"/>.</param>
        /// <returns>The decrypted data, which is the original plain text before encryption.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public byte[] DecryptThenVerify(byte[] cipherData, VirgilCard signer)
        {
            var crypto = VirgilConfig.GetService<Crypto>();
            var publicKey = crypto.ImportPublicKey(signer.PublicKey);

            var cipherdata = crypto.DecryptThenVerify(cipherData, this.KeyPair.PrivateKey, publicKey);

            return cipherdata;
        }

        public CreateCardRequest BuildCardRequest(string identity, string type, Dictionary<string, string> data = null)
        {
            var crypto = VirgilConfig.GetService<Crypto>();
            var signer = VirgilConfig.GetService<RequestSigner>();

            var exportedPublicKey = crypto.ExportPublicKey(this.KeyPair.PublicKey);
            var request = new CreateCardRequest(identity, type, exportedPublicKey, data);

            signer.SelfSign(request, this.KeyPair.PrivateKey);

            return request;
        }
        
        /// <summary>
        /// Signs the request as authority.
        /// </summary>
        public void SignRequest(SignableRequest request, string appId)
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(appId));
            }

            var signer = VirgilConfig.GetService<RequestSigner>();
            signer.AuthoritySign(request, appId, this.KeyPair.PrivateKey);
        }

        /// <summary>
        /// Destroys the current <see cref="VirgilKey"/>.
        /// </summary>
        public void Destroy()
        {
            if (string.IsNullOrWhiteSpace(this.KeyName))
                throw new NotSupportedException();

            var storage = VirgilConfig.GetService<IKeyStorage>();
            storage.Delete(this.KeyName);
        }
    }
}