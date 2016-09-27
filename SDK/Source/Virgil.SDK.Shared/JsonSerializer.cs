namespace Virgil.SDK
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal class JsonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Converters =
            {
                new StringEnumConverter()
            }
        };

        public static string Serialize(object model)
        {
            return JsonConvert.SerializeObject(model, Settings);
        }

        public static TModel Deserialize<TModel>(string json)
        {
            return JsonConvert.DeserializeObject<TModel>(json, Settings);
        }
    }
}