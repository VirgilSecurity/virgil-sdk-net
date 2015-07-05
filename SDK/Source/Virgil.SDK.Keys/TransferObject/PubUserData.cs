namespace Virgil.SDK.Keys.TransferObject
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    internal class PubUserData
    {
        [JsonProperty("id")]
        public PubIdBundle Id { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("is_confirmed")]
        public bool Confirmed { get; set; }

        [JsonProperty("signs")]
        public List<PubSign> Signs { get; set; }
    }
}