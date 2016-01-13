namespace Virgil.SDK.Clients
{
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Virgil.SDK.Http;

    /// <summary>
    /// Provides a base implementation of HTTP client for the Virgil Security services which provide response signature.
    /// </summary>
    public abstract class ResponseVerifyClient : EndpointClient
    {
        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult" />
        /// </summary>
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
            var publicKey = virgilCardDto.PublicKey.PublicKey;
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
    }
}