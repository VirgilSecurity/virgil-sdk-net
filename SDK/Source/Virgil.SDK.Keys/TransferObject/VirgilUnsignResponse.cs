using System;
using Newtonsoft.Json;

namespace Virgil.SDK.Keys.TransferObject
{
    public class VirgilUnsignResponse
    {
        [JsonProperty("signed_virgil_card_id")]
        public Guid SignedVirgilCardId { get; set; }
    }
}