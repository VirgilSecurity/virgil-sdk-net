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
 *     (1) Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *
 *     (2) Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in
 *     the documentation and/or other materials provided with the
 *     distribution.
 *
 *     (3) Neither the name of the copyright holder nor the names of its
 *     contributors may be used to endorse or promote products derived from
 *     this software without specific prior written permission.
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

    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Requests;

    /// <summary>
    /// The <see cref="VirgilKey"/> object represents an opaque reference to keying material 
    /// that is managed by the user agent.
    /// </summary>
    public sealed partial class VirgilKey
    {
        private readonly ICryptoKeyContainer cryptoContainer;

        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        public VirgilKey(ICryptoKeyContainer cryptoContainer)
        {
            this.cryptoContainer = cryptoContainer;
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilKey" /> object that represents a new named key,
        /// using default key storage cryptoService.
        /// </summary>
        /// <param name="keyName">The name of the key.</param>
        /// <param name="keyPassword">The key password.</param>
        /// <returns>
        /// An instance of <see cref="VirgilKey" /> that represent a newly created key.
        /// </returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static VirgilKey Create(string keyName, string keyPassword = null)
        {
            if (string.IsNullOrWhiteSpace(keyName)) 
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));

            var keyContainer = new VirgilKeyContainer();
            keyContainer.InitializeNew(new VirgilKeyDetails
            {
                Name = keyName,
                Password = keyPassword
            });

            return new VirgilKey(keyContainer);
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilKey" /> object that represents a new named key,
        /// using default key storage cryptoService.
        /// </summary>
        /// <param name="keyParams">The key pairParameters.</param>
        /// <returns>An instance of <see cref="VirgilKey" /> that represent a newly created key.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static VirgilKey Create(VirgilKeyDetails keyParams)
        {
            if (keyParams == null)
                throw new ArgumentNullException(nameof(keyParams));
            
            var keyContainer = new VirgilKeyContainer();
            keyContainer.InitializeNew(keyParams);

            return new VirgilKey(keyContainer);
        }
        
        /// <summary>
        /// Loads the <see cref="VirgilKey"/> from default container by specified name.
        /// </summary>
        /// <param name="keyName">The name of the key.</param>
        /// <param name="keyPassword">The key password.</param>
        /// <returns>An instance of <see cref="VirgilKey"/></returns>
        public static VirgilKey Load(string keyName, string keyPassword = null)
        {
            if (String.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(nameof(keyName));

            var keyContainer = new VirgilKeyContainer();
            keyContainer.InitializeExisting(new VirgilKeyDetails
            {
                Name = keyName,
                Password = keyPassword
            });

            return new VirgilKey(keyContainer);
        }
        
        /// <summary>
        /// Loads the <see cref="VirgilKey"/> from specified container.
        /// </summary>
        /// <param name="keyContainer">The key container.</param>
        /// <returns>An instance of <see cref="VirgilKey"/></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static VirgilKey Load(ICryptoKeyContainer keyContainer)
        {
            if (keyContainer == null)
                throw new ArgumentNullException(nameof(keyContainer));
            
            return new VirgilKey(keyContainer);
        }

        /// <summary>
        /// Exports the <see cref="VirgilKey"/> to default Virgil Security format.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
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

            var signature = this.cryptoContainer.PerformSignatureGeneration(data.ToBytes());
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
            
            var data = this.cryptoContainer.PerformDecryption(cipherdata.ToBytes());
            return VirgilBuffer.FromBytes(data);
        }

        /// <summary>
        /// Approves the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Approve(VirgilCardRequest request)
        {
            throw new NotImplementedException();
        }
    }
}