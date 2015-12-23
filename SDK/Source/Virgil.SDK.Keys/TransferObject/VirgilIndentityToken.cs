namespace Virgil.SDK.Keys.TransferObject
{
    using Newtonsoft.Json;

    public class VirgilIndentityToken
    {
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public IdentityType Type { get; set; }
    }
}