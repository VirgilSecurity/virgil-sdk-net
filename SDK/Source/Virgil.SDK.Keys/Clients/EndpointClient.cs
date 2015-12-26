namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Domain;
    using Exceptions;
    using Http;
    using Infrastructurte;
    using Newtonsoft.Json;

    /// <summary>
    ///     Base class for all API clients.
    /// </summary>
    public abstract class EndpointClient
    {
        const string SIGN_ID_HEADER = "X-VIRGIL-RESPONSE-ID";
        const string SIGN_HEADER = "X-VIRGIL-RESPONSE-SIGN";

        protected readonly IConnection Connection;

        protected Guid EndpointPublicKeyId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EndpointClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected EndpointClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        ///     Performs an asynchronous HTTP POST request.
        ///     Attempts to map the response body to an object of type <typeparamref name="TResult" />
        /// </summary>
        protected async Task<TResult> Send<TResult>(IRequest request)
        {
            var result = await this.Connection.Send(request);
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        ///     Performs an asynchronous HTTP request.
        /// </summary>
        protected virtual async Task<IResponse> Send(IRequest request)
        {
            return await this.Connection.Send(request);
        }
      

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