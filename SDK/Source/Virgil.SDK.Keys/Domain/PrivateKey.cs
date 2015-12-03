namespace Virgil.SDK.Keys.Domain
{
    public class PrivateKey
    {
        private PrivateKey()
        {
        }

        internal PrivateKey(byte[] privateKeyData)
        {
            this.Data = privateKeyData;
        }

        public byte[] Data { get; private set; }
    }
}