namespace Virgil.SDK.Client.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents virgil verify response
    /// </summary>
    public class VerifyEmailModel
    {
        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        [JsonProperty("action_id")]
        public Guid ActionId { get; set; }  
    }
}
