namespace Virgil.SDK.Keys.Domain
{
    public class PublicKey
    {
        internal PublicKey(byte[] privateKeyData)
        {
            this.Data = privateKeyData;
        }

        public byte[] Data { get; private set; }
    }
}