namespace Virgil.PKI.Dtos
{
    using Newtonsoft.Json;

    public class PkiTicketExpand
    {
        [JsonProperty("public_key")]
        public PkiPublicKey PublicKey { get; set; }
    }
}