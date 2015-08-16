namespace Virgil.SDK.PrivateKeys.TransferObject
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class GetAllPrivateKeysResult
    {
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("data")]
        public IEnumerable<PrivateKeyResult> PrivateKeys { get; set; }
    }
}