namespace Virgil.SDK.TransferObject
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents private key service grab response
    /// </summary>
    public class GrabResponse
    {
        /// <summary>
        /// Gets or sets the virgil card identifier.
        /// </summary>
        /// <value>
        /// The virgil card identifier.
        /// </value>
        [JsonProperty("virgil_card_id")]
        public Guid VirgilCardId { get; set; }

        /// <summary>
        /// Gets or sets the private key.
        /// </summary>
        /// <value>
        /// The private key.
        /// </value>
        [JsonProperty("private_key")]
        public byte[] PrivateKey { get; set; }
    }
}