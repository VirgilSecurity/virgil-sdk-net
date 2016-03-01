namespace Virgil.SDK.TransferObject
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents extended public key grab response
    /// </summary>
    /// <seealso cref="Virgil.SDK.TransferObject.PublicKeyDto" />
    public class GetPublicKeyExtendedResponse : PublicKeyDto
    {
        /// <summary>
        /// Gets or sets the virgil cards.
        /// </summary>
        /// <value>
        /// The virgil cards.
        /// </value>
        [JsonProperty("virgil_cards")]
        public List<VirgilCardDescriptorDto> VirgilCards { get; set; }
    }
}