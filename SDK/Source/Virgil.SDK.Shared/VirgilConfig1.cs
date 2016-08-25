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
    using System.Collections.Generic;
    using Virgil.SDK.Clients;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Storage;

    /// <summary>
    /// The <see cref="VirgilConfig1"/> is responsible for the initialization of the high-level SDK components.
    /// </summary>
    public class VirgilConfig1
    {
        private static readonly ServiceContainer Container;

        static VirgilConfig1()
        {
            Container = new ServiceContainer();
            Initialize();
        }

        private static void Initialize()
        {
            Container.RegisterSingleton<IPrivateKeyStorage, VirgilPrivateKeyStorage>();
            Container.RegisterSingleton<CryptoService, VirgilCryptoService>();
            Container.RegisterTransient<IKeyPairGenerator, VirgilKeyPairGenerator>();

            ServiceLocator.SetServiceResolver(Container);
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

    public class VirgilFactory
    {
        private readonly ServiceContainer container;
        private IServiceInjectAdapter injectAdapter;

        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilFactory"/> class from being created.
        /// </summary>
        private VirgilFactory(ServiceContainer container)
        {
            this.container = container;
        }

        public IKeyPairGenerator GetKeyPairGenerator()
        {
            return this.GetService<IKeyPairGenerator>();
        }

        public IPrivateKeyStorage GetPrivateKeyStorage()
        {
            return this.GetService<IPrivateKeyStorage>();
        }

        public CryptoService GetCryptoService()
        {
            return this.GetService<CryptoService>();
        }

        public IServiceHub GetServicesClient()
        {
            return this.GetService<IServiceHub>();
        }

        private TService GetService<TService>()
        {
            if (this.injectAdapter != null && this.injectAdapter.CanResolve<TService>())
            {   
                return this.injectAdapter.Resolve<TService>();
            }

            return this.container.Resolve<TService>();
        }

        /// <summary>
        /// Initializes the specified access token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public static VirgilFactory Create(string accessToken)
        {
            return Create(new VirgilConfig(accessToken));
        }

        /// <summary>
        /// Initializes the specified access token.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static VirgilFactory Create(VirgilConfig config)
        {
            var container = new ServiceContainer();
            container.RegisterTransient<IKeyPairGenerator, VirgilKeyPairGenerator>();
            container.RegisterSingleton<IPrivateKeyStorage, VirgilPrivateKeyStorage>();
            container.RegisterSingleton<CryptoService, VirgilCryptoService>();

            if (!string.IsNullOrWhiteSpace(config.AccessToken))
            {
                var serviceHub = ServiceHub.Create(config.AccessToken);
                container.RegisterInstance<IServiceHub, ServiceHub>(serviceHub);
            }

            var factory = new VirgilFactory(container)
            {
                injectAdapter = config.ServiceInjectAdapter
            };
            
            return factory;
        }
    }

    public class VirgilConfig
    {
        internal string AccessToken { get; private set; }
        internal string ServiceCardsAddress { get; private set; }

        internal PublicKey ServiceCardsPublicKey { get; private set; }
        internal string ServiceIdentityAddress { get; private set; }
        internal string ServicePrivateKeyAddress { get; private set; }
        internal PublicKey ServicePrivateKeyPublicKey { get; private set; }
        internal IServiceInjectAdapter ServiceInjectAdapter { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilConfig"/> class.
        /// </summary>
        public VirgilConfig()
        {
            var factory = VirgilFactory.Create("ACCESS_TOKEN");

            var generator  = factory.GetKeyPairGenerator();
            var storage = factory.GetPrivateKeyStorage();
            var client = factory.GetServicesClient();


            var  storage.Load("ALICE");
            


            
            var keypair = generator.Generate();

            storage.Store("ALICE", keypair.PrivateKey);
            var key = storage.Load("ALICE");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilConfig"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public VirgilConfig(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        public void SetCardsServiceParameters(string address, PublicKey publicKey)
        {
            this.ServiceCardsAddress = address;
            this.ServiceCardsPublicKey = publicKey;
        }

        public void SetIdentityServiceParameters(string address)
        {
            this.ServiceIdentityAddress = address;
        }

        public void SetPrivateKeyServiceParameters(string address, PublicKey publicKey)
        {
            this.ServicePrivateKeyAddress = address;
            this.ServicePrivateKeyPublicKey = publicKey;
        }

        public void SetServiceInjectAdapter(IServiceInjectAdapter injectAdapter)
        {
            this.ServiceInjectAdapter = injectAdapter;
        }
    }
}