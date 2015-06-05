namespace Virgil.PKI.Dtos
{
    using System;
    using Newtonsoft.Json;

    public class PkiIdBundle
    {
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("public_key_id")]
        public Guid PublicKeyId { get; set; }

        [JsonProperty("user_data_id")]
        public Guid UserDataId { get; set; }
    }
}