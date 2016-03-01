namespace Virgil.SDK.TransferObject
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents virgil unsign response
    /// </summary>
    public class VirgilUnsignResponse
    {
        /// <summary>
        /// Gets or sets the signed virgil card identifier.
        /// </summary>
        /// <value>
        /// The signed virgil card identifier.
        /// </value>
        [JsonProperty("signed_virgil_card_id")]
        public Guid SignedVirgilCardId { get; set; }
    }
}