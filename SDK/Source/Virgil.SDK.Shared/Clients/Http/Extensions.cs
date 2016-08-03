namespace Virgil.SDK.Http
{
    using System;
    using System.Net.Http;

    internal static class Extensions
    {
        internal static HttpMethod GetMethod(this RequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case RequestMethod.Get: return HttpMethod.Get;
                case RequestMethod.Post: return HttpMethod.Post;
                case RequestMethod.Put: return HttpMethod.Put;
                case RequestMethod.Delete: return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestMethod));
            }
        }
    }
}