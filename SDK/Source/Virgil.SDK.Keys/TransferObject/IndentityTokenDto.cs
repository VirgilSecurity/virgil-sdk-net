namespace Virgil.SDK.Keys.TransferObject
{
    using Newtonsoft.Json;

    /// <summary>
    /// 
    /// </summary>
    public class IndentityTokenDto
    {
        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        /// <value>
        /// The validation token.
        /// </value>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        public IdentityType Type { get; set; }
    }
}