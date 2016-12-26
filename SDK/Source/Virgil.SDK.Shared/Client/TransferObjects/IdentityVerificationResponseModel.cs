namespace Virgil.SDK.Shared.Client.TransferObjects
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents virgil verify response
    /// </summary>
    internal class IdentityVerificationResponseModel
    {
        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        [JsonProperty("action_id")]
        public Guid ActionId { get; set; }  
    }
}
