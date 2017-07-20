using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK.Shared.Client.Models
{
    public class AsknowledgeModel
    {
        [JsonProperty("code")]
        public string AccessCode { get; set; }
    }
}
