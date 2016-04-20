namespace Virgil.SDK.TransferObject
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Represent public key object returned from virgil public keys service
    /// </summary>
    public class PublicKeyDto
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
        /// Gets or sets the public key.
        /// </summary>
        /// <value>
        /// The public key.
        /// </value>
        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }
    }
}