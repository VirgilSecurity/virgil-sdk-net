namespace Virgil.SDK.Keys.TransferObject
{
    using System.Collections.Generic;
    using Helpers;
    using Model;
    using Newtonsoft.Json;


    internal class UserDataCreateRequest
    {
        public UserDataCreateRequest()
        {
        }

        public UserDataCreateRequest(UserData userData)
        {
            this.Class = userData.Class.ToJsonValue();
            this.Type = userData.Type.ToJsonValue();
            this.Value = userData.Value;
        }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    internal class PubUserData
    {
        [JsonProperty("id")]
        public PubIdBundle Id { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("is_confirmed")]
        public bool Confirmed { get; set; }

        [JsonProperty("signs")]
        public List<PubSign> Signs { get; set; }
    }
}