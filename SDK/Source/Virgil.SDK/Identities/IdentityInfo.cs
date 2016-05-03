namespace Virgil.SDK.Identities
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an identity information.
    /// </summary>
    public class IdentityInfo
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }
    }
}