namespace Virgil.SDK.Keys.Dtos
{
    using Newtonsoft.Json;

    internal class PkiTicketExpand
    {
        [JsonProperty("public_key")]
        public PkiPublicKey PublicKey { get; set; }
    }
}