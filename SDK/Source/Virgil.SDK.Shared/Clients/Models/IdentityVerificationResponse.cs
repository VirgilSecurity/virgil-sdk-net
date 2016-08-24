namespace Virgil.SDK.Identities
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Represents virgil verify response
    /// </summary>
    public class IdentityVerificationResponse
    {
        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        [JsonProperty("action_id")]
        public Guid ActionId { get; set; }
    }
}