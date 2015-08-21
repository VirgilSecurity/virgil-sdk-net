namespace Virgil.SDK.Keys.TransferObject
{
    using Newtonsoft.Json;

    internal class ResetPublicKeyResponse
    {
        [JsonProperty("action_token")]
        public string Token { get; set; }

        [JsonProperty("user_ids")]
        public string[] UserIds { get; set; }
    }

    internal class ResetPublicKeyConfirmation
    {
        [JsonProperty("action_token")]
        public string Token { get; set; }

        [JsonProperty("confirmation_codes")]
        public string[] ConfirmationCodes { get; set; }
    }
}