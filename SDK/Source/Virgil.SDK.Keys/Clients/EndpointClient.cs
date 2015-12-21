namespace Virgil.SDK.Keys.Clients
{
    using System.Threading.Tasks;
    using Http;
    using Newtonsoft.Json;

    /// <summary>
    ///     Base class for all API clients.
    /// </summary>
    public abstract class EndpointClient
    {
        protected readonly IConnection Connection;

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
        protected async Task<IResponse> Send(IRequest request)
        {
            return await this.Connection.Send(request);
        }
    }
}