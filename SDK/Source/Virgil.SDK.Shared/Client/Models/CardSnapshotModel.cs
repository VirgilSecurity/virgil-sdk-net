using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Virgil.SDK.Client;

namespace Virgil.SDK.Client.Models
{
    public class CardSnapshotModel : ISnapshotModel
    {
        [JsonProperty("identity")]
        public string Identity { get; internal set; }

        [JsonProperty("identity_type")]
        public string IdentityType { get; internal set; }

        [JsonProperty("public_key")]
        public byte[] PublicKeyData { get; internal set; }

        [JsonProperty("data")]
        public IReadOnlyDictionary<string, string> CustomFields { get; internal set; }

        [JsonProperty("scope")]
        public CardScope Scope { get; internal set; }

        [JsonProperty("info")]
        public CardInfoModel Info { get; set; }
    }
}