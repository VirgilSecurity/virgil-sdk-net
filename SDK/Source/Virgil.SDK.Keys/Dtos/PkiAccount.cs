namespace Virgil.SDK.Keys.Dtos
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class PkiAccount
    {
        [JsonProperty("id")]
        public PkiIdBundle Id { get; set; }

        [JsonProperty("public_keys")]
        public List<PkiPublicKey> PublicKeys { get; set; }
    }
}