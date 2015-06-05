namespace Virgil.PKI.Dtos
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class PkiAccount
    {
        [JsonProperty("id")]
        public PkiIdBundle Id { get; set; }

        [JsonProperty("public_keys")]
        public List<PkiPublicKey> PublicKeys { get; set; }
    }
}