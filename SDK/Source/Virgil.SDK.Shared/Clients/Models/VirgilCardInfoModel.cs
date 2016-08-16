namespace Virgil.SDK.Clients.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an information about Virgil Card.
    /// </summary>
    public class VirgilCardInfoModel
    {
        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        [JsonProperty("device")]
        public string Device { get; set; }

        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        [JsonProperty("device_name")]
        public string DeviceName { get; set; }
    }
}