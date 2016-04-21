namespace Virgil.SDK.TransferObject
{
    using Newtonsoft.Json;
    
    /// <summary>
    /// Represents identity object returned from virgil card service
    /// </summary>
    public class IdentityTokenDto
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
        public IdentityType Type { get; set; }

        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }
    }
}