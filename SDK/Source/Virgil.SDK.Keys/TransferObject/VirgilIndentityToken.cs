namespace Virgil.SDK.Keys.TransferObject
{
    using Newtonsoft.Json;

    public class VirgilIndentityToken
    {
        [JsonProperty("identity_token")]
        public string Token { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}