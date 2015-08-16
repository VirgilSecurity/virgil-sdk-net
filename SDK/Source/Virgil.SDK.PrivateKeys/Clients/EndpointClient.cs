namespace Virgil.SDK.PrivateKeys.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

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
        protected async Task<TResult> Get<TResult>(string endpoint, params KeyValuePair<string, string>[] headers)
        {
            var request = new Request
            {
                Endpoint = endpoint,
                Method = RequestMethod.Get,
                Headers = headers.ToDictionary(it => it.Key, it => it.Value)
            };

            IResponse result = await Connection.Send(request);
            return JsonConvert.DeserializeObject<TResult>(result.Body);
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