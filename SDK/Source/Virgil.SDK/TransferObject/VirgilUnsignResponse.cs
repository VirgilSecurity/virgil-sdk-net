namespace Virgil.SDK.TransferObject
{
    using System;
    using Newtonsoft.Json;

    public class VirgilUnsignResponse
    {
        [JsonProperty("signed_virgil_card_id")]
        public Guid SignedVirgilCardId { get; set; }
    }
}