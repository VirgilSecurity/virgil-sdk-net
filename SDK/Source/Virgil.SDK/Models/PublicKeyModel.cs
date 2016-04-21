namespace Virgil.SDK.Models
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Represent public key object returned from virgil public keys service
    /// </summary>
    public class PublicKeyModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        [JsonProperty("public_key")]
        public byte[] Value { get; set; }

        /// <summary>
        /// Gets or sets the created at date.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}