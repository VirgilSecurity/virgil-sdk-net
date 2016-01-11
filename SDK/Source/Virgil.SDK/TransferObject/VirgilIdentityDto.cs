namespace Virgil.SDK.TransferObject
{
    using System;
    using Newtonsoft.Json;

    public class VirgilIdentityDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("type")]
        public IdentityType Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }
    }
}