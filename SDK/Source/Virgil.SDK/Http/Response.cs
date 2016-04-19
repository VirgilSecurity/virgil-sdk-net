namespace Virgil.SDK.Http
{
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="IResponse"/> default implementation
    /// </summary>
    internal class Response : IResponse
    {
        /// <summary>
        /// Raw response body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Information about the API.
        /// </summary>
        public IReadOnlyDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// The response status code.
        /// </summary>
        public int StatusCode { get; set; }
    }
}