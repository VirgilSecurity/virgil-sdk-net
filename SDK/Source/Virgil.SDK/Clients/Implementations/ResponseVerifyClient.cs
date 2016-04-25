namespace Virgil.SDK.Clients
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Exceptions;
    using Newtonsoft.Json;
    using Virgil.SDK.Http;

    /// <summary>
    /// Provides a base implementation of HTTP client for the Virgil Security services which provide response signature.
    /// </summary>
    internal abstract class ResponseVerifyClient : EndpointClient
    {
        const string SIGN_ID_HEADER = "X-VIRGIL-RESPONSE-ID";
        
        const string SIGN_HEADER = "X-VIRGIL-RESPONSE-SIGN";

        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult" />
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<TResult> Send<TResult>(IRequest request)
        {
            var result = await this.Send(request).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP request.
        /// </summary>
        protected override async Task<IResponse> Send(IRequest request)
        {
            var response = await this.Connection.Send(request).ConfigureAwait(false);
            var virgilCardDto = await this.Cache.GetServiceCard(this.EndpointApplicationId).ConfigureAwait(false);
            var publicKey = virgilCardDto.PublicKey.Value;
            this.VerifyResponse(response, publicKey);
            return response;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseVerifyClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected ResponseVerifyClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseVerifyClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The service key cache.</param>
        protected ResponseVerifyClient(IConnection connection, IServiceKeyCache cache) : base(connection, cache)
        {
        }

        /// <summary>
        /// Verifies the HTTP response with specified public key.
        /// </summary>
        /// <param name="nativeResponse">An instance of HTTP response.</param>
        /// <param name="publicKey">A public key to be used for verification.</param>
        protected virtual void VerifyResponse(IResponse nativeResponse, byte[] publicKey)
        {
            var headers = nativeResponse.Headers;
            var content = nativeResponse.Body;

            var signId = headers.FirstOrDefault(it => it.Key == SIGN_ID_HEADER).Value;
            var signBase64 = headers.FirstOrDefault(it => it.Key == SIGN_HEADER).Value;

            if (string.IsNullOrWhiteSpace(signId))
            {
                throw new ServiceSignVerificationException($"{SIGN_ID_HEADER} header was not found in service response");
            }

            if (string.IsNullOrWhiteSpace(signBase64))
            {
                throw new ServiceSignVerificationException($"{SIGN_HEADER} header was not found in service response");
            }

            byte[] sign;

            try
            {
                sign = Convert.FromBase64String(signBase64);
            }
            catch (FormatException)
            {
                throw new ServiceSignVerificationException($"{SIGN_HEADER} header is not a base 64 string");
            }

            var signed = Encoding.UTF8.GetBytes(signId + content);

            using (var signer = new VirgilSigner())
            {
                var verify = signer.Verify(signed, sign, publicKey);

                if (!verify)
                {
                    throw new ServiceSignVerificationException("Request sign verification error");
                }
            }
        }
    }
}