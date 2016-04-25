namespace Virgil.SDK.Models
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
        public IdentityInfo()
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="IdentityInfo"/> class.
        /// </summary>
        public IdentityInfo(string identityValue, IdentityType identityType)
        {
            this.Value = identityValue;
            this.Type = identityType;
        }

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
        /// Creates an identity info with email type.
        /// </summary>
        public static IdentityInfo Email(string emailAddress)
        {
            return new IdentityInfo(emailAddress, IdentityType.Email);
        }

        /// <summary>
        /// Creates an identity info with custom identity type.
        /// </summary>
        public static IdentityInfo Custom(string emailAddress)
        {
            return new IdentityInfo(emailAddress, IdentityType.Custom);
        }
    }
}