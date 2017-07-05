namespace Virgil.SDK.Client.Http
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a generic HTTP response
    /// </summary>
    public interface IResponse
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