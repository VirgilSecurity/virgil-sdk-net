namespace Virgil.SDK.TransferObject
{
    using System;
    using Newtonsoft.Json;

    public class VirgilVerifyResponse
    {
        [JsonProperty("action_id")]
        public Guid ActionId { get; set; }
    }
}