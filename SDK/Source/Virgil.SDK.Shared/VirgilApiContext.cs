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
    using System.Collections.Generic;
    using System.Linq;

    using Virgil.SDK.Common;
    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Device;
    using Virgil.SDK.Storage;

    /// <summary>
    /// The <see cref="VirgilApiContext"/> class manages the <see cref="VirgilApi"/> dependencies during run time.
    /// It also contains a list of preperties that uses to configurate the high-level components.
    /// </summary>
    public partial class VirgilApiContext
    {
        private ICrypto customCrypto;
        private IKeyStorage customStorage;
        private IDeviceManager customDevice;

        private readonly Lazy<ICrypto> lazyCrypto;
        private readonly Lazy<IKeyStorage> lazyStorage;
        private readonly Lazy<IDeviceManager> lazyDevice;
        private readonly Lazy<VirgilClient> lazyClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilApiContext"/> class.
        /// </summary>
        public VirgilApiContext()
        {
            this.lazyCrypto  = new Lazy<ICrypto>(this.InitCrypto);
            this.lazyStorage = new Lazy<IKeyStorage>(this.InitStorage);
            this.lazyDevice  = new Lazy<IDeviceManager>(this.InitDeviceManager);
            this.lazyClient  = new Lazy<VirgilClient>(this.InitClient);
        }

        /// <summary>
        /// Gets or sets the access token provides an authenticated secure access to the 
        /// Virgil Security services. The access token also allows the API to associate 
        /// your app requests with your Virgil Security developer’s account. It's not required if 
        /// <see cref="ClientParams"/> has been set.
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
        /// Gets or sets the client parameters.
        /// </summary>
        public VirgilClientParams ClientParams { get; set; }

        /// <summary>
        /// Gets a crypto API that represents a set of methods for dealing with low-level 
        /// cryptographic primitives and algorithms.
        /// </summary>
        internal ICrypto Crypto => this.lazyCrypto.Value;

        /// <summary>
        /// Gets a cryptographic keys storage. 
        /// </summary>
        internal IKeyStorage KeyStorage => this.lazyStorage.Value;

        /// <summary>
        /// Gets the instance that represents an infirmation about current device.
        /// </summary>
        internal IDeviceManager DeviceManager => this.lazyDevice.Value;

        /// <summary>
        /// Gets a Virgil Security services client.
        /// </summary>
        internal VirgilClient Client => this.lazyClient.Value;

        /// <summary>
        /// Gets the request signer.
        /// </summary>
        internal IRequestSigner RequestSigner => new RequestSigner(this.Crypto);

        /// <summary>
        /// Sets a crypto API that represents a set of methods for dealing with low-level 
        /// cryptographic primitives and algorithms.
        /// </summary>
        public void SetCrypto(ICrypto crypto)
        {
            if (crypto == null)
                throw new ArgumentNullException(nameof(crypto));

            if (this.lazyCrypto.IsValueCreated)
                throw new NotSupportedException();

            this.customCrypto = crypto;
        }

        /// <summary>
        /// Sets a cryptographic keys storage. 
        /// </summary>
        public void SetKeyStorage(IKeyStorage keyStorage)
        {
            if (keyStorage == null)
                throw new ArgumentNullException(nameof(keyStorage));

            if (this.lazyStorage.IsValueCreated)
                throw new NotSupportedException();

            this.customStorage = keyStorage;
        }

        /// <summary>
        /// Sets a manager that provides an infirmation about current device.
        /// </summary>
        public void SetDeviceManager(IDeviceManager deviceManager)
        {
            if (deviceManager == null)
                throw new ArgumentNullException(nameof(deviceManager));

            if (this.lazyDevice.IsValueCreated)
                throw new NotSupportedException();

            this.customDevice = deviceManager;
        }
      
        #region Private Methods

        private ICrypto InitCrypto()
        {
            return this.customCrypto ?? new VirgilCrypto();
        }

        private VirgilClient InitClient()
        {
            var client = this.ClientParams == null 
                ? new VirgilClient(this.AccessToken) 
                : new VirgilClient(this.ClientParams);

            var validator = new CardValidator(this.Crypto);

            if (this.CardVerifiers != null && this.CardVerifiers.Any())
            {
                foreach (var verifierInfo in this.CardVerifiers)
                {
                    validator.AddVerifier(verifierInfo.CardId, verifierInfo.PublicKeyData.GetBytes());
                }
            }

            client.SetCardValidator(validator);
            return client;
        }

        private IKeyStorage InitStorage()
        {
            return this.customStorage ?? this.GetDefaultKeyStorage();
        }

        private IDeviceManager InitDeviceManager()
        {
            return this.customDevice ?? this.GetDefaultDeviceManager();
        }

        #endregion
    }
} 