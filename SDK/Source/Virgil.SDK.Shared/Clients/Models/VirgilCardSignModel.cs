namespace Virgil.SDK.Clients.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a digital signature.
    /// </summary>
    public class VirgilCardSignModel
    {
        /// <summary>
        /// Gets or sets the signer card identifier.
        /// </summary>
        [JsonProperty("signer_card_id")]
        public Guid SignerCardId { get; set; }

        /// <summary>
        /// Gets or sets the signed digest.
        /// </summary>
        [JsonProperty("signed_digest")]
        public byte[] SignedDigest { get; set; }
    }
}