namespace Virgil.SDK.PrivateKeys.TransferObject
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the result of authentication operation.
    /// </summary>
    internal class AuthenticationResult
    {
        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }
    }
}