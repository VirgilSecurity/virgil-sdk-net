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
    using Newtonsoft.Json;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// <see cref="JwtHeaderContent"/> represents header of <see cref="Jwt"/>.
    /// </summary>
    [DataContract]
    public class JwtHeaderContent
    {
        public const string VirgilContentType = "virgil-jwt;v=1";
        public const string JwtType = "JWT";

        /// <summary>
        /// Signature algorithm.
        /// </summary>
        [DataMember(Name = "alg")]
        public string Algorithm { get; internal set; }

        /// <summary>
        /// Access token type.
        /// </summary>
        [DataMember(Name = "typ")]
        public string Type { get; internal set; }

        /// <summary>
        /// Access token content type.
        /// </summary>
        [DataMember(Name = "cty")]
        public string ContentType { get; internal set; }

        /// <summary>
        /// Id of public key which is used for jwt signature verification.
        /// </summary>
        [DataMember(Name = "kid")]
        public string KeyId { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtHeaderContent"/>
        /// </summary>
        /// <param name="algorithm">signature algorithm</param>
        /// <param name="keyId">API key id. Take it from 
        /// <see cref="https://dashboard.virgilsecurity.com/api-keys"/></param>
        public JwtHeaderContent(
            string algorithm, 
            string keyId, 
            string type = JwtType, 
            string contentType = VirgilContentType)
        {
            ValidateParams(algorithm, keyId);

            this.Algorithm = algorithm;
            this.KeyId = keyId;
            this.Type = type;
            this.ContentType = contentType;
        }

        private static void ValidateParams(string algorithm, string apiKeyId)
        {
            if (string.IsNullOrWhiteSpace(algorithm))
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            if (string.IsNullOrWhiteSpace(apiKeyId))
            {
                throw new ArgumentNullException(nameof(apiKeyId));
            }
        }

        [JsonConstructor]
        internal JwtHeaderContent()
        {
            
        }
    }
}
