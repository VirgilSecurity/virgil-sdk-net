namespace Virgil.SDK.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Represents an information about public key's cards.
    /// </summary>
    public class PublicKeyExtendedResponse
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
        /// Gets or sets the public key.
        /// </summary>
        [JsonProperty("public_key")]
        public byte[] Value { get; set; }

        /// <summary>
        /// Gets or sets the virgil cards.
        /// </summary>
        [JsonProperty("virgil_cards")]
        public List<CardModel> Cards { get; set; }
    }
}