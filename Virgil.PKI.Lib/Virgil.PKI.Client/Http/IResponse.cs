namespace Virgil.PKI.Http
{
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Represents a generic HTTP response
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Raw response body. Typically a string, but when requesting images, it will be a byte array.
        /// </summary>
        object Body { get; }

        /// <summary>
        /// Information about the API.
        /// </summary>
        IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>
        /// The response status code.
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The content type of the response.
        /// </summary>
        string ContentType { get; }
    }
}