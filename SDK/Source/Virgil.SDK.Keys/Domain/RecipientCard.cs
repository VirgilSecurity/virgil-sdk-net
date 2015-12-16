namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Text;
    using Crypto;
    using TransferObject;

    public class RecipientCard
    {
        protected RecipientCard()
        {
        }

        internal RecipientCard(VirgilCardDto virgilCardDto)
        {
            this.Id = virgilCardDto.Id;
            this.Identity = new Identity(virgilCardDto.Identity);

            this.PublicKeyId = virgilCardDto.PublicKey.Id;
            this.PublicKey = new PublicKey(virgilCardDto.PublicKey.PublicKey);

            this.Hash = virgilCardDto.Hash;
        }

        public Guid Id { get; protected set; }
        public Identity Identity { get; protected set; }

        public Guid PublicKeyId { get; protected set; }
        public PublicKey PublicKey { get; protected set; }

        public string Hash { get; protected set; }

        public byte[] Encrypt(byte[] data)
        {
            using (var cipher = new VirgilCipher())
            {
                cipher.AddKeyRecipient(PublicKeyId.ToString().GetBytes(), PublicKey.Data);
                return cipher.Encrypt(data, true);
            }
        }

        public byte[] Encrypt(string data)
        {
            return Encrypt(data.GetBytes());
        }

        public byte[] GetRecepientId()
        {
            return this.Id.ToString().ToLowerInvariant().GetBytes(Encoding.UTF8);
        }
    }
}