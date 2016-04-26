namespace Virgil.SDK.Models
{
    using System;

    using Newtonsoft.Json;
    using Virgil.SDK.Identities;

    /// <summary>
    /// Represents identity object returned from virgil services
    /// </summary>
    public class IdentityModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created at date.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("type")]
        public IdentityType Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this identity is confirmed.
        /// </summary>
        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }
    }
}