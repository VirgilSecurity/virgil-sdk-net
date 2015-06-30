namespace Virgil.SDK.Keys.TransferObject
{
    using Newtonsoft.Json;

    internal class PkiTicketExpand
    {
        [JsonProperty("public_key")]
        public PkiPublicKey PublicKey { get; set; }
    }
}