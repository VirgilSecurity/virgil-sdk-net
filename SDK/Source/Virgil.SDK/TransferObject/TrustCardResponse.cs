namespace Virgil.SDK.TransferObject
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents trust card response
    /// </summary>
    public class TrustCardResponse
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
        /// Gets or sets the signer virgil card identifier.
        /// </summary>
        /// <value>
        /// The signer virgil card identifier.
        /// </value>
        [JsonProperty("signer_virgil_card_id")]
        public Guid SignerVirgilCardId { get; set; }

        /// <summary>
        /// Gets or sets the signed virgil card identifier.
        /// </summary>
        /// <value>
        /// The signed virgil card identifier.
        /// </value>
        [JsonProperty("signed_virgil_card_id")]
        public Guid SignedVirgilCardId { get; set; }

        /// <summary>
        /// Gets or sets the signed digest.
        /// </summary>
        /// <value>
        /// The signed digest.
        /// </value>
        [JsonProperty("signed_digest")]
        public byte[] SignedDigest { get; set; }
    }
}