namespace Virgil.SDK.Clients.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class VirgilCardCreateModel
    {
        /// <summary>
        /// Gets the unique identifier for the Virgil Card.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets public key value
        /// </summary>
        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        [JsonProperty("identity")]
        public string Identity { get; set; }

        /// <summary>
        /// Gets or sets the type of the identity.
        /// </summary>
        [JsonProperty("identity_type")]
        public string IdentityType { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this card is confirmed.
        /// </summary>
        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        [JsonProperty("data")]
        public IDictionary<string, string> Data { get; set; }

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        [JsonProperty("info")]
        public VirgilCardInfoModel Info { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        [JsonProperty("meta")]
        public VirgilCardMetaDataModel Meta { get; set; }

        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        [JsonProperty("virgil_card_validation_token")]
        public string ValidationToken { get; set; }

        /// <summary>
        /// Gets or sets the signs.
        /// </summary>
        [JsonProperty("signs")]
        public IEnumerable<VirgilCardSignModel> Signs { get; set; }
    }
}