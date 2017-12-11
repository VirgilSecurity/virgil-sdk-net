
using NeoSmart.Utils;
using Virgil.CryptoApi;
using Virgil.SDK.Common;

namespace Virgil.SDK.Shared.Web.Authorization
{

    public class JsonWebToken
    {
        public JsonWebTokenHeader Header { get; private set; }
        public JsonWebTokenBody Body { get; private set; }
        public byte[] Signature { get; private set; }

        public JsonWebToken(string accId, string[] appIds, string version)
        {
            this.Header = new JsonWebTokenHeader("VIRGIL", "JWT");
            this.Body = new JsonWebTokenBody(accId, appIds, version);
        }

        public void SignBy(ICrypto crypto, IPrivateKey privateKey)
        {
            this.Signature = crypto.GenerateSignature(
                Bytes.FromString(this.HeaderBase64() + "." + this.BodyBase64()),
                privateKey);
        }

        public override string ToString()
        {
            return this.HeaderBase64() + "." + this.BodyBase64() + "." + this.SignatureBase64();
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
