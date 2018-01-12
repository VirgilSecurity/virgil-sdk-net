
using System;
using System.Security.Cryptography;
using NeoSmart.Utils;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;

namespace Virgil.SDK.Web.Authorization
{

    public class Jwt : IAccessToken
    {
        public JwtHeaderContent HeaderContent { get; set; }

        public JwtBodyContent BodyContent { get; set; }

        public byte[] SignatureData { get; set; }

        internal Jwt(JwtHeaderContent jwtHeaderContent, 
            JwtBodyContent jwtBodyContent 
            )
        {
            this.BodyContent = jwtBodyContent ?? throw new ArgumentNullException(nameof(jwtBodyContent));
            this.HeaderContent = jwtHeaderContent ?? throw new ArgumentNullException(nameof(jwtHeaderContent));
        }

        public override string ToString()
        {
            var jwt = this.HeaderBase64() + "." + this.BodyBase64();
            if (SignatureData != null)
            {
                jwt += "." + this.SignatureBase64();
            }
            return jwt;
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow >= this.BodyContent.ExpiresAt;
        }

        private string HeaderBase64( )
        {
            return UrlBase64.Encode(Bytes.FromString(Configuration.Serializer.Serialize(this.HeaderContent)));
        }

        private string BodyBase64()
        {
            return UrlBase64.Encode(Bytes.FromString(Configuration.Serializer.Serialize(this.BodyContent)));
        }

        private string SignatureBase64()
        {
            return UrlBase64.Encode(this.SignatureData);
        }
  
        public string Identity()
        {
            return BodyContent.Identity;
        }
    }
}
