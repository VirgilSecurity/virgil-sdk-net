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
    using System.Linq;
    using System.Reflection;

    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Client;
    using Virgil.SDK.Common;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Storage;

    /// <summary>
    /// The <see cref="VirgilConfig"/> is responsible for the initialization of the high-level SDK components.
    /// </summary>
    public sealed partial class VirgilConfig
    {
        private static readonly ServiceContainer Container;

        /// <summary>
        /// Initializes the <see cref="VirgilConfig"/> class.
        /// </summary>
        static VirgilConfig()
        {
            Container = new ServiceContainer();
        }
        
        /// <summary>
        /// Initializes a Virgil high-level API with specified access token.
        /// </summary>
        /// <param name="accessToken">
        /// The access token provides an authenticated secure access to the Virgil Security services and 
        /// is passed with each API call. The access token also allows the API to associate your app’s 
        /// requests with your Virgil Security developer’s account.
        /// </param>
        /// <param name="crypto">
        /// The <see cref="ICrypto"/> represents a set of methods for dealing with low-level 
        /// cryptographic primitives and algorithms.
        /// </param>
        /// <param name="storage">
        /// This <see cref="IKeyStorage"/> represents a storage facility for cryptographic keys.
        /// </param>
        /// <param name="validator">
        /// </param>
        public static void Initialize
        (
            string accessToken,
            ICrypto crypto = null,
            ICardValidator validator = null,
            IKeyStorage storage = null
        )
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(accessToken));
            
            InitializeContainer();

            if (crypto == null)
            {
                InitializeCrypto();
                crypto = Container.Resolve<ICrypto>();
            }
            else
            {
                Container.RemoveService<ICrypto>();
                Container.RegisterInstance<ICrypto>(crypto);
            }

            if (storage != null)
            {
                Container.RemoveService<IKeyStorage>();
                Container.RegisterInstance<IKeyStorage>(storage);
            }

            Container.RegisterSingleton<RequestSigner, RequestSigner>();

            var client = new VirgilClient(accessToken);

            if (validator == null)
            {
                validator = new CardValidator(crypto);
            }

            client.SetCardValidator(validator);
            Container.RegisterInstance<VirgilClient, VirgilClient>(client);
        }

        /// <summary>
        /// Gets the specified service instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        internal static TService GetService<TService>()
        {
            try
            {
                return Container.Resolve<TService>();
            }
            catch (ServiceNotRegisteredException)
            {
                throw new VirgilConfigIsNotInitializedException();
            }
        }

        /// <summary>
        /// Restores the persisted high-level SDK components values to their corresponding default properties.
        /// </summary>
        public static void Reset()
        {
            Container.Clear();
        }

        private static void InitializeCrypto()
        {
            var cryptoType = Assembly.Load("Virgil.SDK.Crypto")
                .GetExportedTypes()
                .SingleOrDefault(it => it.FullName == "Virgil.SDK.Cryptography.VirgilCrypto");

            if (cryptoType == null)
            {
                return;
            }

            var crypto = Activator.CreateInstance(cryptoType);
            Container.RegisterInstance<ICrypto>(crypto);
        }
    }
}