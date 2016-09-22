namespace Virgil.SDK.Client.Http
{
    using Newtonsoft.Json;

    internal static class ResponseExtensions
    {
        public static TResult Parse<TResult>(this IResponse response)
        {
            return JsonConvert.DeserializeObject<TResult>(response.Body);
        }
    }
}