namespace Virgil.SDK.Client
{
    using Newtonsoft.Json;

    public class RevokeCardModel 
    {
        /// <summary>
        /// Gets or sets the card identifier.
        /// </summary>
        [JsonProperty("card_id")]
        public string CardId { get; set; }

        /// <summary>   
        /// Gets or sets the reason.
        /// </summary>
        [JsonProperty("revocation_reason")]
        public RevocationReason Reason { get; set; }
    }
}