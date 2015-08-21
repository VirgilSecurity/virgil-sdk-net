namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Text;
    using Crypto;
    using Newtonsoft.Json;

    public static class RequestExtensions
    {
        private const string RequestSignHeader = "X-VIRGIL-REQUEST-SIGN";
        private const string RequestSignPublicKeyIdHeader = "X-VIRGIL-REQUEST-SIGN-PK-ID";

        public static Request WithEndpoint(this Request request, string endpoint)
        {
            request.Endpoint = endpoint;
            return request;
        }

        public static Request WithHeader(this Request request, string key, string value)
        {
            request.Headers.Add(key, value);
            return request;
        }

        public static Request WithBody(this Request request, object body)
        {
            request.Body = JsonConvert.SerializeObject(body);
            return request;
        }
        
        public static Request SignRequest(this Request request, byte[] privateKey, Guid publicKeyId)
        {
            using (var signer = new VirgilSigner())
            {
                var signBase64 = Convert.ToBase64String(signer.Sign(Encoding.UTF8.GetBytes(request.Body), privateKey));
                request.Headers.Add(RequestSignHeader, signBase64);
                request.Headers.Add(RequestSignPublicKeyIdHeader, publicKeyId.ToString());
            }

            return request;
        }

        public static Request SignRequest(this Request request, byte[] privateKey)
        {
            using (var signer = new VirgilSigner())
            {
                var signBase64 = Convert.ToBase64String(signer.Sign(Encoding.UTF8.GetBytes(request.Body), privateKey));
                request.Headers.Add(RequestSignHeader, signBase64);
            }

            return request;
        }
    }
}