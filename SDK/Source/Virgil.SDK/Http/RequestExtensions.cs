namespace Virgil.SDK.Http
{
    using System;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Virgil.Crypto;
    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Extensions to help construct http requests
    /// </summary>
    public static class RequestExtensions
    {
        private const string RequestSignHeader = "X-VIRGIL-REQUEST-SIGN";
        private const string RequestSignVirgilCardIdHeader = "X-VIRGIL-REQUEST-SIGN-VIRGIL-CARD-ID";
        private const string RequestIdHeader = "X-VIRGIL-REQUEST-ID";
        private const string RequestSignPkUuidHeader = "X-VIRGIL-REQUEST-SIGN-PK-UUID";

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Converters =
            {
                new StringEnumConverter()
            }
        };

        /// <summary>
        /// Sets the request enpoint
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request WithEndpoint(this Request request, string endpoint)
        {
            request.Endpoint = endpoint;
            return request;
        }

        /// <summary>
        /// Adds new header to the headers collection
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="key">The header name.</param>
        /// <param name="value">The header value.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request WithHeader(this Request request, string key, string value)
        {
            request.Headers.Add(key, value);
            return request;
        }

        /// <summary>
        /// Withes the body.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="body">The body.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request WithBody(this Request request, object body)
        {
            request.Body = JsonConvert.SerializeObject(body, Settings);
            return request;
        }

        /// <summary>
        /// Signs the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cardId">The card identifier.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request SignRequest(this Request request, Guid cardId, byte[] privateKey, string privateKeyPassword = null)
        {
            using (var signer = new VirgilSigner())
            {
                var uuid = Guid.NewGuid().ToString().ToLowerInvariant();

                var requestSign = Encoding.UTF8.GetBytes(uuid + request.Body);

                byte[] sign = privateKeyPassword == null
                    ? signer.Sign(requestSign, privateKey)
                    : signer.Sign(requestSign, privateKey, Encoding.UTF8.GetBytes(privateKeyPassword));

                var signBase64 = Convert.ToBase64String(sign);

                request.Headers.Add(RequestIdHeader, uuid);
                request.Headers.Add(RequestSignHeader, signBase64);
                request.Headers.Add(RequestSignVirgilCardIdHeader, cardId.ToString().ToLowerInvariant());
            }

            return request;
        }

        /// <summary>
        /// Signs the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request SignRequest(this Request request, byte[] privateKey, string privateKeyPassword = null)
        {
            using (var signer = new VirgilSigner())
            {
                var uuid = Guid.NewGuid().ToString().ToLowerInvariant();

                var requestSign = Encoding.UTF8.GetBytes(uuid + request.Body);

                byte[] sign = privateKeyPassword == null
                   ? signer.Sign(requestSign, privateKey)
                   : signer.Sign(requestSign, privateKey, Encoding.UTF8.GetBytes(privateKeyPassword));

                var signBase64 = Convert.ToBase64String(sign);
                
                request.Headers.Add(RequestIdHeader, uuid);
                request.Headers.Add(RequestSignHeader, signBase64);
            }

            return request;
        }

        /// <summary>
        /// Sets the public key UUID.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request WithPublicKeyUuid(this Request request, Guid publicKeyId)
        {
            request.Headers.Add(RequestSignPkUuidHeader, publicKeyId.ToString().ToLowerInvariant());
            return request;
        }

        /// <summary>
        /// Encrypts the json body.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="dto">The dto.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request EncryptJsonBody(this Request request, VirgilCardDto dto)
        {
            using (var cipher = new VirgilCipher())
            {
                cipher.AddKeyRecipient(Encoding.UTF8.GetBytes(dto.Id.ToString()), dto.PublicKey.PublicKey);
                request.Body = Convert.ToBase64String(cipher.Encrypt(Encoding.UTF8.GetBytes(request.Body), true));
            }

            return request;
        }
    }
}