namespace Virgil.SDK.Keys.Domain
{
    public class PublicKey
    {
        internal PublicKey(byte[] publicKeyData)
        {
            this.Data = publicKeyData;
        }

        public byte[] Data { get; private set; }
    }
}