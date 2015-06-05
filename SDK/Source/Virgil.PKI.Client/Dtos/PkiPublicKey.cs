namespace Virgil.PKI.Dtos
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class PkiPublicKey
    {
        [JsonProperty("id")]
        public PkiIdBundle Id { get; set; }

        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }

        [JsonProperty("user_data")]
        public List<PkiUserData> UserData { get; set; }
    }
}