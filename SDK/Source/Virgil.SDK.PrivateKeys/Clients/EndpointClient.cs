using System.Collections.Generic;
using System.Linq;
using Virgil.SDK.PrivateKeys.Http;

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
        public const string RequestSignHeader = "X-VIRGIL-REQUEST-SIGN";
        public const string RequestSignPublicKeyIdHeader = "X-VIRGIL-REQUEST-SIGN-PK-ID";

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
        /// <typeparam name="TResult">The type to map the response to</typeparam>
        /// <param name="endpoint">URI endpoint to send request to</param>
        /// <param name="body">The object to serialize as the body of the request</param>
        /// <param name="headers">The HTTP headers list</param>
        protected async Task<TResult> Post<TResult>(string endpoint, object body, params KeyValuePair<string, string>[] headers)
        {
            string content = JsonConvert.SerializeObject(body);

            var request = new Request
            {
                Endpoint = endpoint,
                Method = RequestMethod.Post,
                Body = content,
                Headers = headers.ToDictionary(it => it.Key, it => it.Value)
            };

            IResponse result = await Connection.Send(request);
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP PUT request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult"/>
        /// </summary>
        /// <typeparam name="TResult">The type to map the response to</typeparam>
        /// <param name="endpoint">URI endpoint to send request to</param>
        /// <param name="body">The body of the request</param>
        protected async Task<TResult> Put<TResult>(string endpoint, object body, params KeyValuePair<string, string>[] headers)
        {
            var request = new Request
            {
                Body = JsonConvert.SerializeObject(body), 
                Endpoint = endpoint, 
                Method = RequestMethod.Put,
                Headers = headers.ToDictionary(it => it.Key, it => it.Value)
            };

            var result = await this.Connection.Send(request);
            return JsonConvert.DeserializeObject<TResult>(result.Body);
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="endpoint">URI endpoint to send request to</param>
        protected async Task Delete(string endpoint, object body, params KeyValuePair<string, string>[] headers)
        {
            string content = JsonConvert.SerializeObject(body);

            var request = new Request
            {
                Endpoint = endpoint,
                Method = RequestMethod.Delete,
                Body = content,
                Headers = headers.ToDictionary(it => it.Key, it => it.Value)
            };

            await this.Connection.Send(request);
        }
    }
}