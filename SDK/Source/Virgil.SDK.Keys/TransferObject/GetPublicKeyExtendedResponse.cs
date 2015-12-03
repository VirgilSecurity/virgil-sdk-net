using System.Collections.Generic;
using Newtonsoft.Json;

namespace Virgil.SDK.Keys.TransferObject
{
    public class GetPublicKeyExtendedResponse : PublicKeyDto
    {
        [JsonProperty("virgil_cards")]
        public List<VirgilCardDto> VirgilCards { get; set; }
    }
}