namespace Virgil.SDK.Client.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class VirgilCardModel 
    {
        [JsonProperty("identity")]
        public string Identity { get; set; }

        [JsonProperty("identity_type")]
        public string IdentityType { get; set; }

        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }
    }
}