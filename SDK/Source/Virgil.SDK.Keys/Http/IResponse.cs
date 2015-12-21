namespace Virgil.SDK.Keys.Http
{
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    ///     Represents a generic HTTP response
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        ///     Raw response body.
        /// </summary>
        string Body { get; }

        /// <summary>
        ///     Information about the API.
        /// </summary>
        IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>
        ///     The response status code.
        /// </summary>
        HttpStatusCode StatusCode { get; }

        ///// </summary>
        ///// The content type of the response.

        ///// <summary>
        //string ContentType { get; }
    }
}