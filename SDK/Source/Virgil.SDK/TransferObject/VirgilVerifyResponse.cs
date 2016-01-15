namespace Virgil.SDK.TransferObject
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents virgil verify response
    /// </summary>
    public class VirgilVerifyResponse
    {
        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        /// <value>
        /// The action identifier.
        /// </value>
        [JsonProperty("action_id")]
        public Guid ActionId { get; set; }
    }
}