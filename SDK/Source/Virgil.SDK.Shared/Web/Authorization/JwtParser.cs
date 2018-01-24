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
using NeoSmart.Utils;
using Virgil.SDK.Common;

namespace Virgil.SDK.Web.Authorization
{
    internal class JwtParser
    {
        public static Jwt Parse(string jwt)
        {
            var parts = jwt.Split(new char[] {'.'});
            if (parts.Length != 3)
            {
                throw new ArgumentException("Wrong JWT format.");
            }

            try
            {
                var headerJson = Bytes.ToString(UrlBase64.Decode(parts[0]));
                var header = Configuration.Serializer.Deserialize<JwtHeaderContent>(headerJson);
                var bodyJson = Bytes.ToString(UrlBase64.Decode(parts[1]));
                var body = Configuration.Serializer.Deserialize<JwtBodyContent>(bodyJson);
                body.AppId = body.Issuer.Clone().ToString().Replace(JwtBodyContent.SubjectPrefix, "");
                body.Identity = body.Subject.Clone().ToString().Replace(JwtBodyContent.IdentityPrefix, "");
                return new Jwt(header, body) {SignatureData = UrlBase64.Decode(parts[2])};
            }
            catch (Exception)
            {
                throw new ArgumentException("Wrong JWT format.");
            }

        }
    }
}
