namespace Virgil.SDK.TransferObject
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents Virgil Card without public key
    /// </summary>
    public class VirgilCardDescriptorDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardDescriptorDto"/> class.
        /// </summary>
        public VirgilCardDescriptorDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardDescriptorDto"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public VirgilCardDescriptorDto(VirgilCardDescriptorDto source)
        {
            this.Id = source.Id;
            this.CreatedAt = source.CreatedAt;
            this.IsConfirmed = source.IsConfirmed;
            this.Hash = source.Hash;
            this.Identity = source.Identity;
            this.CustomData = source.CustomData;
        }

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
        /// Gets or sets a value indicating whether this instance is confirmed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is confirmed; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        /// <value>
        /// The hash.
        /// </value>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        [JsonProperty("identity")]
        public VirgilIdentityDto Identity { get; set; }

        /// <summary>
        /// Gets or sets the custom data.
        /// </summary>
        /// <value>
        /// The custom data.
        /// </value>
        [JsonProperty("data")]
        public Dictionary<string, string> CustomData { get; set; }
    }
}