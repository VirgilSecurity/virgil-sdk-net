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
using Virgil.SDK.Common;

namespace Virgil.SDK.Web.Authorization
{
    public class JwtVerifier
    {
        public readonly IAccessTokenSigner AccessTokenSigner;
        public readonly IPublicKey ApiPublicKey;
        public readonly string ApiPublicKeyId;
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

        public bool VerifyToken(Jwt jwToken)
        {
            if (jwToken == null)
            {
                throw new ArgumentNullException(nameof(jwToken));
            }
            if (jwToken.HeaderContent.ApiKeyId != ApiPublicKeyId || 
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