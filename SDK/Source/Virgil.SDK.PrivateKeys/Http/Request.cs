namespace Virgil.SDK.PrivateKeys.Http
{
    using System.Collections.Generic;

    public class Request : IRequest
    {
        public string Endpoint { get; set; }

        public string Body { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public RequestMethod Method { get; set; }
    }
}