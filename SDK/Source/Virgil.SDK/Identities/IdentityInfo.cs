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
        public IdentityType Type { get; set; }

        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }
        
        /// <summary>
        /// Creates an identity info with email type.
        /// </summary>
        public static IdentityInfo Email(string emailAddress)
        {
            return new IdentityInfo { Value = emailAddress, Type = IdentityType.Email };
        }

        /// <summary>
        /// Creates an identity info with custom identity type.
        /// </summary>
        public static IdentityInfo Custom(string emailAddress, string validationToken = null)
        {
            return new IdentityInfo { Value = emailAddress, Type = IdentityType.Custom, ValidationToken = validationToken };
        }
    }
}