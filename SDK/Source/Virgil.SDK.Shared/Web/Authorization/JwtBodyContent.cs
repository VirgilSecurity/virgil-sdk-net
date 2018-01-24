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
using System.Runtime.Serialization;

namespace Virgil.SDK.Web.Authorization
{
    [DataContract]
    public class JwtBodyContent
    {
        public const string IdentityPrefix = "identity-";
        public const string SubjectPrefix = "virgil-";

        public string AppId { get; internal set; }
        public string Identity{ get; internal set; }

        [DataMember(Name = "iss")]
        public string Issuer { get; internal set; } 

        [DataMember(Name = "sub")]
        public string Subject { get; internal set; } 

        [DataMember(Name = "iat")]
        public DateTime IssuedAt { get; internal set; }

        [DataMember(Name = "exp")]
        public DateTime ExpiresAt { get; internal set; }

        [DataMember(Name = "ada")]
        public Dictionary<string, string> AdditionalData { get; internal set; }

        public JwtBodyContent(string appId, 
            string identity, 
            TimeSpan lifeTime, 
            Dictionary<string, string> data)
        {
            this.AppId = appId;
            this.Identity = identity;

            //to truncate milliseconds and microseconds
            var timeNow = DateTime.UtcNow;
            this.IssuedAt = timeNow.AddTicks(-timeNow.Ticks % TimeSpan.TicksPerSecond);
            this.ExpiresAt = this.IssuedAt.AddMilliseconds(lifeTime.TotalMilliseconds);
            this.Identity = identity;
            this.AdditionalData = data;
            this.Issuer = $"{SubjectPrefix}{AppId}";
            this.Subject = $"{IdentityPrefix}{Identity}";
        }

        public JwtBodyContent()
        {
        }
    }

}
