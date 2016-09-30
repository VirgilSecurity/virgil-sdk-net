namespace Virgil.SDK.Client
{
    using Newtonsoft.Json;

    internal class SignedResponseModel
    {
        [JsonProperty("card_id")]
        public string CardId { get; set; }

        [JsonProperty("content_snapshot")]
        public byte[] ContentSnapshot { get; set; }

        [JsonProperty("meta")]
        public SignedResponseMetaModel Meta { get; set; }
    }
}