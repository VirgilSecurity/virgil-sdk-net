namespace Virgil.Examples.IPMessaging
{
    public class EncryptedMessageModel
    {
        public byte[] EncryptedMessage { get; set; }
        public byte[] Signature { get; set; }
    }
}