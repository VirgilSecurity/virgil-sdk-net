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

namespace Virgil.SDK.Web.Authorization
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// <see cref="JwtBodyContent"/> represents content of <see cref="Jwt"/>.
    /// </summary>
    [DataContract]
    public class JwtBodyContent
    {
        public const string IdentityPrefix = "identity-";
        public const string SubjectPrefix = "virgil-";

        /// <summary>
        /// Jwt application id.
        /// </summary>
        public string AppId { get; internal set; }

        /// <summary>
        /// Jwt identity.
        /// </summary>
        public string Identity{ get; internal set; }

        /// <summary>
        /// Jwt issuer.
        /// </summary>
        [DataMember(Name = "iss")]
        public string Issuer { get; internal set; }

        /// <summary>
        /// Jwt subject.
        /// </summary>
        [DataMember(Name = "sub")]
        public string Subject { get; internal set; }

        /// <summary>
        /// When Jwt was issued.
        /// </summary>
        [DataMember(Name = "iat")]
        public DateTime IssuedAt { get; internal set; }

        /// <summary>
        /// When Jwt will expire.
        /// </summary>
        [DataMember(Name = "exp")]
        public DateTime ExpiresAt { get; internal set; }

        /// <summary>
        /// Jwt additional data.
        /// </summary>
        [DataMember(Name = "ada")]
        public Dictionary<object, object> AdditionalData { get; internal set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="JwtBodyContent"/>
        /// </summary>
        /// <param name="appId">Application ID. Take it from 
        /// <see cref="https://dashboard.virgilsecurity.com"/></param>
        /// <param name="identity">identity (must be equal to RawSignedModel identity when publishing card)</param>
        /// <param name="issuedAt">issued data</param>
        /// <param name="expiresAt">expiration date</param>
        /// <param name="data">dictionary with additional data</param>
        public JwtBodyContent(string appId, 
            string identity, 
            DateTime issuedAt,
            DateTime expiresAt,
            Dictionary<object, object> data)
        {
            ValidateParams(appId, identity, issuedAt, expiresAt);

            this.AppId = appId;
            this.Identity = identity;
            this.ExpiresAt = expiresAt;
            this.IssuedAt = issuedAt;
            this.Identity = identity;
            this.AdditionalData = data;
            this.Issuer = $"{SubjectPrefix}{AppId}";
            this.Subject = $"{IdentityPrefix}{Identity}";
        }

        [JsonConstructor]
        internal JwtBodyContent()
        {
        }

        private static void ValidateParams(string appId, string identity, DateTime issuedAt, DateTime expiresAt)
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new ArgumentNullException(nameof(appId));
            }

            if (string.IsNullOrWhiteSpace(identity))
            {
                throw new ArgumentNullException(nameof(identity));
            }

            if (issuedAt == null)
            {
                throw new ArgumentNullException(nameof(issuedAt));
            }
            if (expiresAt == null)
            {
                throw new ArgumentNullException(nameof(expiresAt));
            }
        }
    }
}
