namespace Virgil.PKI.Dtos
{
    using System;
    using Newtonsoft.Json;

    public class PkiTicketDetails
    {
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("unique_id")]
        public string UniqueId { get; set; }

        [JsonProperty("public_key_id")]
        public Guid PublicKeyId { get; set; }

        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }
    }
}