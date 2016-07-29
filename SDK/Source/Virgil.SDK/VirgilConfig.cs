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

    using Virgil.SDK.Clients;
    using Virgil.SDK.Storage;
    using Virgil.SDK.Cryptography;

    /// <summary>
    /// The <see cref="VirgilConfig"/> is responsible for the initialization of the high-level SDK components.
    /// </summary>
    public class VirgilConfig
    {
        /// <summary>
        /// Initializes service clients with API 
        /// </summary>
        /// <param name="accessToken">
        /// The access token provides an authenticated secure access to the Virgil Security services and 
        /// is passed with each API call. The access token also allows the API to associate your app’s 
        /// requests with your Virgil Security developer’s account.
        /// </param>
        public static void Initialize(string accessToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the implementation of <see cref="IServiceHub"/> that provides access to Virgil Security services.
        /// </summary>
        internal static void SetServiceHub(IServiceHub serviceHub)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the implementation of <see cref="ICryptoProvider"/> provides cryptographic operations 
        /// such as signature generation and verification, and encryption and decryption.
        /// </summary>
        public static void SetCryptoProvider(ICryptoProvider cryptoProvider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the implementation of <see cref="IKeyStorageProvider"/> that provides key storage. 
        /// </summary>
        public static void SetKeyStorageProvider(IKeyStorageProvider storage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Restores the persisted high-level SDK components values to their corresponding default properties.
        /// </summary>
        public static void Reset()
        {
            throw new NotImplementedException();
        }
    }
}