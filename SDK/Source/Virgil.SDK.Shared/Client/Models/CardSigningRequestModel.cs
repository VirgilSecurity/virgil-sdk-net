namespace Virgil.SDK.Client.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class CardSigningRequestModel
    {
        [JsonProperty("identity")]
        public string Identity { get; set; }

        [JsonProperty("identity_type")]
        public string IdentityType { get; set; }

        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
        
        [JsonProperty("data")]
        public IDictionary<string, string> Data { get; set; }
    }
}