namespace Virgil.SDK.TransferObject
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents identity object returned from virgil services
    /// </summary>
    public class VirgilIdentityDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created at date.
        /// </summary>
        /// <value>
        /// The created at date.
        /// </value>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        public IdentityType Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this identity is confirmed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this identity is confirmed; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }
    }
}