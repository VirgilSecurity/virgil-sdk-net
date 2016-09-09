#region "Copyright (C) 2015 Virgil Security Inc."
// Copyright (C) 2015 Virgil Security Inc.
// 
// Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
// 
//  (1) Redistributions of source code must retain the above copyright
//  notice, this list of conditions and the following disclaimer.
// 
//  (2) Redistributions in binary form must reproduce the above copyright
//  notice, this list of conditions and the following disclaimer in
//  the documentation and/or other materials provided with the
//  distribution.
// 
//  (3) Neither the name of the copyright holder nor the names of its
//  contributors may be used to endorse or promote products derived from
//  this software without specific prior written permission.
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

    using Virgil.SDK.Clients;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Storage;

    /// <summary>
    /// The <see cref="VirgilConfig"/> is responsible for the initialization of the high-level SDK components.
    /// </summary>
    public class VirgilConfig
    {
        private static readonly ServiceContainer Container;

        static VirgilConfig()
        {
            Container = new ServiceContainer();
            Initialize();
        }

        private static void Initialize()
        {
            Container.RegisterSingleton<IKeyStorage, VirgilKeyStorage>();
            Container.RegisterTransient<ICrypto, VirgilCrypto>();
        }
        
        /// <summary>
        /// Initializes a Virgil high-level API with specified access token.
        /// </summary>
        /// <param name="accessToken">
        /// The access token provides an authenticated secure access to the Virgil Security services and 
        /// is passed with each API call. The access token also allows the API to associate your app’s 
        /// requests with your Virgil Security developer’s account.
        /// </param>
        public static void Initialize(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(accessToken));

            Container.RegisterInstance<IServiceHub, ServiceHub>(ServiceHub.Create(accessToken));
        }

        /// <summary>
        /// Initializes a Virgil high-level API with specified configuration.
        /// </summary>
        public static void Initialize(ServiceHubConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            Container.RegisterInstance<IServiceHub, ServiceHub>(ServiceHub.Create(config));
        }
        
        /// <summary>
        /// Restores the persisted high-level SDK components values to their corresponding default properties.
        /// </summary>
        public static void Reset()
        {
            Container.Clear();
            Initialize();
        }
    }
}