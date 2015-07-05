namespace Virgil.SDK.Keys.TransferObject
{
    using Newtonsoft.Json;

    internal class PubTicketExpand
    {
        [JsonProperty("public_key")]
        public PubPublicKey PublicKey { get; set; }
    }
}