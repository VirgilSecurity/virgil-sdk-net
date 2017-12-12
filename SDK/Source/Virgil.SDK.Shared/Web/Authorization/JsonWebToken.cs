
using System;
using NeoSmart.Utils;
using Virgil.CryptoApi;
using Virgil.SDK.Common;

namespace Virgil.SDK.Shared.Web.Authorization
{

    public class JsonWebToken
    {
        public JsonWebTokenHeader Header { get; private set; }
        public JsonWebTokenBody Body { get; private set; }
        public JsonWebTokenSignatureGenerator SignatureGenerator { get; private set; }
        public byte[] Signature { get; private set; }

        public JsonWebToken(JsonWebTokenBody jwtBody, JsonWebTokenSignatureGenerator jwtSignatureGenerator)
        {
            if (jwtBody == null)
            {
                throw new ArgumentNullException(nameof(jwtBody));
            }
            ValidateSignatureGenerator(jwtSignatureGenerator);

            this.Header = new JsonWebTokenHeader("VIRGIL", "JWT");
            this.Body = jwtBody;
            this.SignatureGenerator = jwtSignatureGenerator;
            this.Body.Refresh();
            this.UpdateSignature();
        }

        private static void ValidateSignatureGenerator(JsonWebTokenSignatureGenerator jwtSignatureGenerator)
        {
            if (jwtSignatureGenerator == null)
            {
                throw new ArgumentNullException(nameof(jwtSignatureGenerator));
            }
            if (jwtSignatureGenerator.Crypto == null)
            {
                throw new ArgumentNullException(nameof(jwtSignatureGenerator.Crypto));
            }

            if (jwtSignatureGenerator.PrivateKey == null)
            {
                throw new ArgumentNullException(nameof(jwtSignatureGenerator.PrivateKey));
            }
        }

        private void UpdateSignature()
        {
            var unsigned = Bytes.FromString(this.HeaderBase64() + "." + this.BodyBase64());
            this.Signature = this.SignatureGenerator.Crypto.GenerateSignature(
                unsigned,
                this.SignatureGenerator.PrivateKey);
        }

        public override string ToString()
        {
            return this.HeaderBase64() + "." + this.BodyBase64() + "." + this.SignatureBase64();
        }

        public bool IsExpired()
        {
            return this.Body.IsExpired();
        }
        public void Refresh()
        {
            this.Body.Refresh();
            this.UpdateSignature();
        }

        private string HeaderBase64( )
        {
            return Bytes.ToString(Bytes.FromString(Configuration.Serializer.Serialize(this.Header)),
                StringEncoding.BASE64);
        }

        private string BodyBase64()
        {
            return UrlBase64.Encode(Bytes.FromString(Configuration.Serializer.Serialize(this.Body)));
        }

        private string SignatureBase64()
        {
            return UrlBase64.Encode(this.Signature);
        }

    }
}
