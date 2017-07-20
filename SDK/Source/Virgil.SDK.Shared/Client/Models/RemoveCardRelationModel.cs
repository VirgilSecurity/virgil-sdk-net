using Newtonsoft.Json;

namespace Virgil.SDK.Client.Models
{
    class RemoveCardRelationModel : ISnapshotModel
    {
        [JsonProperty("card_id")]
        public string CardId { get; internal set; }

        [JsonProperty("revocation_reason")]
        public RevocationReason RevocationReason { get; internal set; }
    }
}
