namespace Virgil.SDK.PrivateKeys.TransferObject
{
    using System;
    using Newtonsoft.Json;

    public class GetPrivateKeyByIdResult
    {
        [JsonProperty("public_key_id")]
        public Guid PublicKeyId { get; set; }

        [JsonProperty("private_key")]
        public byte[] PrivateKey { get; set; }
    }
}