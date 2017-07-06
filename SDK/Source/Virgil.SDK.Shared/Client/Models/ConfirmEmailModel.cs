namespace Virgil.SDK.Client.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an confirmed identity information.
    /// </summary>
    public class ConfirmEmailModel
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public string Identity { get; set; }


        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }
    }
}