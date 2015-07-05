namespace Virgil.SDK.Keys.TransferObject
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class PubPublicKey
    {
        [JsonProperty("id")]
        public PubIdBundle Id { get; set; }

        [JsonProperty("public_key")]
        public byte[] Key { get; set; }

        [JsonProperty("user_data")]
        public List<PubUserData> UserData { get; set; }
    }
}