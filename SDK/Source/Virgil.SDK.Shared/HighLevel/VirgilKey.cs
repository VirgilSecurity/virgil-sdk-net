#region Copyright (C) Virgil Security Inc.
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

    using Virgil.SDK.Device;
    using Virgil.SDK.Client;
    using Virgil.SDK.Common;
    using Virgil.SDK.Storage;
    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Cryptography;
    
    public sealed partial class VirgilKey
    {
        private const string KeyEntryMetaKeyCardId = "card_id";
        private const string KeyEntryMetaKeyCardSnapshot = "card_snapshot";

        private string fingerprint;
        private string identity;
        private KeyPair keyPair;
            
        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        private VirgilKey()
        {
        }

        /// <summary>
        /// Exports the <see cref="VirgilKey"/> to default Virgil Security format.
        /// </summary>
        public byte[] Export(string password = null)
        {
            var crypto = VirgilConfig.GetService<ICrypto>();
            return crypto.ExportPrivateKey(this.keyPair.PrivateKey, password);
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

            var crypto = VirgilConfig.GetService<ICrypto>();
            var signature = crypto.Sign(data, this.keyPair.PrivateKey);

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

            var crypto = VirgilConfig.GetService<ICrypto>();
            var data = crypto.Decrypt(cipherData, this.keyPair.PrivateKey);

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

            var crypto = VirgilConfig.GetService<ICrypto>();
            var publicKeys = recipients.Select(p => crypto.ImportPublicKey(p.PublicKey)).ToArray();

            var cipherdata = crypto.SignThenEncrypt(data, this.keyPair.PrivateKey, publicKeys);

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
            var crypto = VirgilConfig.GetService<ICrypto>();
            var publicKey = crypto.ImportPublicKey(signer.PublicKey);

            var cipherdata = crypto.DecryptThenVerify(cipherData, this.keyPair.PrivateKey, publicKey);

            return cipherdata;
        }

        /// <summary>
        /// Signs the request as authority.
        /// </summary>
        public void SignRequest(ISignableRequest request)
        {
            var signer = VirgilConfig.GetService<IRequestSigner>();
            signer.AuthoritySign(request, this.fingerprint, this.keyPair.PrivateKey);
        }

        /// <summary>
        /// Destroys the current <see cref="VirgilKey"/>.
        /// </summary>
        public void Destroy()
        {
            if (string.IsNullOrWhiteSpace(this.identity))
                throw new NotSupportedException();

            var storage = VirgilConfig.GetService<IKeyStorage>();
            storage.Delete(this.identity);
        }

        /// <summary>
        /// Creates a <see cref="VirgilKey"/> with specified key name.
        /// </summary>
        /// <param name="identity">The identity name.</param>
        /// <param name="password">The password.</param>
        /// <returns>The instance of <see cref="VirgilKey"/></returns>
        public static VirgilKey Create(string identity, string password = null)
        {
            return Create(new VirgilKeyDetails 
            {
                Identity = identity,
                PrivateKeyPassword = password
            });
        }

        /// <summary>
        /// Creates the <see cref="VirgilKey"/> with specified details.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <returns>The instance of <see cref="VirgilKey"/></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="VirgilKeyIsAlreadyExistsException"></exception>
        public static VirgilKey Create(VirgilKeyDetails details)
        {
            if (details == null)
                throw new ArgumentNullException(nameof(details));
            
            if (string.IsNullOrWhiteSpace(details.Identity))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(details.Identity));
            
            var crypto  = VirgilConfig.GetService<ICrypto>();
            var device  = VirgilConfig.GetService<IDeviceManager>();
            var storage = VirgilConfig.GetService<IKeyStorage>();

            KeyPair keyPair;
            
            if (details.PrivateKeyData == null)
            {
                keyPair = crypto.GenerateKeys();
            }
            else
            {
                var privateKey = crypto.ImportPrivateKey(details.PrivateKeyData, details.PrivateKeyPassword);
                var publicKey = crypto.ExtractPublicKey(privateKey);    

                keyPair = new KeyPair(publicKey, privateKey);
            }

            var snapshotModel = new PublishCardSnapshotModel
            {
                Identity = details.Identity,
                IdentityType = details.IdentityType ?? "undefined",
                PublicKeyData = crypto.ExportPublicKey(keyPair.PublicKey),
                Data = details.Data,
                Info = new CardInfoModel
                {
                    DeviceName = device.GetDeviceName(),
                    Device = device.GetSystemName()
                }
            };

            var exportedPrivateKey = crypto.ExportPrivateKey(keyPair.PrivateKey, details.PrivateKeyPassword);

            var publishCardRequest = new PublishCardRequest(snapshotModel);
            var fingerprint = crypto.CalculateFingerprint(publishCardRequest.Snapshot).ToHEX();

            var keyEntry = new KeyEntry
            {
                Name = snapshotModel.Identity,
                Value = exportedPrivateKey,
                MetaData = new Dictionary<string, string>
                {
                    [KeyEntryMetaKeyCardId] = fingerprint,
                    [KeyEntryMetaKeyCardSnapshot] = Convert.ToBase64String(publishCardRequest.Snapshot)
                }
            };

            if (storage.Exists(keyEntry.Name))
                throw new VirgilKeyIsAlreadyExistsException();

            storage.Store(keyEntry);

            var virgilKey = new VirgilKey
            {
                identity = snapshotModel.Identity,
                keyPair = keyPair,
                fingerprint = fingerprint
            };

            return virgilKey;
        }

        /// <summary>
        /// Loads the <see cref="VirgilKey"/> by specified key name.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="password">The password.</param>
        /// <returns>The instance of <see cref="VirgilKey"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="VirgilKeyIsNotFoundException"></exception>
        public static VirgilKey Load(string identity, string password = null)
        {
            if (string.IsNullOrWhiteSpace(identity))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(identity));
    
            var crypto = VirgilConfig.GetService<ICrypto>();
            var storage = VirgilConfig.GetService<IKeyStorage>();
            
            if (!storage.Exists(identity))
                throw new VirgilKeyIsNotFoundException();
            
            var entry = storage.Load(identity);
            var privateKey = crypto.ImportPrivateKey(entry.Value, password);
            var publicKey = crypto.ExtractPublicKey(privateKey);

            var virgilKey = new VirgilKey
            {
                identity = identity,
                fingerprint = entry.MetaData[KeyEntryMetaKeyCardId],
                keyPair = new KeyPair(publicKey, privateKey)
            };

            return virgilKey;
        }

        public PublishCardRequest BuildCardPublishRequest()
        {
            throw new NotImplementedException();
        }
        
        public RevokeCardRequest BuildRevokeCardRequest()
        {
            throw new NotImplementedException();
        }
    }
}