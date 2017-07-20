using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK.Client.Models
{
    public class ChallengeMessageModel
    {
        [JsonProperty("authorization_grant_id")]
        public string AuthenticationGrantId { get; set; }

        [JsonProperty("encrypted_message")]
        public byte[] EncryptedMessage { get; set; }
    }
}
