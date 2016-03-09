namespace Virgil.Examples.IPMessaging
{
    using System;

    using Virgil.SDK.TransferObject;

    public class ChatMember
    {
        public ChatMember(VirgilCardDto card, byte[] privateKey)
        {
            this.CardId = card.Id;
            this.Identity = card.Identity.Value;
            this.PublicKey = card.PublicKey.PublicKey;
            this.PrivateKey = privateKey;
        }
        
        public Guid CardId { get; set; }
        public string Identity { get; set; }
        public byte[] PublicKey { get; set; }
        public byte[] PrivateKey { get; set; }
    }
}