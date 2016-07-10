namespace Virgil.SDK.Http
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a generic HTTP response
    /// </summary>
    internal interface IResponse
    {
        /// <summary>
        /// Raw response body.
        /// </summary>
        string Body { get; }

        /// <summary>
        /// Information about the API.
        /// </summary>
        IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>
        /// The response status code.
        /// </summary>
        int StatusCode { get; }
    }
}