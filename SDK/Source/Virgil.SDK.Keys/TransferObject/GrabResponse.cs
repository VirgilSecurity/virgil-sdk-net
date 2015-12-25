namespace Virgil.SDK.Keys.TransferObject
{
    using System;
    using Newtonsoft.Json;

    public class GrabResponse
    {
        [JsonProperty("virgil_card_id")]
        public Guid VirgilCardId { get; set; }

        [JsonProperty("private_key")]
        public byte[] PrivateKey { get; set; }
    }
}