namespace Virgil.SDK.Http
{
    using System.Collections.Generic;

    public class Response : IResponse
    {
        public string Body { get; set; }
        public IReadOnlyDictionary<string, string> Headers { get; set; }
        public int StatusCode { get; set; }
    }
}