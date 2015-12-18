namespace Virgil.SDK.Keys.Domain
{
    using Crypto;

    public class PublicKey
    {
        internal PublicKey(byte[] publicKeyData)
        {
            this.Data = publicKeyData;
        }

        public PublicKey(VirgilKeyPair nativeKeyPair)
        {
            this.Data = nativeKeyPair.PublicKey();
        }

        public byte[] Data { get; private set; }
    }
}