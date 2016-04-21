namespace Virgil.SDK.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Represents a card that agrigates inself the identity and public key.
    /// </summary>
    public class CardModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is confirmed.
        /// </summary>
        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        [JsonProperty("identity")]
        public IdentityModel Identity { get; set; }
        
        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        [JsonProperty("public_key")]
        public PublicKeyModel PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the custom data.
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, string> CustomData { get; set; }

        /// <summary>
        /// Gets or sets the created at date.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}