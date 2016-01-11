namespace Virgil.SDK.Domain
{
    using System;
    using Virgil.SDK.TransferObject;

    public class PublishedPublicKey : PublicKey
    {
        public PublishedPublicKey(PublicKeyDto publicKeyDto)
        {
            this.Data = publicKeyDto.PublicKey;
            this.Id = publicKeyDto.Id;
            this.CreatedAt = publicKeyDto.CreatedAt;
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public static implicit operator byte[](PublishedPublicKey @this) => @this.Data;
    }
}