namespace Virgil.SDK.Keys.TransferObject
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    internal class PubTicketExpanded
    {
        [JsonProperty("id")]
        public PubIdBundle Id { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("signs")]
        public List<PubSign> Signs { get; set; }

        [JsonProperty("expanded")]
        public PubTicketExpand Expanded { get; set; }
    }
}