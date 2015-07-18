namespace Virgil.SDK.PrivateKeys.Clients
{
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    
    using Virgil.SDK.PrivateKeys.Http;

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
        /// Performs an asynchronous HTTP GET request.
        /// Attempts to map the response to an object of type <typeparamref name="TResult" />
        /// </summary>
        /// <typeparam name="TResult">The type to map the response to</typeparam>
        /// <param name="endpoint">URI endpoint to send request to</param>
        protected async Task<TResult> Get<TResult>(string endpoint)
        {
            IResponse result = await Connection.Send(Request.Get(endpoint));
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult" />
        /// </summary>
        /// <typeparam name="TResult">The type to map the response to</typeparam>
        /// <param name="endpoint">URI endpoint to send request to</param>
        /// <param name="body">The object to serialize as the body of the request</param>
        protected async Task<TResult> Post<TResult>(string endpoint, object body)
        {
            string content = JsonConvert.SerializeObject(body);
            IResponse result = await Connection.Send(Request.Post(endpoint, content));
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP PUT request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult"/>
        /// </summary>
        /// <typeparam name="TResult">The type to map the response to</typeparam>
        /// <param name="endpoint">URI endpoint to send request to</param>
        /// <param name="body">The body of the request</param>
        public async Task<TResult> Put<TResult>(string endpoint, object body)
        {
            var result = await this.Connection.Send(Request.Get(endpoint));
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="endpoint">URI endpoint to send request to</param>
        public async Task Delete(string endpoint)
        {
            await this.Connection.Send(Request.Get(endpoint));
        }
    }
}