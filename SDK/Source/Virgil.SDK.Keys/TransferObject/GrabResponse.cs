namespace Virgil.SDK.Keys.TransferObject
{
    using System;
    using Newtonsoft.Json;

    public class GrabResponse
    {
        [JsonProperty("public_key_id")]
        public Guid PublicKeyId { get; set; }

        [JsonProperty("private_key")]
        public byte[] PrivateKey { get; set; }
    }
}