namespace Virgil.SDK.TransferObject
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class VirgilCardDto : VirgilCardDescriptorDto
    {
        [JsonProperty("public_key")]
        public PublicKeyDto PublicKey { get; set; }

        public VirgilCardDto()
        {
            
        }

        public VirgilCardDto(VirgilCardDescriptorDto descriptor, PublicKeyDto publicKey) : base(descriptor)
        {
            this.PublicKey = publicKey;
        }
    }

    public class VirgilCardDescriptorDto
    {
        public VirgilCardDescriptorDto()
        {
        }

        public VirgilCardDescriptorDto(VirgilCardDescriptorDto source)
        {
            this.Id = source.Id;
            this.CreatedAt = source.CreatedAt;
            this.IsConfirmed = source.IsConfirmed;
            this.Hash = source.Hash;
            this.Identity = source.Identity;
            this.CustomData = source.CustomData;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("identity")]
        public VirgilIdentityDto Identity { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, string> CustomData { get; set; }
    }
}