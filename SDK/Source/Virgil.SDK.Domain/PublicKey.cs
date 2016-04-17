namespace Virgil.SDK.Domain
{
    using Virgil.Crypto;

    public class PublicKey
    {
        protected PublicKey()
        {
        }

        public PublicKey(byte[] publicKeyData)
        {
            this.Data = publicKeyData;
        }

        public PublicKey(VirgilKeyPair nativeKeyPair)
        {
            this.Data = nativeKeyPair.PublicKey();
        }

        public byte[] Data { get; protected set; }

        public static implicit operator byte[](PublicKey @this) => @this.Data;
    }
}