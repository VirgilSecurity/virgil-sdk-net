namespace Virgil.SDK.Shared.Client.TransferObjects
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an confirmed identity information.
    /// </summary>
    internal class IdentityConfirmationResponseModel
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