namespace Virgil.SDK.PrivateKeys.TransferObject
{
    using System;
    using Newtonsoft.Json;

    internal class CreateAccountResult
    {
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }
    }
}