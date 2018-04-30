namespace Virgil.SDK.Common
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;

    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings settings;

        public NewtonsoftJsonSerializer()
        {
            this.settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            settings.Converters.Add(new UnixTimestampConverter());
        }

        public TModel Deserialize<TModel>(string json)
        {
            return JsonConvert.DeserializeObject<TModel>(json, settings);
        }

        public string Serialize(object model)
        {
            return JsonConvert.SerializeObject(model, settings);
        }
    }

    public class UnixTimestampConverter : DateTimeConverterBase
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(UnixTimestampFromDateTime((DateTime)value).ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value == null ? epoch : TimeFromUnixTimestamp((long)reader.Value);
        }

        private static DateTime TimeFromUnixTimestamp(long unixTimestamp)
        {
            long unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;
            return new DateTime(epoch.Ticks + unixTimeStampInTicks);
        }

        public static long UnixTimestampFromDateTime(DateTime date)
        {
            long unixTimestamp = date.Ticks - epoch.Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }
    }
}
