namespace Virgil.SDK.PrivateKeys.Http
{
    using System.Collections.Generic;

    public class Request : IRequest
    {
        public string Endpoint { get; set; }

        public string Body { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public RequestMethod Method { get; set; }

        public static Request Get(string url)
        {
            return new Request
            {
                Endpoint = url,
                Method = RequestMethod.Get
            };
        }

        public static Request Post(string url, string content)
        {
            return new Request
            {
                Endpoint = url,
                Method = RequestMethod.Post,
                Body = content
            };
        }
    }
}