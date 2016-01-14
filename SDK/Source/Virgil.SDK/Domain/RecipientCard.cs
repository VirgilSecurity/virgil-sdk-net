namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Virgil.Crypto;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    public class RecipientCard
    {
        protected VirgilCardDto VirgilCardDto;

        protected RecipientCard()
        {
        }

        protected RecipientCard(RecipientCard recipientCard)
        {
            this.VirgilCardDto = recipientCard.VirgilCardDto;
            this.Id = recipientCard.Id;
            this.Identity = recipientCard.Identity;
            this.PublicKey = recipientCard.PublicKey;
            this.Hash = recipientCard.Hash;
            this.CreatedAt = recipientCard.CreatedAt;
        }

        internal RecipientCard(VirgilCardDto virgilCardDto)
        {
            this.VirgilCardDto = virgilCardDto;
            this.Id = virgilCardDto.Id;
            this.Identity = new Identity(virgilCardDto.Identity);
            this.PublicKey = new PublishedPublicKey(virgilCardDto.PublicKey);
            this.Hash = virgilCardDto.Hash;
            this.CreatedAt = virgilCardDto.CreatedAt;
        }

        public Dictionary<string, string> CustomData => new Dictionary<string, string>(this.VirgilCardDto.CustomData);

        public Guid Id { get; protected set; }

        public Identity Identity { get; protected set; }

        public PublishedPublicKey PublicKey { get; protected set; }

        public string Hash { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public byte[] Encrypt(byte[] data)
        {
            using (var cipher = new VirgilCipher())
            {
                cipher.AddKeyRecipient(this.GetRecepientId(), this.PublicKey.Data);
                return cipher.Encrypt(data, true);
            }
        }

        public string Encrypt(string data)
        {
            return Convert.ToBase64String(this.Encrypt(data.GetBytes(Encoding.UTF8)));
        }

        public byte[] GetRecepientId()
        {
            return this.Id.ToString().ToLowerInvariant().GetBytes(Encoding.UTF8);
        }
    }
}