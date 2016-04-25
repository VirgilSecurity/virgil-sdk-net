namespace Virgil.SDK.Identities
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an identity information.
    /// </summary>
    public class IdentityInfo
    {
        /// <summary>
        /// Initializes an instance of <see cref="IdentityInfo"/> class.
        /// </summary>
        public IdentityInfo(string identityValue, IdentityType identityType, string validationToken = null)
        {
            this.Value = identityValue;
            this.Type = identityType;
            this.ValidationToken = validationToken;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; private set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("type")]
        public IdentityType Type { get; private set; }

        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; private set; }
        
        /// <summary>
        /// Creates an identity info with email type.
        /// </summary>
        public static IdentityInfo Email(string emailAddress)
        {
            return new IdentityInfo(emailAddress, IdentityType.Email, null);
        }

        /// <summary>
        /// Creates an identity info with custom identity type.
        /// </summary>
        public static IdentityInfo Custom(string emailAddress, string validationToken)
        {
            return new IdentityInfo(emailAddress, IdentityType.Custom, validationToken);
        }
    }
}