namespace Virgil.SDK.Http
{
    using System.Collections.Generic;

    public class Request : IRequest
    {
        public Request()
        {
            this.Headers = new Dictionary<string, string>();
        }

        public string Endpoint { get; set; }
        public string Body { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public RequestMethod Method { get; set; }

        internal static Request Create(RequestMethod method)
        {
            return new Request {Method = method};
        }
    }
}