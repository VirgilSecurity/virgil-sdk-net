namespace Virgil.SDK.Keys.TransferObject
{
    using System.Collections.Generic;
    
    using Newtonsoft.Json;

    internal class PubAccount
    {
        [JsonProperty("id")]
        public PubIdBundle Id { get; set; }

        [JsonProperty("public_keys")]
        public List<PubPublicKey> PublicKeys { get; set; }
    }
}