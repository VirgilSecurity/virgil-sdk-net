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
    public class VirgilApi : IVirgilApi
    {
        private readonly VirgilApiContext context;

        private readonly Lazy<IKeysManager> lazyKeysManager;
        private readonly Lazy<ICardsManager> lazyCardsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilApi"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <example>
        /// <para>Initializes a new instance for actions: get card, find card.</para>
        /// <code>
        ///     var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
        /// </code>
        /// </example>
        public VirgilApi(string accessToken)
            : this(new VirgilApiContext { AccessToken = accessToken })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilApi"/> class.
        /// </summary>
        /// <example>
        /// <para>Initializes a new instance for actions with global cards.</para>
        ///     <code>
        ///       var virgilApi = new VirgilApi();
        ///     </code>
        /// </example>
        public VirgilApi()
            : this(new VirgilApiContext { AccessToken = null })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilApi"/> class.
        /// </summary>
        /// <param name="context">The configuration.</param> 
        /// <example>
        /// <para>Initializes a new instance for actions: publish card, revoke card.</para>
        /// <code>
        ///            var virgil = new VirgilApi(new VirgilApiContext
        ///            {
        ///                    AccessToken = "[YOUR_ACCESS_TOKEN_HERE]",
        ///                    Credentials = new AppCredentials
        ///                {
        ///                    AppId = "[YOUR_APP_ID_HERE]",
        ///                    AppKey = VirgilBuffer.FromFile("[YOUR_APP_KEY_FILEPATH_HERE]"),
        ///                    AppKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]",
        ///                }
        ///             });
        /// </code>
        /// </example>
        public VirgilApi(VirgilApiContext context)    
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            this.context = context;

            this.lazyKeysManager = new Lazy<IKeysManager>(() => new KeysManager(this.context));
            this.lazyCardsManager = new Lazy<ICardsManager>(() => new CardsManager(this.context));
        }

        /// <summary>
        /// Gets an instances of the class that provides a work with <see cref="VirgilKey"/> entities.
        /// </summary>
        public IKeysManager Keys => this.lazyKeysManager.Value;

        /// <summary>
        /// Gets an instances of the class that provides a work with <see cref="VirgilCard"/> entities.
        /// </summary>
        public ICardsManager Cards => this.lazyCardsManager.Value;
    }
}
