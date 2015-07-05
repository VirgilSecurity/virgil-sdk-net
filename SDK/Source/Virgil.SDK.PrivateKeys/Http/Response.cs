namespace Virgil.SDK.PrivateKeys.Http
{
    using System.Collections.Generic;
    using System.Net;

    public class Response : IResponse
    {
        public string Body { get; set; }
        public IReadOnlyDictionary<string, string> Headers { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}