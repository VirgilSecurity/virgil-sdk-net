namespace Virgil.SDK.TransferObject
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class GetPublicKeyExtendedResponse : PublicKeyDto
    {
        [JsonProperty("virgil_cards")]
        public List<VirgilCardDto> VirgilCards { get; set; }
    }
}