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
    using System.Collections.Generic;

    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Device;
    using Virgil.SDK.Storage;

    /// <summary>
    /// The <see cref="VirgilApiConfig"/> class contains a list of preperties that uses to configurate 
    /// the high-level SDK components.
    /// </summary>
    public class VirgilApiConfig
    {
        /// <summary>
        /// Gets or sets the access token provides an authenticated secure access to the 
        /// Virgil Security services. The access token also allows the API to associate 
        /// your app requests with your Virgil Security developer’s account.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the application authentication credentials.
        /// </summary>
        public Credentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets a list of Virgil Card verifiers.
        /// </summary>
        public IEnumerable<CardVerifierInfo> CardVerifiers { get; set; }

        /// <summary>
        /// Gets or sets a Crypto that represents a set of methods for dealing with low-level 
        /// cryptographic primitives and algorithms.
        /// </summary>
        public ICrypto Crypto { get; set; }

        /// <summary>
        /// Gets or sets a cryptographic keys storage. 
        /// </summary>
        public IKeyStorage KeyStorage { get; set; }

        /// <summary>
        /// Gets or sets the instance that represents an infirmation about current device.
        /// </summary>
        public IDeviceManager DeviceManager { get; set; }
    }
}