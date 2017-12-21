
using System;
using System.Security.Cryptography;
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
            ValidateSignatureGenerator(jwtSignatureGenerator);

            this.Body = jwtBody ?? throw new ArgumentNullException(nameof(jwtBody));
            this.Header = new JsonWebTokenHeader("VIRGIL", "JWT");
            this.SignatureGenerator = jwtSignatureGenerator;
            this.UpdateSignature();
        }

        private JsonWebToken(JsonWebTokenHeader header, JsonWebTokenBody jwtBody, byte[] signature)
        {
            this.Header = header ?? throw new ArgumentNullException(nameof(header));
            this.Body = jwtBody ?? throw new ArgumentNullException(nameof(jwtBody));
            this.Signature = signature;
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

        private string HeaderBase64( )
        {
            return UrlBase64.Encode(Bytes.FromString(Configuration.Serializer.Serialize(this.Header)));
        }

        private string BodyBase64()
        {
            return UrlBase64.Encode(Bytes.FromString(Configuration.Serializer.Serialize(this.Body)));
        }

        private string SignatureBase64()
        {
            return UrlBase64.Encode(this.Signature);
        }

        public static JsonWebToken From(string jwt)
        {
            var parts = jwt.Split(new char[]{'.'});
            if (parts.Length != 3)
            {
                throw new ArgumentException("Wrong JWT format.");
            }

            try
            {
                var headerJson = Bytes.ToString(UrlBase64.Decode(parts[0]));
                var header = Configuration.Serializer.Deserialize<JsonWebTokenHeader>(headerJson);

                var bodyJson = Bytes.ToString(UrlBase64.Decode(parts[1]));
                var body = Configuration.Serializer.Deserialize<JsonWebTokenBody>(bodyJson);

                var signature = UrlBase64.Decode(parts[2]);
                return new JsonWebToken(header, body, signature);
            }
            catch (Exception)
            {
                throw new ArgumentException("Wrong JWT format.");
            }

        }

    }
}
