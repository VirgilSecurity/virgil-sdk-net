using Newtonsoft.Json.Converters;

namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Text;
    using Crypto;
    using Newtonsoft.Json;

    public static class RequestExtensions
    {
        private const string RequestSignHeader = "X-VIRGIL-REQUEST-SIGN";
        private const string RequestSignVirgilCardIdHeader = "X-VIRGIL-REQUEST-SIGN-VIRGIL-CARD-ID";
        private const string RequestUUIDHeader = "X-VIRGIL-REQUEST-UUID";

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Converters =
            {
                new StringEnumConverter()
            }
        };

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
            request.Body = JsonConvert.SerializeObject(body, Settings);
            return request;
        }
        
        public static Request SignRequest(this Request request, byte[] privateKey, Guid virgilCardId)
        {
            using (var signer = new VirgilSigner())
            {
                var uuid = Guid.NewGuid().ToString().ToLowerInvariant();

                var signBase64 = Convert.ToBase64String(signer.Sign(Encoding.UTF8.GetBytes(uuid + request.Body), privateKey));

                request.Headers.Add(RequestUUIDHeader, uuid);
                request.Headers.Add(RequestSignHeader, signBase64);
                request.Headers.Add(RequestSignVirgilCardIdHeader, virgilCardId.ToString().ToLowerInvariant());
            }

            return request;
        }

        public static Request SignRequest(this Request request, byte[] privateKey)
        {
            using (var signer = new VirgilSigner())
            {
                var uuid = Guid.NewGuid().ToString().ToLowerInvariant();

                var signBase64 = Convert.ToBase64String(signer.Sign(Encoding.UTF8.GetBytes(uuid + request.Body), privateKey));

                request.Headers.Add(RequestUUIDHeader, uuid);
                request.Headers.Add(RequestSignHeader, signBase64);
            }

            return request;
        }
    }
}