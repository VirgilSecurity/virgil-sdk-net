using System;
using Newtonsoft.Json;

namespace Virgil.SDK.Keys.TransferObject
{
    public class PublicKeyDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }
    }
}