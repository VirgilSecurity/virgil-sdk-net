using System;
using NeoSmart.Utils;
using Virgil.SDK.Common;

namespace Virgil.SDK.Web.Authorization
{
    internal class JwtParser
    {
        public static Jwt Parse(string jwt)
        {
            var parts = jwt.Split(new char[] { '.' });
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
                body.Identity = body.Subject.Clone().ToString().Replace(JwtBodyContent.IdentityPrefix, ""); ;
                var signature = UrlBase64.Decode(parts[2]);
                return new Jwt(header, body) {SignatureData = signature,  };
            }
            catch (Exception)
            {
                throw new ArgumentException("Wrong JWT format.");
            }

        }
    }
}