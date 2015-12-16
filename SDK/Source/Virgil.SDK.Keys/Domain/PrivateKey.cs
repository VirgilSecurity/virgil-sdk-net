namespace Virgil.SDK.Keys.Domain
{
    using Crypto;

    public class PrivateKey
    {
        private PrivateKey()
        {
        }

        internal PrivateKey(byte[] privateKeyData)
        {
            this.Data = privateKeyData;
        }

        internal PrivateKey(VirgilKeyPair virgilKeyPair)
        {
            this.Data = virgilKeyPair.PrivateKey();
        }

        public byte[] Data { get; private set; }
    }
}