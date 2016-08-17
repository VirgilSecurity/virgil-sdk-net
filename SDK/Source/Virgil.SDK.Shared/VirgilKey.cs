#region "Copyright (C) 2015 Virgil Security Inc."
/**
 * Copyright (C) 2015 Virgil Security Inc.
 *
 * Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *   (1) Redistributions of source code must retain the above copyright
 *   notice, this list of conditions and the following disclaimer.
 *   
 *   (2) Redistributions in binary form must reproduce the above copyright
 *   notice, this list of conditions and the following disclaimer in
 *   the documentation and/or other materials provided with the
 *   distribution.
 *   
 *   (3) Neither the name of the copyright holder nor the names of its
 *   contributors may be used to endorse or promote products derived from
 *   this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
 * IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */
#endregion

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;

    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Cryptography;

    /// <summary>
    /// The <see cref="VirgilKey"/> object represents an opaque reference to keying material 
    /// that is managed by the user agent.
    /// </summary>
    public sealed partial class VirgilKey
    {
        private readonly ISecurityModule securityModule;  

        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        internal VirgilKey(ISecurityModule securityModule)
        {
            this.securityModule = securityModule;
        }

        public static VirgilKey Create(string keyName)
        {
            return Create(keyName, null);
        }

        public static VirgilKey Create(string keyName, IKeyPairParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));
            }

            var keyStorage = VirgilConfig.ServiceResolver.Resolve<IKeyPairStorage>();
            var keyPairGenerator = VirgilConfig.ServiceResolver.Resolve<IKeyPairGenerator>();

            if (keyStorage.Exists(keyName))
            {
                throw new VirgilKeyIsAlreadyExistsException();
            }

            var keyPairId = Guid.NewGuid();
            var keyPair = keyPairGenerator.Generate(parameters);

            var entry = new KeyPairEntry
            {
                PublicKey = keyPair.PublicKey.Value,
                PrivateKey = keyPair.PrivateKey.Value,
                MetaData = new Dictionary<string, string> { { "Id", keyPairId.ToString() } }
            };

            keyStorage.Store(keyName, entry);
            var securityModule = VirgilConfig.ServiceResolver.Resolve<SecurityModule>();

            securityModule.Initialize(keyPairId.ToByteArray(), keyPair.PrivateKey);

            return new VirgilKey(securityModule);
        }

        public static VirgilKey Load(string keyName)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));
            }

            var keyStorage = VirgilConfig.ServiceResolver.Resolve<IKeyPairStorage>();

            if (!keyStorage.Exists(keyName))
            {
                throw new VirgilKeyIsNotFoundException();
            }

            var entry = keyStorage.Load(keyName);
            var securityModule = VirgilConfig.ServiceResolver.Resolve<SecurityModule>();

            var keyPairId = Guid.Parse(entry.MetaData["Id"]);
            securityModule.Initialize(keyPairId.ToByteArray(), new PrivateKey(entry.PrivateKey));

            return new VirgilKey(securityModule);
        }

        /// <summary>
        /// Loads the <see cref="VirgilKey"/> from specified security module instance.
        /// </summary>
        /// <param name="securityModule">The security module.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static VirgilKey Load(ISecurityModule securityModule)
        {
            if (securityModule == null)
                throw new ArgumentNullException(nameof(securityModule));

            return new VirgilKey(securityModule);
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
        /// <exception cref="System.ArgumentNullException"></exception>
        public VirgilBuffer Sign(VirgilBuffer data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var signature = this.securityModule.SignData(data.ToBytes());
            return VirgilBuffer.FromBytes(signature);
        }

        /// <summary>
        /// Decrypts the specified cipherdata using <see cref="VirgilKey"/>.
        /// </summary>
        /// <param name="cipherdata">The cipherdata.</param>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public VirgilBuffer Decrypt(VirgilBuffer cipherdata)
        {
            if (cipherdata == null)
                throw new ArgumentNullException(nameof(cipherdata));
            
            var data = this.securityModule.DecryptData(cipherdata.ToBytes());
            return VirgilBuffer.FromBytes(data);
        }
    }
}