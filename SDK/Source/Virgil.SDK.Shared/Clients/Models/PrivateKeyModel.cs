namespace Virgil.SDK.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents private key service grab response
    /// </summary>
    public class PrivateKeyModel
    {
        /// <summary>
        /// Gets or sets the virgil card identifier.
        /// </summary>
        [JsonProperty("virgil_card_id")]
        public Guid CardId { get; set; }

        /// <summary>
        /// Gets or sets the private key.
        /// </summary>
        [JsonProperty("private_key")]
        public byte[] PrivateKey { get; set; }
    }
}