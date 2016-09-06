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

    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Storage;

    /// <summary>
    /// The <see cref="VirgilKey"/> object represents an opaque reference to keying material 
    /// that is managed by the user agent.
    /// </summary>
    public sealed partial class VirgilKey
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        private Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the key.
        /// </summary>
        private string KeyName { get; set; }

        /// <summary>
        /// Gets or sets the private key.
        /// </summary>
        private PrivateKey PrivateKey { get; set; }

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

            var keyStorage = ServiceLocator.Resolve<IKeyStorage>();
            var encryptor = ServiceLocator.Resolve<IKeyStorage>();  
            var crypto = ServiceLocator.Resolve<Virgil.Crypto>();

            if (keyStorage.Exists(keyName))
            {
                throw new VirgilKeyIsAlreadyExistsException();
            }

            var virgilKey = new VirgilKey
            {
                Id = Guid.NewGuid(),
                KeyName = keyName,
                PrivateKey = crypto.GenerateKey(),
            };
            
            var entry = new KeyEntry
            {
                Name = virgilKey.KeyName,
                Value = crypto.ExportKey(virgilKey.PrivateKey),
                MetaData = new Dictionary<string, string> { { "Id", virgilKey.Id.ToString() } }
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

            var keyStorage = ServiceLocator.Resolve<IKeyStorage>();

            if (!keyStorage.Exists(keyName))
            {
                throw new VirgilKeyIsNotFoundException();
            }

            var entry = keyStorage.Load(keyName);
            var securityModule = ServiceLocator.Resolve<CryptoService>();

            var keyPairId = Guid.Parse(entry.MetaData["Id"]);
            securityModule.Initialize(keyPairId.ToByteArray(), new PrivateKey(entry.Value), password);

            var key = new VirgilKey
            {
                id = keyPairId,
                keyName = keyName,
                cryptoModule = securityModule
            };

            return key;
        }

        /// <summary>
        /// Loads the <see cref="VirgilKey"/> from specified security module instance.
        /// </summary>
        /// <param name="cryptoModule">The security module.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static VirgilKey Load(ICryptoModule cryptoModule)
        {
            if (cryptoModule == null)
                throw new ArgumentNullException(nameof(cryptoModule));

            return new VirgilKey();
        }

        /// <summary>
        /// Exports the <see cref="VirgilKey"/> to default Virgil Security format.
        /// </summary>
        public VirgilBuffer Export()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a digital signature for specified data using current <see cref="VirgilKey"/>.
        /// </summary>
        /// <param name="data">The data for which the digital signature will be generated.</param>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VirgilBuffer Sign(VirgilBuffer data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var signature = this.cryptoModule.SignData(data.ToBytes());
            return VirgilBuffer.FromBytes(signature);
        }

        /// <summary>
        /// Decrypts the specified cipherdata using <see cref="VirgilKey"/>.
        /// </summary>
        /// <param name="cipherdata">The cipherdata.</param>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VirgilBuffer Decrypt(VirgilBuffer cipherdata)
        {
            if (cipherdata == null)
                throw new ArgumentNullException(nameof(cipherdata));
            
            var data = this.cryptoModule.DecryptData(cipherdata.ToBytes());
            return VirgilBuffer.FromBytes(data);
        }

        /// <summary>
        /// Creates the card request.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="identityType">Type of the identity.</param>
        /// <param name="scope">The scope.</param>
        /// <returns></returns>
        public VirgilCardCreateRequest BuildCreateCardRequest(string identity, string identityType, VirgilCardScope scope)
        {
            return new VirgilCardCreateRequest
            (
                identity, 
                identityType, 
                this.cryptoModule.GetPublicKey(), 
                "",
                "",
                scope
            );
        }
    }
}