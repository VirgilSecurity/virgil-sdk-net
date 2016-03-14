namespace Virgil.Examples.IPMessaging
{
    using Newtonsoft.Json;

    public class EncryptedMessageModel
    {
        [JsonProperty("message")]
        public byte[] Message { get; set; }

        [JsonProperty("sign")]
        public byte[] Sign { get; set; }
    }
}