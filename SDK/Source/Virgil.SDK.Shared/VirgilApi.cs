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

    /// <summary>
    /// The <see cref="VirgilApi"/> class is a high-level API that provides easy access to 
    /// Virgil Security services and allows to perform cryptographic operations by using two domain entities 
    /// <see cref="VirgilKey"/> and <see cref="VirgilCard"/>. Where the <see cref="VirgilKey"/> is an entity
    /// that represents a user's Private key, and the <see cref="VirgilCard"/> is the entity that represents
    /// user's identity and a Public key.
    /// </summary>
    public partial class VirgilApi : IVirgilApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilApi"/> class.
        /// </summary>
        public VirgilApi()
            : this(new VirgilApiConfig())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilApi" /> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public VirgilApi(string accessToken) 
            : this(new VirgilApiConfig { AccessToken = accessToken })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilApi"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param> 
        public VirgilApi(VirgilApiConfig config)    
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (string.IsNullOrWhiteSpace(config.AccessToken))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(config.AccessToken));
        }

        public VirgilBuffer Encrypt(VirgilBuffer buffer, params VirgilCard[] recipients)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Decrypt(VirgilBuffer cipherBuffer, VirgilKey key)
        {
            throw new NotImplementedException();    
        }

        public VirgilBuffer Sign(VirgilBuffer buffer, VirgilKey signerKeyPair)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Verify(VirgilBuffer buffer, VirgilBuffer signature, VirgilCard signerCard)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer SignThenEncrypt(VirgilBuffer buffer, VirgilKey signerKeyPair, params VirgilCard[] recipients)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer DecryptThenVerify(VirgilBuffer cipherBuffer, VirgilKey recipientKeyPair, VirgilCard signerCard)
        {
            throw new NotImplementedException();
        }
    }
}
