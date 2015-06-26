namespace Virgil.SDK.Keys.Dtos
{
    using Newtonsoft.Json;

    internal class PkiTicketData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("is_confirmed")]
        public bool Confirmed { get; set; }
    }
}