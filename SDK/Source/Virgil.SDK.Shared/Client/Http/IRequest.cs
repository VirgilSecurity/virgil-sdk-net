namespace Virgil.SDK.Client.Http
{
    using System.Collections.Generic;

    /// <summary>
    /// Represent a generic HTTP request 
    /// </summary>
    internal interface IRequest
    {
        /// <summary>
        /// Gets the endpoint. Does not include server base address
        /// </summary>
        string Endpoint { get; }

        /// <summary>
        /// Gets the request method.
        /// </summary>
        RequestMethod Method { get; }

        /// <summary>
        /// Gets the http headers.
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Gets the requests body.
        /// </summary>
        string Body { get; }
    }
}