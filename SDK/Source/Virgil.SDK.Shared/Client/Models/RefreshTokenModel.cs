namespace Virgil.SDK.Client.Models
{
    using Newtonsoft.Json;

    public class RefreshTokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
