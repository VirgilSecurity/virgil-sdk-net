namespace Virgil.SDK.Keys.TransferObject
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class PkiPublicKey
    {
        [JsonProperty("id")]
        public PkiIdBundle Id { get; set; }

        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }

        [JsonProperty("user_data")]
        public List<PkiUserData> UserData { get; set; }
    }
}