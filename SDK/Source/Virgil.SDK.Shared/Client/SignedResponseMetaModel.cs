namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class SignedResponseMetaModel
    {
        [JsonProperty("signs")]
        public Dictionary<string, byte[]> Signatures { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("card_version")]
        public string Version { get; set; }
    }
}