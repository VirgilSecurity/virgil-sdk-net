namespace Virgil.PKI.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.PKI.Http;

    /// <summary>
    /// Base class for all API clients.
    /// </summary>
    public abstract class ApiClient
    {
        private IConnection connection;

        protected ApiClient(IConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Performs an asynchronous HTTP GET request.
        /// Attempts to map the response to an object of type <typeparamref name="TResult"/>
        /// </summary>
        /// <typeparam name="TResult">The type to map the response to</typeparam>
        /// <param name="endpoint">URI endpoint to send request to</param>
        /// <param name="parameters">Querystring parameters for the request</param>
        public Task<TResult> Get<TResult>(string endpoint, IDictionary<string, string> parameters)
        {
            throw  new NotImplementedException();
        }

        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult"/>
        /// </summary>
        /// <typeparam name="TResult">The type to map the response to</typeparam>
        /// <param name="endpoint">URI endpoint to send request to</param>
        /// <param name="body">The object to serialize as the body of the request</param>
        public Task<TResult> Post<TResult>(string endpoint, object body)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs an asynchronous HTTP PUT request.
        /// Attempts to map the response body to an object of type <typeparamref name="TResult"/>
        /// </summary>
        /// <typeparam name="TResult">The type to map the response to</typeparam>
        /// <param name="endpoint">URI endpoint to send request to</param>
        /// <param name="body">The body of the request</param>
        public Task<TResult> Put<TResult>(string endpoint, object body)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="endpoint">URI endpoint to send request to</param>
        public void Delete(string endpoint)
        {
            throw new NotImplementedException();
        }
    }
}