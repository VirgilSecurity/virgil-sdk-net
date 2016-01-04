namespace Virgil.SDK.Keys.Helpers
{
    using System.Diagnostics;
    using Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    internal static class DebugHelper
    {
        [Conditional("DEBUG")]
        internal static void PrintRequest(IRequest request)
        {
            var serializeObject = JsonConvert.SerializeObject(request, Formatting.Indented);
            Debug.WriteLine(serializeObject);
        }

        [Conditional("DEBUG")]
        internal static void PrintResponse(IResponse response)
        {
            var serializeObject = JsonConvert.SerializeObject(response, Formatting.Indented);
            Debug.WriteLine(serializeObject);
        }
    }
}