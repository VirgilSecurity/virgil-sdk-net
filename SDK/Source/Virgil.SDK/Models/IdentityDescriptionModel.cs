namespace Virgil.SDK.Models
{
    using Newtonsoft.Json;

    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Represents an identity for creating a card.
    /// </summary>
    public class IdentityDescriptionModel
    {
        /// <summary>
        /// Gets or sets the identity type.
        /// </summary>
        [JsonProperty("type")]
        public IdentityType Type { get; set; }

        /// <summary>
        /// Gets or sets the identity value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the validation token. Used to create card wit confirmed identity.
        /// </summary>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }
    }
}