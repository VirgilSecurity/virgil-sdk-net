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

namespace Virgil.SDK
{
    using System;

    using Virgil.SDK.Exceptions;

    /// <summary>
    /// The <see cref="KeysManager"/> class provides a list of methods to generate the <see cref="VirgilKey"/>s 
    /// and further them storage in secure place. 
    /// </summary>
    internal class KeysManager : IKeysManager
    {
        private readonly VirgilApiContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeysManager"/> class.
        /// </summary>
        public KeysManager(VirgilApiContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Generates a new <see cref="VirgilKey"/> with default parameters.
        /// </summary>
        public VirgilKey Generate()
        {
            var keyPair = this.context.Crypto.GenerateKeys();
            return new VirgilKey(this.context, keyPair.PrivateKey);
        }

        /// <summary>
        /// Loads the <see cref="VirgilKey"/> from current storage by specified key name.
        /// </summary>
        /// <param name="keyName">The name of the Key.</param>
        /// <param name="keyPassword">The Key password.</param>
        /// <returns>An instance of <see cref="VirgilKey"/> class.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="VirgilKeyIsNotFoundException"></exception>
        public VirgilKey Load(string keyName, string keyPassword = null)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));
            
            if (!this.context.KeyStorage.Exists(keyName))
                throw new VirgilKeyIsNotFoundException();

            var entry = this.context.KeyStorage.Load(keyName);
            var privateKey = this.context.Crypto.ImportPrivateKey(entry.Value, keyPassword);

            var virgilKey = new VirgilKey(this.context, privateKey);

            return virgilKey;
        }

        /// <summary>
        /// Imports the <see cref="VirgilKey"/> from buffer.
        /// </summary>
        /// <param name="keyBuffer">The buffer with Key.</param>
        /// <param name="keyPassword">The Key password.</param>
        /// <returns>An instance of <see cref="VirgilKey"/> class.</returns>
        public VirgilKey Import(VirgilBuffer keyBuffer, string keyPassword = null)
        {
            var privateKey = this.context.Crypto.ImportPrivateKey(keyBuffer.GetBytes(), keyPassword);
            var virgilKey = new VirgilKey(this.context, privateKey);

            return virgilKey;
        }

        /// <summary>
        /// Removes the <see cref="VirgilKey"/> from the storage.
        /// </summary>
        public void Destroy(string keyName)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(keyName));
                
            this.context.KeyStorage.Delete(keyName);
        }
    }
}