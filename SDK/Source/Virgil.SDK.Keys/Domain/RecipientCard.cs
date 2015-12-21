namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Crypto;
    using TransferObject;

    public class RecipientCard
    {
        protected readonly VirgilCardDto VirgilCardDto;

        protected RecipientCard()
        {
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