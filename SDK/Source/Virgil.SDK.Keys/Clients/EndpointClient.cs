namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Virgil.Crypto;
    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Http;
    using Virgil.SDK.Keys.Infrastructure;

    /// <summary>
    /// Provides a base implementation of HTTP client for the Virgil Security services.
    /// </summary>
    public abstract class EndpointClient
    {
        const string SIGN_ID_HEADER = "X-VIRGIL-RESPONSE-ID";
        const string SIGN_HEADER = "X-VIRGIL-RESPONSE-SIGN";

        /// <summary>
        /// The connection
        /// </summary>
        protected readonly IConnection Connection;

        /// <summary>
        /// The endpoint application identifier
        /// </summary>
        protected string EndpointApplicationId;

        /// <summary>
        /// The cache
        /// </summary>
        protected IServiceKeyCache Cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected EndpointClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The service key cache.</param>
        protected EndpointClient(IConnection connection, IServiceKeyCache cache)
        {
            this.Connection = connection;
            this.Cache = cache;
        }

        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult" />
        /// </summary>
        protected async Task<TResult> Send<TResult>(IRequest request)
        {
            var result = await this.Connection.Send(request);
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP request.
        /// </summary>
        protected virtual async Task<IResponse> Send(IRequest request)
        {
            return await this.Connection.Send(request);
        }

        /// <summary>
        /// Verifies the HTTP response with specified public key.
        /// </summary>
        /// <param name="nativeResponse">An instance of HTTP response.</param>
        /// <param name="publicKey">A public key to be used for verification.</param>
        protected void VerifyResponse(IResponse nativeResponse, byte[] publicKey)
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

            var signed = (signId + content).GetBytes(Encoding.UTF8);

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