namespace Virgil.SDK.Keys.Dtos
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class PkiTicketExpanded
    {
        [JsonProperty("id")]
        public PkiIdBundle Id { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("signs")]
        public List<PkiSign> Signs { get; set; }

        [JsonProperty("expanded")]
        public PkiTicketExpand Expanded { get; set; }
    }
}