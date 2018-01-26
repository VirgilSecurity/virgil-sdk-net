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
using System.Collections.Generic;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;

namespace Virgil.SDK.Web.Authorization
{
    public class JwtGenerator
    {

        public readonly IPrivateKey AccessPrivateKey;

        public readonly string AccessKeyId;

        public readonly string AppId;

        public readonly TimeSpan LifeTime;

        public readonly IAccessTokenSigner AccessTokenSigner;

        public JwtGenerator(
            string appId, 
            IPrivateKey accessPrivateKey, 
            string accessKeyId,
            TimeSpan lifeTime,
            IAccessTokenSigner accessTokenSigner
            )
        {
            this.AppId = appId;
            this.AccessPrivateKey = accessPrivateKey;
            this.LifeTime = lifeTime;
            this.AccessKeyId = accessKeyId;
            this.AccessTokenSigner = accessTokenSigner;
        }
        public Jwt GenerateToken(string identity, Dictionary<string, string> data = null)
        {
            var jwtBody = new JwtBodyContent(
                AppId, 
                identity,
                LifeTime,
                data);

            var jwtHeader = new JwtHeaderContent(AccessTokenSigner.GetAlgorithm(), AccessKeyId);
           
            var unsignedJwt = new Jwt(jwtHeader, jwtBody, null);
            var jwtBytes = Bytes.FromString(unsignedJwt.ToString());
            var signature = AccessTokenSigner.GenerateTokenSignature(jwtBytes, AccessPrivateKey);
            return new Jwt(jwtHeader, jwtBody, signature);
        }

    }
}
