namespace Virgil.SDK.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents trust card response
    /// </summary>
    public class SignModel
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
        /// Gets or sets the signer virgil card identifier.
        /// </summary>
        [JsonProperty("signer_virgil_card_id")]
        public Guid SignerCardId { get; set; }

        /// <summary>
        /// Gets or sets the signed virgil card identifier.
        /// </summary>
        [JsonProperty("signed_virgil_card_id")]
        public Guid SignedCardId { get; set; }

        /// <summary>
        /// Gets or sets the signed digest.
        /// </summary>
        [JsonProperty("signed_digest")]
        public byte[] SignedDigest { get; set; }
    }
}