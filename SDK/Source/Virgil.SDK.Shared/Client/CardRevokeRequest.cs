namespace Virgil.SDK.Client
{
    using Newtonsoft.Json;

    public class CardRevokeRequest : ClientRequest
    {
        /// <summary>
        /// Gets or sets the card identifier.
        /// </summary>
        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        /// <summary>   
        /// Gets or sets the reason.
        /// </summary>
        [JsonProperty("revocation_reason")]
        public RevocationReason Reason { get; set; }
    }
}