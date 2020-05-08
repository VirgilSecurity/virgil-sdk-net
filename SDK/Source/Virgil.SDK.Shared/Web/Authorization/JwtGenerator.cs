#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2019 Virgil Security Inc.
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

namespace Virgil.SDK.Web.Authorization
{
    using System;
    using System.Collections.Generic;

    using Virgil.CryptoAPI;
    using Virgil.SDK.Common;

    /// <summary>
    /// The <see cref="JwtGenerator"/> class implements <see cref="Jwt"/> generation. 
    /// </summary>
    public class JwtGenerator
    {
        /// <summary>
        /// Private Key which will be used for signing 
        /// enerated access tokens. 
        /// Take it on <see cref="https://dashboard.virgilsecurity.com/api-keys"/>.
        /// </summary>
        public readonly IPrivateKey ApiKey;

        /// <summary>
        /// Key Id of <see cref="ApiKey"/>. 
        /// Take it on <see cref="https://dashboard.virgilsecurity.com/api-keys"/>
        /// </summary>
        public readonly string ApiPublicKeyId;

        /// <summary>
        /// Application id. 
        /// Take it on <see cref="https://dashboard.virgilsecurity.com"/>.
        /// </summary>
        public readonly string AppId;

        /// <summary>
        /// Lifetime of generated tokens.
        /// </summary>
        public readonly TimeSpan LifeTime;

        /// <summary>
        ///  An instance of <see cref="IAccessTokenSigner"/> that is used to 
        /// generate token signature using <see cref="ApiKey"/>.
        /// </summary>
        public readonly IAccessTokenSigner AccessTokenSigner;

        /// <summary>
        /// Initializes a new instance of <see cref="JwtGenerator"/>.
        /// </summary>
        /// <param name="appId">Application id. Take it on 
        /// <see cref="https://dashboard.virgilsecurity.com"/></param>
        /// <param name="apiKey">Private Key which will be used for signing 
        /// enerated access tokens. Take it on 
        /// <see cref="https://dashboard.virgilsecurity.com/api-keys"/></param>
        /// <param name="apiPublicKeyId">Key Id of <see cref="apiKey"/>. 
        /// Take it on <see cref="https://dashboard.virgilsecurity.com/api-keys"/>
        ///  </param>
        /// <param name="lifeTime">Lifetime of generated tokens.</param>
        /// <param name="accessTokenSigner">
        /// An instance of <see cref="IAccessTokenSigner"/> that is used to 
        /// generate token signature using <see cref="apiKey"/>.</param>
        public JwtGenerator(
            string appId, 
            IPrivateKey apiKey, 
            string apiPublicKeyId,
            TimeSpan lifeTime,
            IAccessTokenSigner accessTokenSigner
            )
        {
            this.AppId = appId;
            this.ApiKey = apiKey;
            this.LifeTime = lifeTime;
            this.ApiPublicKeyId = apiPublicKeyId;
            this.AccessTokenSigner = accessTokenSigner;
        }

        /// <summary>
        /// Generates new JWT using specified identity and additional data.
        /// </summary>
        /// <param name="identity">identity to generate with.</param>
        /// <param name="data">dictionary with additional data which will be kept in jwt body.</param>
        /// <returns>a new instanse of <see cref="Jwt"/>.</returns>
        public Jwt GenerateToken(string identity, Dictionary<object, object> data = null)
        {
            if (string.IsNullOrWhiteSpace(identity))
            {
                throw new ArgumentException($"{nameof(identity)} property is mandatory");
            }

            //to truncate milliseconds and microseconds
            var timeNow = DateTime.UtcNow;
            var issuedAt = timeNow.AddTicks(-timeNow.Ticks % TimeSpan.TicksPerSecond);
            var expiresAt = issuedAt.AddMilliseconds(LifeTime.TotalMilliseconds);
            var jwtBody = new JwtBodyContent(
                AppId, 
                identity,
                issuedAt,
                expiresAt,
                data);

            var jwtHeader = new JwtHeaderContent(AccessTokenSigner.GetAlgorithm(), ApiPublicKeyId);
            var unsignedJwt = new Jwt(jwtHeader, jwtBody, null);
            var jwtBytes = Bytes.FromString(unsignedJwt.ToString());
            var signature = AccessTokenSigner.GenerateTokenSignature(jwtBytes, ApiKey);
            return new Jwt(jwtHeader, jwtBody, signature);
        }
    }
}
