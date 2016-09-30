namespace Virgil.SDK.Client
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class CreateCardModel
    {
        [JsonProperty("identity")]
        public string Identity { get; set; }

        [JsonProperty("identity_type")]
        public string IdentityType { get; set; }

        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }

        [JsonProperty("scope")]
        public CardScope Scope { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }

        [JsonProperty("info")]
        public CardInfo Info { get; set; }
    }
}