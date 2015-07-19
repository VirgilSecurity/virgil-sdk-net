namespace Virgil.SDK.PrivateKeys.TransferObject
{
    using System;

    using Newtonsoft.Json;

    public class AddPrivateKeyResult
    {
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("public_key_id")]
        public Guid PublicKeyId { get; set; }

        [JsonProperty("private_key")]
        public byte[] PrivateKey { get; set; }
    }
}