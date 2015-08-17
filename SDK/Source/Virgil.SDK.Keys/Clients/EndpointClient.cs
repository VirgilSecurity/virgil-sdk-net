namespace Virgil.SDK.Keys.Clients
{
    using System.Threading.Tasks;
    
    using Newtonsoft.Json;

    using Virgil.SDK.Keys.Http;

    /// <summary>
    /// Base class for all API clients.
    /// </summary>
    public abstract class EndpointClient
    {
        protected readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected EndpointClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult" />
        /// </summary>
        protected async Task<TResult> Send<TResult>(IRequest request)
        {
            IResponse result = await Connection.Send(request);
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP request.
        /// </summary>
        protected async Task Send(IRequest request)
        {
            await Connection.Send(request);
        }
    }
}