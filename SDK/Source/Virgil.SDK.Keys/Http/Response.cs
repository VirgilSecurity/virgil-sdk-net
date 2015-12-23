namespace Virgil.SDK.Keys.Http
{
    using System.Collections.Generic;
    using System.Net;

    public class Response : IResponse
    {
        public string Body { get; set; }
        public IReadOnlyDictionary<string, string> Headers { get; set; }
        public int StatusCode { get; set; }
    }
}