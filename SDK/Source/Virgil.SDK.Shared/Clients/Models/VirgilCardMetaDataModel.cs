namespace Virgil.SDK.Clients.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an information about meta data of the Virgil Card.
    /// </summary>
    public class VirgilCardMetaDataModel
    {
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the fingerprint.
        /// </summary>
        [JsonProperty("fingerprint")]
        public byte[] Fingerprint { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [JsonProperty("card_version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the signs.
        /// </summary>
        [JsonProperty("signs")]
        public IList<VirgilCardSignModel> Signs { get; set; }
    }
}