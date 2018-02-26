#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2018 Virgil Security Inc.
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

using System;
using Virgil.CryptoAPI;

namespace Virgil.SDK.Web.Authorization
{
    /// <summary>
    /// The <see cref="JwtVerifier"/> provides verification for <see cref="Jwt"/>.
    /// </summary>
    public class JwtVerifier
    {
        /// <summary>
        /// An instance of <see cref="IAccessTokenSigner"/> that is used to 
        /// verify token signature.
        /// </summary>
        public readonly IAccessTokenSigner AccessTokenSigner;

        /// <summary>
        /// Public Key which should be used to verify signatures
        /// </summary>
        public readonly IPublicKey ApiPublicKey;

        /// <summary>
        /// Id of public key which should be used to verify signatures
        /// </summary>
        public readonly string ApiPublicKeyId;

        /// <summary>
        /// Initializes a new instance of <see cref="JwtVerifier"/>.
        /// </summary>
        /// <param name="accessTokenSigner">An instance of <see cref="IAccessTokenSigner"/> that is used to 
        /// verify token signature.</param>
        /// <param name="apiPublicKey">Public Key which should be used to verify signatures</param>
        /// <param name="apiPublicKeyId">Id of public key which should be used to verify signatures</param>
        public JwtVerifier(IAccessTokenSigner accessTokenSigner, IPublicKey apiPublicKey, string apiPublicKeyId)
        {
            this.AccessTokenSigner = accessTokenSigner ?? throw new ArgumentNullException(nameof(accessTokenSigner));
            this.ApiPublicKey = apiPublicKey ?? throw new ArgumentNullException(nameof(apiPublicKey));

            if (string.IsNullOrWhiteSpace(apiPublicKeyId))
            {
                throw new ArgumentNullException(nameof(apiPublicKeyId));
            }
            this.ApiPublicKeyId = apiPublicKeyId;
        }

        /// <summary>
        /// To verify specified token.
        /// </summary>
        /// <param name="jwToken">An instance of <see cref="Jwt"/> to be virefied.</param>
        /// <returns>true if token is verified, otherwise false.</returns>
        public bool VerifyToken(Jwt jwToken)
        {
            if (jwToken == null)
            {
                throw new ArgumentNullException(nameof(jwToken));
            }
            if (jwToken.HeaderContent.KeyId != ApiPublicKeyId || 
                jwToken.HeaderContent.Algorithm != AccessTokenSigner.GetAlgorithm() ||
                jwToken.HeaderContent.ContentType != JwtHeaderContent.VirgilContentType ||
                jwToken.HeaderContent.Type != JwtHeaderContent.JwtType)
            {
                return false;
            }
            return this.AccessTokenSigner.VerifyTokenSignature(
                jwToken.SignatureData,
                jwToken.Unsigned(),
                ApiPublicKey);
        }
    }
}