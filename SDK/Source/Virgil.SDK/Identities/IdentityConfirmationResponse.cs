namespace Virgil.SDK.Identities
{
    using Newtonsoft.Json;
    using Virgil.SDK.Models;

    /// <summary>
    /// Represents an confirmed identity information.
    /// </summary>
    public class IdentityConfirmationResponse
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
        public VerifiableIdentityType Type { get; set; }

        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }
    }
}