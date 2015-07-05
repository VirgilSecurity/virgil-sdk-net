namespace Virgil.SDK.PrivateKeys.Http
{
    using System.Collections.Generic;

    public interface IRequest
    {
        /// <summary>
        ///     Gets the endpoint. Does not include server base address
        /// </summary>
        /// <value>
        ///     The endpoint.
        /// </value>
        string Endpoint { get; }

        /// <summary>
        ///     Gets the request method.
        /// </summary>
        /// <value>
        ///     The method.
        /// </value>
        RequestMethod Method { get; }

        /// <summary>
        ///     Gets the http headers.
        /// </summary>
        /// <value>
        ///     The headers.
        /// </value>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        ///     Gets the requests body.
        /// </summary>
        /// <value>
        ///     The body.
        /// </value>
        string Body { get; }
    }
}