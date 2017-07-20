namespace Virgil.SDK.Shared.Client.Models
{
    using Newtonsoft.Json;

    public class VerifyAccessTokenModel
    {
        [JsonProperty("resource_owner_virgil_card_id")]
        public string ResourceOwnerVirgilCardId { get; set; }
    }
}
