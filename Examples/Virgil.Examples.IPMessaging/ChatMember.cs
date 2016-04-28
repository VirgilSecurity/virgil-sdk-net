namespace Virgil.Examples.IPMessaging
{
    using System;

    using Virgil.SDK.Models;

    public class ChatMember
    {
        public ChatMember(CardModel card, byte[] privateKey)
        {
            this.CardId = card.Id;
            this.Identity = card.Identity.Value;
            this.PublicKey = card.PublicKey.Value;
            this.PrivateKey = privateKey;
        }
        
        public Guid CardId { get; set; }
        public string Identity { get; set; }
        public byte[] PublicKey { get; set; }
        public byte[] PrivateKey { get; set; }
    }
}