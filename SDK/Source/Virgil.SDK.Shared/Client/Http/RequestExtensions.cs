namespace Virgil.SDK.Http
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Extensions to help construct http requests
    /// </summary>
    internal static class RequestExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Converters =
            {
                new StringEnumConverter()
            }
        };

        /// <summary>
        /// Sets the request enpoint
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request WithEndpoint(this Request request, string endpoint)
        {
            request.Endpoint = endpoint;
            return request;
        }
        
        /// <summary>
        /// Withes the body.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="body">The body.</param>
        /// <returns><see cref="Request"/></returns>
        public static Request WithBody(this Request request, object body)
        {
            request.Body = JsonConvert.SerializeObject(body, Settings);
            return request;
        }
    }
}