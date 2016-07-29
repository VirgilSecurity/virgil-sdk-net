namespace Virgil.SDK.Http
{
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="IRequest" /> default implementation"/>
    /// </summary>
    /// <seealso cref="IRequest" />
    internal class Request : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        public Request()
        {
            this.Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the endpoint. Does not include server base address
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Gets the requests body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets the http headers.
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets the request method.
        /// </summary>
        public RequestMethod Method { get; set; }

        internal static Request Create(RequestMethod method)
        {
            return new Request {Method = method};
        }
    }
}