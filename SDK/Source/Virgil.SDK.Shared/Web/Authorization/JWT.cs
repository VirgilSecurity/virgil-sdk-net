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
using Virgil.SDK.Common;

namespace Virgil.SDK.Web.Authorization
{
    /// <summary>
    /// The <see cref="Jwt"/> class implements interface <see cref="IAccessToken"/>
    ///  in terms of Virgil JWT.
    /// </summary>
    public class Jwt : IAccessToken
    {
        /// <summary>
        /// Gets a jwt header.
        /// </summary>
        public readonly JwtHeaderContent HeaderContent;

        /// <summary>
        /// Gets a jwt body.
        /// </summary>
        public readonly JwtBodyContent BodyContent;

        /// <summary>
        /// Gets a digital signature of jwt.
        /// </summary>
        public readonly byte[] SignatureData;

        private readonly string stringRepresentation;
        private readonly byte[] unsignedData;


        /// <summary>
        /// Initializes a new instance of the <see cref="Jwt" /> class using specified header, body and signature.
        /// </summary>
        /// <param name="jwtHeaderContent">jwt header, an instance of <see cref="JwtHeaderContent"/>.</param>
        /// <param name="jwtBodyContent">jwt body, an instance of <see cref="JwtBodyContent"/>.</param>
        /// <param name="signatureData">jwt signature data.</param>
        public Jwt(
            JwtHeaderContent jwtHeaderContent,
            JwtBodyContent jwtBodyContent, 
            byte[] signatureData
            )
        {
            BodyContent = jwtBodyContent ?? throw new ArgumentNullException(nameof(jwtBodyContent));
            HeaderContent = jwtHeaderContent ?? throw new ArgumentNullException(nameof(jwtHeaderContent));
            SignatureData = signatureData;
            var withoutSignature = this.HeaderBase64() + "." + this.BodyBase64();
            unsignedData = Bytes.FromString(withoutSignature);
            stringRepresentation = withoutSignature;
            if (this.SignatureData != null)
            {
                stringRepresentation += "." + this.SignatureBase64();
            }
        }
       
        /// <summary>
        /// Initializes a new instance of the <see cref="Jwt" /> class using its string representation.
        /// </summary>
        /// <param name="jwtStr">string representation of signed jwt. It must be equal to:
        ///  base64UrlEncode(JWT Header) + "." + base64UrlEncode(JWT Body) "." + base64UrlEncode(Jwt Signature). 
        /// </param>
        public Jwt(string jwtStr)
        {
            var parts = jwtStr.Split(new char[] { '.' });
            if (parts.Length != 3)
            {
                throw new ArgumentException("Wrong JWT format.");
            }
            try
            {
                var headerJson = Bytes.ToString(Base64Url.Decode(parts[0]));
                HeaderContent = Configuration.Serializer.Deserialize<JwtHeaderContent>(headerJson);
                var bodyJson = Bytes.ToString(Base64Url.Decode(parts[1]));
                BodyContent = Configuration.Serializer.Deserialize<JwtBodyContent>(bodyJson);
                SignatureData = Base64Url.Decode(parts[2]);
            }
            catch (Exception)
            {
                throw new ArgumentException("Wrong JWT format.");
            }
           
            BodyContent.AppId = BodyContent.Issuer.Clone().ToString().Replace(JwtBodyContent.SubjectPrefix, "");
            BodyContent.Identity = BodyContent.Subject.Clone().ToString().Replace(JwtBodyContent.IdentityPrefix, "");
            unsignedData = Bytes.FromString(parts[0] + "." + parts[1]);
            stringRepresentation = jwtStr;
        }

        /// <summary>
        /// String representation of jwt.
        /// </summary>
        public override string ToString()
        {
            return stringRepresentation;
        }

        /// <summary>
        /// String representation of jwt without signature.
        /// It equals to:
        ///  base64UrlEncode(JWT Header) + "." + base64UrlEncode(JWT Body) 
        /// </summary>
        public byte[] Unsigned()
        {
            return unsignedData;
        }

        /// <summary>
        ///  Whether or not token is expired.
        /// </summary>
        public bool IsExpired()
        {
            return DateTime.UtcNow >= this.BodyContent.ExpiresAt;
        }

        private string HeaderBase64()
        {
            return Base64Url.Encode(Bytes.FromString(Configuration.Serializer.Serialize(this.HeaderContent)));
        }

        private string BodyBase64()
        {
            return Base64Url.Encode(Bytes.FromString(Configuration.Serializer.Serialize(this.BodyContent)));
        }

        private string SignatureBase64()
        {
            return Base64Url.Encode(this.SignatureData);
        }

        /// <summary>
        /// Jwt identity.
        /// </summary>
        /// <returns></returns>
        public string Identity()
        {
            return BodyContent.Identity;
        }
    }
}
