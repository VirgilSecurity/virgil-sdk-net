namespace Virgil.SDK.Clients
{
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Virgil.SDK.Http;

    /// <summary>
    /// Provides a base implementation of HTTP client for the Virgil Security services.
    /// </summary>
    public abstract class EndpointClient
    {
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
        protected virtual async Task<TResult> Send<TResult>(IRequest request)
        {
            var result = await this.Connection.Send(request).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP request.
        /// </summary>
        protected virtual async Task<IResponse> Send(IRequest request)
        {
            return await this.Connection.Send(request).ConfigureAwait(false);
        }

        
    }
}